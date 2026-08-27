<template>
  <div class="results-page">
  <section v-if="ready" class="results">
    <header class="results-hero">
      <div>
        <p class="eyebrow">{{ store.mode === 'single' ? 'Final results' : 'Tournament forecast' }}</p>
        <h1 class="page-title">{{ heading }}</h1>
        <p class="hint">
          {{ store.mode === 'single' ? 'One complete draw' : `${store.iterations.toLocaleString()} simulated draws` }}
          · Seed {{ store.seed }} · Rankings {{ store.snapshotAsOf }}
        </p>
      </div>
      <div class="actions">
        <button class="btn primary" type="button" @click="copyLink">{{ copied ? 'Copied ✓' : 'Copy link' }}</button>
        <button class="btn" type="button" :disabled="refreshing" @click="rerun">{{ refreshing ? 'Running…' : 'Run again' }}</button>
        <RouterLink class="btn ghost" to="/play">New tournament</RouterLink>
      </div>
    </header>

    <template v-if="store.mode === 'single' && store.result">
      <article v-if="champion" class="champion-card">
        <div class="champion-burst" aria-hidden="true"></div>
        <div class="trophy" aria-hidden="true">★</div>
        <div class="champion-flag"><Flag :team="champion.team" /></div>
        <div class="champion-copy">
          <p class="eyebrow">Tournament champion</p>
          <h2>{{ displayName(champion.team) }}</h2>
          <p>{{ champion.team.location }} · Tour rank {{ champion.team.rank }}</p>
        </div>
        <div v-if="finalGame" class="final-score">
          <span>Final</span>
          <strong>{{ finalGame.homeScore }}–{{ finalGame.awayScore }}</strong>
        </div>
      </article>

      <nav class="round-nav" aria-label="Results sections">
        <button type="button" @click="scrollTo('standings')">Standings</button>
        <button v-for="group in gameGroups" :key="group.round" type="button" @click="openAndScroll(group.round)">
          {{ group.title }} <span>{{ group.games.length }}</span>
        </button>
      </nav>

      <section id="standings" class="results-section">
        <div class="section-heading compact">
          <div>
            <p class="eyebrow">The field</p>
            <h2>Final standings</h2>
          </div>
          <span class="legend"><i></i> Qualified for playoffs</span>
        </div>
        <div class="table-wrap">
          <table class="table standings-table">
            <thead>
              <tr>
                <th>Place</th>
                <th></th>
                <th>Team</th>
                <th>W</th>
                <th>L</th>
                <th>LSD</th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="(row, index) in placingStandings"
                :key="row.team.id"
                :class="{ qualified: row.madePlayoffs, champion: row.placing === 1 }"
                :style="{ animationDelay: `${Math.min(index, 12) * 35}ms` }"
              >
                <td class="placing">{{ row.placing }}</td>
                <td><Flag :team="row.team" /></td>
                <td><strong>{{ displayName(row.team) }}</strong><small>{{ row.team.location }}</small></td>
                <td>{{ row.wins }}</td>
                <td>{{ row.losses }}</td>
                <td>{{ Math.round(row.lsdTotal) }} cm</td>
              </tr>
            </tbody>
          </table>
        </div>
      </section>

      <section
        v-for="group in gameGroups"
        :id="`round-${group.round}`"
        :key="group.round"
        class="round-block"
      >
        <button
          class="round-heading"
          type="button"
          :aria-expanded="isRoundOpen(group.round)"
          @click="toggleRound(group.round)"
        >
          <span>
            <small>{{ group.games.length }} {{ group.games.length === 1 ? 'game' : 'games' }}</small>
            <strong>{{ group.title }}</strong>
          </span>
          <i :class="{ open: isRoundOpen(group.round) }" aria-hidden="true"></i>
        </button>
        <Transition name="round">
          <div v-if="isRoundOpen(group.round)" class="games">
            <article
              v-for="(game, i) in group.games"
              :key="`${game.homeId}-${game.awayId}-${i}`"
              class="game"
              :class="{ 'final-game': group.round === 'final' }"
              :style="{ animationDelay: `${Math.min(i, 12) * 45}ms` }"
            >
              <div class="game-label">{{ game.label }}</div>
              <div class="line" :class="{ winner: game.winnerId === game.homeId, loser: game.winnerId !== game.homeId }">
                <Flag :team="team(game.homeId)" />
                <span class="nm">{{ displayName(team(game.homeId)) }}</span>
                <img v-if="game.homeHammer" class="hammer" :src="hammerSrc" alt="Had hammer" />
                <span class="sc">{{ game.homeScore }}</span>
              </div>
              <div class="line" :class="{ winner: game.winnerId === game.awayId, loser: game.winnerId !== game.awayId }">
                <Flag :team="team(game.awayId)" />
                <span class="nm">{{ displayName(team(game.awayId)) }}</span>
                <img v-if="!game.homeHammer" class="hammer" :src="hammerSrc" alt="Had hammer" />
                <span class="sc">{{ game.awayScore }}</span>
              </div>
            </article>
          </div>
        </Transition>
      </section>
    </template>

    <template v-else-if="store.many">
      <section v-if="favorite" class="favorite-card">
        <div class="favorite-rank">#1</div>
        <Flag :team="favorite.team" />
        <div>
          <p class="eyebrow">The favorite</p>
          <h2>{{ displayName(favorite.team) }}</h2>
          <p>{{ favorite.team.location }} · Wins {{ favorite.winPct.toFixed(1) }}% of simulations</p>
        </div>
        <div class="favorite-stat">
          <strong>{{ favorite.winPct.toFixed(1) }}%</strong>
          <span>title chance</span>
        </div>
      </section>

      <section class="results-section">
        <div class="section-heading compact">
          <div>
            <p class="eyebrow">Power rankings</p>
            <h2>Championship odds</h2>
          </div>
          <span class="runs-badge">{{ store.iterations.toLocaleString() }} runs</span>
        </div>
        <div class="table-wrap">
          <table class="table odds-table">
            <thead>
              <tr>
                <th>Rank</th>
                <th></th>
                <th>Team</th>
                <th>Win probability</th>
                <th>Avg place</th>
                <th>Playoff chance</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row, index) in store.many" :key="row.team.id" :style="{ animationDelay: `${Math.min(index, 12) * 45}ms` }">
                <td class="placing">{{ index + 1 }}</td>
                <td><Flag :team="row.team" /></td>
                <td><strong>{{ displayName(row.team) }}</strong><small>{{ row.team.location }}</small></td>
                <td class="odds-cell">
                  <div class="odds-value">{{ row.winPct.toFixed(1) }}%</div>
                  <div class="odds-track"><span :style="{ width: `${Math.max(row.winPct, 1)}%` }"></span></div>
                </td>
                <td>{{ row.avgPlacing.toFixed(2) }}</td>
                <td>{{ row.playoffPct.toFixed(1) }}%</td>
              </tr>
            </tbody>
          </table>
        </div>
      </section>
    </template>
  </section>

  <div v-else class="loading-state" role="status">
    <div class="stone-spinner" aria-hidden="true"><span></span></div>
    <p>Loading the draw…</p>
  </div>

  <SimOverlay
    :active="refreshing"
    eyebrow="New seed, new story"
    :message="simMessage"
    :progress="simProgress"
    :stage-index="simStageIndex"
    :stage-count="simStageCount"
  />
  </div>
</template>

<script setup lang="ts">
import { computed, nextTick, onMounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Flag from '../components/Flag.vue'
import SimOverlay from '../components/SimOverlay.vue'
import { asset } from '../asset'
import { useSimOverlay } from '../composables/useSimOverlay'
import { encodeShare, decodeShare } from '../share'
import type { RoundId, Team } from '../sim/types'
import { useSimStore } from '../stores/sim'
import { randomSeed } from '../sim/rng'

const props = defineProps<{ payload?: string }>()
const store = useSimStore()
const route = useRoute()
const router = useRouter()
const copied = ref(false)
const ready = ref(false)
const collapsedRounds = ref<Set<RoundId>>(new Set(['round-robin', 'pool', 'tko']))
const hammerSrc = asset('images/hammer.png')
const { active: refreshing, message: simMessage, progress: simProgress, stageIndex: simStageIndex, stageCount: simStageCount, present, dismiss } = useSimOverlay()

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

const placingStandings = computed(() =>
  store.result ? [...store.result.stats].sort((a, b) => a.placing - b.placing) : [],
)
const champion = computed(() => store.result?.stats.find((row) => row.placing === 1))
const finalGame = computed(() => store.result?.games.find((game) => game.round === 'final'))
const favorite = computed(() => store.many?.[0])

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

function isRoundOpen(round: RoundId): boolean {
  return !collapsedRounds.value.has(round)
}

function toggleRound(round: RoundId) {
  const next = new Set(collapsedRounds.value)
  if (next.has(round)) next.delete(round)
  else next.add(round)
  collapsedRounds.value = next
}

function scrollTo(id: string) {
  document.getElementById(id)?.scrollIntoView({ behavior: 'smooth', block: 'start' })
}

async function openAndScroll(round: RoundId) {
  if (!isRoundOpen(round)) toggleRound(round)
  await nextTick()
  scrollTo(`round-${round}`)
}

async function rerun() {
  await present(store.mode)
  store.seed = randomSeed()
  store.simulate()
  await nextTick()
  dismiss()
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
