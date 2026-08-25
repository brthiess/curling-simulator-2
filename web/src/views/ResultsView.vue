<template>
  <section class="results" v-if="ready">
    <h1 class="page-title">{{ heading }}</h1>
    <p class="hint">
      Seed {{ store.seed }} · Rankings as of {{ store.snapshotAsOf }} · simulated, not predicted
    </p>
    <div class="actions">
      <button class="btn primary" type="button" @click="copyLink">{{ copied ? 'Copied' : 'Copy share link' }}</button>
      <button class="btn" type="button" @click="rerun">Run again</button>
      <RouterLink class="btn ghost" to="/play">New tournament</RouterLink>
    </div>

    <template v-if="store.mode === 'single' && store.result">
      <h2 class="results-title">Standings</h2>
      <table class="table">
        <thead>
          <tr>
            <th></th>
            <th>Team</th>
            <th>W</th>
            <th>L</th>
            <th>LSD</th>
            <th>Place</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="row in standings" :key="row.team.id" :class="{ qualified: row.madePlayoffs }">
            <td><Flag :team="row.team" /></td>
            <td>{{ displayName(row.team) }}</td>
            <td>{{ row.wins }}</td>
            <td>{{ row.losses }}</td>
            <td>{{ Math.round(row.lsdTotal) }} cm</td>
            <td>{{ row.placing }}</td>
          </tr>
        </tbody>
      </table>

      <div v-for="group in gameGroups" :key="group.round" class="round-block">
        <h2 class="results-title">{{ group.title }}</h2>
        <div class="games">
          <article v-for="(game, i) in group.games" :key="i" class="game">
            <div class="game-label">{{ game.label }}</div>
            <div class="line" :class="{ winner: game.winnerId === game.homeId, loser: game.winnerId !== game.homeId }">
              <Flag :team="team(game.homeId)" />
              <span class="nm">{{ displayName(team(game.homeId)) }}</span>
              <img v-if="game.homeHammer" class="hammer" :src="hammerSrc" alt="Hammer" />
              <span class="sc">{{ game.homeScore }}</span>
            </div>
            <div class="line" :class="{ winner: game.winnerId === game.awayId, loser: game.winnerId !== game.awayId }">
              <Flag :team="team(game.awayId)" />
              <span class="nm">{{ displayName(team(game.awayId)) }}</span>
              <img v-if="!game.homeHammer" class="hammer" :src="hammerSrc" alt="Hammer" />
              <span class="sc">{{ game.awayScore }}</span>
            </div>
          </article>
        </div>
      </div>
    </template>

    <template v-else-if="store.many">
      <h2 class="results-title">{{ store.iterations.toLocaleString() }} runs</h2>
      <table class="table">
        <thead>
          <tr>
            <th></th>
            <th>Team</th>
            <th>Win %</th>
            <th>Avg place</th>
            <th>Playoff %</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="row in store.many" :key="row.team.id">
            <td><Flag :team="row.team" /></td>
            <td>{{ displayName(row.team) }}</td>
            <td>{{ row.winPct.toFixed(1) }}%</td>
            <td>{{ row.avgPlacing.toFixed(2) }}</td>
            <td>{{ row.playoffPct.toFixed(1) }}%</td>
          </tr>
        </tbody>
      </table>
    </template>
  </section>
  <p v-else class="hint">Loading…</p>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Flag from '../components/Flag.vue'
import { asset } from '../asset'
import { encodeShare, decodeShare } from '../share'
import { sortStandings } from '../sim/game'
import type { RoundId, Team } from '../sim/types'
import { useSimStore } from '../stores/sim'
import { randomSeed } from '../sim/rng'

const props = defineProps<{ payload?: string }>()
const store = useSimStore()
const route = useRoute()
const router = useRouter()
const copied = ref(false)
const ready = ref(false)
const hammerSrc = asset('images/hammer.png')

const ROUND_TITLES: { round: RoundId; title: string }[] = [
  { round: 'round-robin', title: 'Round robin' },
  { round: 'pool', title: 'Pool play' },
  { round: 'crossover', title: 'Crossover' },
  { round: 'tko', title: 'Triple knockout' },
  { round: 'qualification', title: 'Qualification' },
  { round: 'page-1-2-qualifier', title: 'Page 1/2 qualifiers' },
  { round: 'page-3-4-qualifier', title: 'Page 3/4 qualifiers' },
  { round: 'page-1-2', title: 'Page 1v2' },
  { round: 'page-3-4', title: 'Page 3v4' },
  { round: 'quarters', title: 'Quarterfinals' },
  { round: 'semis', title: 'Semifinals' },
  { round: 'bronze', title: 'Bronze' },
  { round: 'final', title: 'Final' },
]

const heading = computed(() => {
  const g = store.gender === 'men' ? 'Men’s' : 'Women’s'
  if (store.formatId === 'scotties' || store.formatId === 'brier') {
    return `${store.format.name} · ${store.variant.name}`
  }
  return `${g} ${store.format.name} · ${store.variant.name}`
})

const standings = computed(() => (store.result ? sortStandings(store.result.stats) : []))

const gameGroups = computed(() => {
  if (!store.result) return []
  return ROUND_TITLES.map(({ round, title }) => ({
    round,
    title,
    games: store.result!.games.filter((g) => g.round === round),
  })).filter((g) => g.games.length)
})

function team(id: string): Team {
  return store.selected.find((t) => t.id === id) ?? {
    id,
    firstName: '',
    lastName: id,
    location: '',
    rank: 99,
    flag: 'canada.png',
    gender: store.gender,
  }
}

function displayName(t: Team): string {
  return t.firstName ? `${t.firstName} ${t.lastName}` : `Team ${t.lastName}`
}

function copyLink() {
  const url = `${window.location.origin}${window.location.pathname}#/s/${encodeShare(store.sharePayload())}`
  void navigator.clipboard.writeText(url)
  copied.value = true
  setTimeout(() => {
    copied.value = false
  }, 1500)
}

function rerun() {
  store.seed = randomSeed()
  store.simulate()
}

onMounted(async () => {
  const raw = props.payload || (typeof route.params.payload === 'string' ? route.params.payload : '')
  if (raw) {
    try {
      await store.restore(decodeShare(raw))
    } catch {
      router.replace('/')
      return
    }
  }
  if (!store.result && !store.many && store.selected.length) store.simulate()
  ready.value = true
  if (!store.result && !store.many) router.replace('/')
})
</script>
