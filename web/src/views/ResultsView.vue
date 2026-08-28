<template>
  <div class="results-page">
  <section v-if="ready" class="results" :class="store.mode === 'single' ? 'results-single' : 'results-many'">
    <template v-if="store.mode === 'single' && store.result">
      <header class="res-head">
        <h1 class="res-title res-title-pulse crt-glow">
          &gt; TOURNAMENT COMPLETE<span class="crt-cursor" aria-hidden="true">_</span>
        </h1>
        <p class="res-meta">
          [META] One complete draw · {{ heading }} · Seed {{ store.seed }} · Rankings {{ store.snapshotAsOf }}
        </p>
      </header>

      <article v-if="champion" class="champion-banner">
        <div class="champion-hatch" aria-hidden="true">\\\\\\\\\\\\\\\\\\\</div>
        <div class="champion-flag" aria-hidden="true">
          <pre>  /| ________________
O|===|* * * * * * *|
  \| |* * * * * * *|
     |_____________|
     |
     |</pre>
        </div>
        <div class="champion-copy">
          <div class="champion-tag">[ CHAMPION ]</div>
          <h2>{{ championLabel(champion.team) }}</h2>
        </div>
        <div v-if="finalGame" class="final-score">
          <div class="final-score-label">FINAL SCORE</div>
          <div class="final-score-box">{{ finalGame.homeScore }} - {{ finalGame.awayScore }}</div>
        </div>
      </article>

      <div class="res-grid">
        <section id="standings" class="crt-panel standings-panel">
          <header class="crt-panel-head">
            <span class="crt-panel-title">┌── FINAL STANDINGS ────────┐</span>
          </header>
          <div class="crt-panel-body">
            <table class="crt-table standings-table">
              <thead>
                <tr>
                  <th>RK</th>
                  <th>TEAM</th>
                  <th class="num">W</th>
                  <th class="num">L</th>
                  <th class="num">LSD</th>
                </tr>
              </thead>
              <tbody>
                <template v-for="(row, index) in placingStandings" :key="row.team.id">
                  <tr :class="standingsRowClass(row, index)">
                    <td>{{ padRank(row.placing) }}</td>
                    <td>{{ qualPrefix(row) }}{{ skipName(row.team) }}</td>
                    <td class="num">{{ row.wins }}</td>
                    <td class="num">{{ row.losses }}</td>
                    <td class="num">{{ row.lsdTotal.toFixed(1) }}</td>
                  </tr>
                  <tr v-if="showQualDivider(index)" class="standings-split">
                    <td colspan="5"></td>
                  </tr>
                </template>
              </tbody>
            </table>
          </div>
        </section>

        <div class="res-side">
          <section class="crt-panel playoff-panel">
            <header class="crt-panel-head">
              <span class="crt-panel-title">┌── {{ playoffTitle }} ──────┐</span>
            </header>
            <div class="crt-panel-body bracket-wrap">
              <div v-if="pageBracket" class="bracket-ascii" aria-label="Page playoff results">
                <div v-for="(row, i) in pageBracket" :key="i">
                  <span v-for="(seg, j) in row" :key="j" :class="seg.k">{{ seg.t }}</span>
                </div>
              </div>
              <div v-else class="bracket-list" aria-label="Playoff games">
                <article v-for="(game, i) in knockoutGames" :key="`${game.homeId}-${game.awayId}-${i}`" class="bracket-game">
                  <div class="game-label">{{ game.label }}</div>
                  <div class="line" :class="{ winner: game.winnerId === game.homeId, loser: game.winnerId !== game.homeId }">
                    <span class="nm">{{ skipName(team(game.homeId)) }}</span>
                    <img v-if="game.homeHammer" class="hammer" :src="hammerSrc" alt="Had hammer" />
                    <span class="sc">{{ game.homeScore }}</span>
                  </div>
                  <div class="line" :class="{ winner: game.winnerId === game.awayId, loser: game.winnerId !== game.awayId }">
                    <span class="nm">{{ skipName(team(game.awayId)) }}</span>
                    <img v-if="!game.homeHammer" class="hammer" :src="hammerSrc" alt="Had hammer" />
                    <span class="sc">{{ game.awayScore }}</span>
                  </div>
                </article>
                <p v-if="finalHighlight" class="bracket-final">{{ finalHighlight }}</p>
              </div>
            </div>
          </section>

          <section class="sys-log" aria-hidden="true">
            <div class="sys-log-label">&gt;&gt; SYS_LOG_OUTPUT</div>
            <div class="sys-log-body">
              <p>&gt; Calculating final Elo ratings... [OK]</p>
              <p>&gt; Updating history tables... [OK]</p>
              <p>&gt; Generating statistical summary...</p>
              <p>&gt; Seed {{ store.seed }} run completed.</p>
              <p>&gt; Rankings snapshot {{ store.snapshotAsOf }}</p>
              <p>&gt; Waiting for user input...<span class="sys-cursor"></span></p>
            </div>
          </section>
        </div>
      </div>

      <nav class="round-nav" aria-label="Results sections">
        <button type="button" @click="scrollTo('standings')">Standings</button>
        <button v-for="group in gameGroups" :key="group.round" type="button" @click="openAndScroll(group.round)">
          {{ group.title }} <span>{{ group.games.length }}</span>
        </button>
      </nav>

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

      <footer class="res-actions">
        <button class="btn" type="button" @click="copyLink">
          <svg class="crt-glyph crt-glyph-sm" viewBox="0 0 24 24" fill="none" aria-hidden="true">
            <path d="M10 14.5 8.8 15.7a3.2 3.2 0 0 1-4.5-4.5l3.2-3.2a3.2 3.2 0 0 1 4.5 0" stroke="currentColor" stroke-width="1.6" />
            <path d="M14 9.5 15.2 8.3a3.2 3.2 0 0 1 4.5 4.5l-3.2 3.2a3.2 3.2 0 1 1-4.5 0" stroke="currentColor" stroke-width="1.6" />
          </svg>
          {{ copied ? '[ COPIED ]' : '[ COPY LINK ]' }}
        </button>
        <button class="btn" type="button" :disabled="refreshing" @click="rerun">
          <svg class="crt-glyph crt-glyph-sm" viewBox="0 0 24 24" fill="none" aria-hidden="true">
            <path d="M19 12a7 7 0 1 1-2-4.9" stroke="currentColor" stroke-width="1.6" />
            <path d="M19 5v5h-5" stroke="currentColor" stroke-width="1.6" />
          </svg>
          {{ refreshing ? '[ RUNNING… ]' : '[ RUN AGAIN ]' }}
        </button>
        <RouterLink class="btn btn-accent" to="/play">
          <svg class="crt-glyph crt-glyph-sm" viewBox="0 0 24 24" fill="none" aria-hidden="true">
            <rect x="4" y="4" width="16" height="16" stroke="currentColor" stroke-width="1.6" />
            <path d="M12 8v8M8 12h8" stroke="currentColor" stroke-width="1.6" />
          </svg>
          [ + NEW TOURNAMENT ]
        </RouterLink>
      </footer>
    </template>

    <template v-else-if="store.many">
      <header class="res-head many-head">
        <div>
          <h1 class="res-title crt-glow">TOURNAMENT FORECAST</h1>
          <p class="res-meta-line">
            ITERATIONS: {{ store.iterations.toLocaleString() }} · SEED {{ store.seed }} · RANKINGS {{ store.snapshotAsOf }}
          </p>
        </div>
        <div class="sys-chrome" aria-hidden="true">
          SYS_TIME: {{ sysTime }}<br />
          MEM_USAGE: 640K OK
        </div>
      </header>

      <section v-if="favorite" class="favorite-block">
        <span class="corner tl" aria-hidden="true"></span>
        <span class="corner tr" aria-hidden="true"></span>
        <span class="corner bl" aria-hidden="true"></span>
        <span class="corner br" aria-hidden="true"></span>
        <div>
          <h2 class="favorite-kicker">&gt;&gt; PREDICTED FAVORITE</h2>
          <p class="favorite-name">[ {{ championLabel(favorite.team) }} ]</p>
        </div>
        <div class="favorite-stat">
          <div class="favorite-kicker">TITLE CHANCE</div>
          <div class="favorite-pct">{{ favorite.winPct.toFixed(1) }}%</div>
        </div>
      </section>

      <section class="odds-matrix">
        <header class="odds-matrix-head">
          <span>TITLE ODDS MATRIX</span>
          <span>SORT: WIN % (DESC)</span>
        </header>
        <div class="odds-matrix-body">
          <table class="crt-table odds-table">
            <thead>
              <tr>
                <th>RANK</th>
                <th>TEAM</th>
                <th>WIN %</th>
                <th>PROBABILITY DISTRIBUTION</th>
                <th>AVG FINISH</th>
                <th class="num">PLAYOFF %</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row, index) in store.many" :key="row.team.id">
                <td>{{ padRank(index + 1) }}</td>
                <td>{{ teamTag(row.team) }}</td>
                <td :class="{ 'win-lead': index === 0 }">{{ row.winPct.toFixed(1) }}%</td>
                <td class="dist" aria-hidden="true">
                  <span class="progress-filled">{{ winDist(row.winPct).fill }}</span><span class="progress-empty">{{ winDist(row.winPct).rest }}</span>
                </td>
                <td>{{ row.avgPlacing.toFixed(1) }}</td>
                <td class="num">{{ row.playoffPct.toFixed(1) }}%</td>
              </tr>
            </tbody>
          </table>
        </div>
      </section>

      <footer class="res-actions">
        <button class="btn" type="button" @click="copyLink">
          <svg class="crt-glyph crt-glyph-sm" viewBox="0 0 24 24" fill="none" aria-hidden="true">
            <path d="M10 14.5 8.8 15.7a3.2 3.2 0 0 1-4.5-4.5l3.2-3.2a3.2 3.2 0 0 1 4.5 0" stroke="currentColor" stroke-width="1.6" />
            <path d="M14 9.5 15.2 8.3a3.2 3.2 0 0 1 4.5 4.5l-3.2 3.2a3.2 3.2 0 1 1-4.5 0" stroke="currentColor" stroke-width="1.6" />
          </svg>
          {{ copied ? '[ COPIED ]' : '[ COPY LINK ]' }}
        </button>
        <button class="btn" type="button" :disabled="refreshing" @click="rerun">
          <svg class="crt-glyph crt-glyph-sm" viewBox="0 0 24 24" fill="none" aria-hidden="true">
            <path d="M19 12a7 7 0 1 1-2-4.9" stroke="currentColor" stroke-width="1.6" />
            <path d="M19 5v5h-5" stroke="currentColor" stroke-width="1.6" />
          </svg>
          {{ refreshing ? '[ RUNNING… ]' : '[ RUN AGAIN ]' }}
        </button>
        <RouterLink class="btn btn-accent" to="/play">
          <svg class="crt-glyph crt-glyph-sm" viewBox="0 0 24 24" fill="none" aria-hidden="true">
            <rect x="4" y="4" width="16" height="16" stroke="currentColor" stroke-width="1.6" />
            <path d="M12 8v8M8 12h8" stroke="currentColor" stroke-width="1.6" />
          </svg>
          [ + NEW TOURNAMENT ]
        </RouterLink>
      </footer>
    </template>
  </section>

  <div v-else class="loading-state" role="status">
    <div class="stone-spinner" aria-hidden="true"><span></span></div>
    <p>LOADING THE DRAW…</p>
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
import { computed, nextTick, onMounted, onUnmounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Flag from '../components/Flag.vue'
import SimOverlay from '../components/SimOverlay.vue'
import { asset } from '../asset'
import { useSimOverlay } from '../composables/useSimOverlay'
import { encodeShare, decodeShare } from '../share'
import type { PlayedGame, RoundId, Team, TeamStats } from '../sim/types'
import { useSimStore } from '../stores/sim'
import { randomSeed } from '../sim/rng'

type BracketSeg = { t: string; k?: 'muted' | 'win' | 'hi' | 'gold' }

const KNOCKOUT_ROUNDS: RoundId[] = [
  'qualification',
  'page-1-2-qualifier',
  'page-3-4-qualifier',
  'page-1-2',
  'page-3-4',
  'quarters',
  'semis',
  'bronze',
  'final',
  'crossover',
]

const props = defineProps<{ payload?: string }>()
const store = useSimStore()
const route = useRoute()
const router = useRouter()
const copied = ref(false)
const ready = ref(false)
const sysTime = ref('00:00:00')
const collapsedRounds = ref<Set<RoundId>>(new Set(['round-robin', 'pool', 'tko']))
const hammerSrc = asset('images/hammer.png')
const { active: refreshing, message: simMessage, progress: simProgress, stageIndex: simStageIndex, stageCount: simStageCount, present, dismiss } = useSimOverlay()

let clockTimer: number | undefined

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

const knockoutGames = computed(() => {
  if (!store.result) return []
  return store.result.games.filter((g) => KNOCKOUT_ROUNDS.includes(g.round))
})

const playoffTitle = computed(() => {
  if (!store.result) return 'PLAYOFFS'
  if (store.result.games.some((g) => g.round === 'page-1-2')) return 'PAGE PLAYOFFS'
  return 'PLAYOFFS'
})

const finalHighlight = computed(() => {
  const game = finalGame.value
  if (!game) return ''
  const winner = team(game.winnerId)
  const top = game.winnerId === game.homeId ? game.homeScore : game.awayScore
  const bot = game.winnerId === game.homeId ? game.awayScore : game.homeScore
  return `[FINAL: ${skipName(winner)} ${top}-${bot}]`
})

const pageBracket = computed(() => {
  const games = store.result?.games
  if (!games) return null
  const oneTwo = games.find((g) => g.round === 'page-1-2')
  const threeFour = games.find((g) => g.round === 'page-3-4')
  const semis = games.filter((g) => g.round === 'semis')
  const finalG = games.find((g) => g.round === 'final')
  if (!oneTwo || !threeFour || semis.length !== 1 || !finalG) return null
  return buildPageBracket(oneTwo, threeFour, semis[0], finalG)
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

function skipName(t: Team): string {
  return t.lastName.toUpperCase()
}

function teamTag(t: Team): string {
  return t.location ? `${skipName(t)} (${t.location.toUpperCase()})` : skipName(t)
}

function championLabel(t: Team): string {
  return t.location ? `TEAM ${skipName(t)} (${t.location.toUpperCase()})` : `TEAM ${skipName(t)}`
}

function padRank(n: number): string {
  return String(n).padStart(2, '0')
}

function qualPrefix(row: TeamStats): string {
  return row.madePlayoffs ? '[Q] ' : '    '
}

function standingsRowClass(row: TeamStats, index: number): string {
  const n = placingStandings.value.length
  const dimFrom = Math.max(n - 2, Math.ceil(n * 0.75))
  return [
    row.madePlayoffs ? 'qualified' : '',
    !row.madePlayoffs && index >= dimFrom ? 'dim' : '',
  ].filter(Boolean).join(' ')
}

function showQualDivider(index: number): boolean {
  const rows = placingStandings.value
  const row = rows[index]
  const next = rows[index + 1]
  return Boolean(row?.madePlayoffs && next && !next.madePlayoffs)
}

function winDist(pct: number): { fill: string; rest: string } {
  const width = 10
  const filled = Math.min(width, Math.round(pct / 6))
  if (filled === 0 && pct >= 1) return { fill: '.', rest: '░'.repeat(width - 1) }
  return { fill: '█'.repeat(filled), rest: '░'.repeat(width - filled) }
}

function padSkip(id: string, width = 10): string {
  return skipName(team(id)).slice(0, width).padEnd(width, ' ')
}

function scoreBox(n: number): string {
  return `[${n}]`.padStart(4, ' ')
}

function buildPageBracket(oneTwo: PlayedGame, threeFour: PlayedGame, semi: PlayedGame, finalG: PlayedGame): BracketSeg[][] {
  const oneTwoHomeWin = oneTwo.winnerId === oneTwo.homeId
  const threeFourHomeWin = threeFour.winnerId === threeFour.homeId
  const byeName = skipName(team(oneTwo.winnerId))
  const threeFourWinner = skipName(team(threeFour.winnerId))
  const semiLoserName = skipName(team(semi.loserId))
  const semiLoserScore = semi.winnerId === semi.homeId ? semi.awayScore : semi.homeScore
  const finalTop = finalG.winnerId === finalG.homeId ? finalG.homeScore : finalG.awayScore
  const finalBot = finalG.winnerId === finalG.homeId ? finalG.awayScore : finalG.homeScore

  const r1: BracketSeg[] = [
    { t: `1 ${padSkip(oneTwo.homeId)}${scoreBox(oneTwo.homeScore)} ──┐`, k: oneTwoHomeWin ? 'win' : 'muted' },
  ]
  const r2: BracketSeg[] = [{ t: '                   │', k: 'muted' }]
  const r3: BracketSeg[] = [
    { t: `2 ${padSkip(oneTwo.awayId)}${scoreBox(oneTwo.awayScore)} ──┴── `, k: oneTwoHomeWin ? 'muted' : 'win' },
    { t: `${byeName} (BYE to F)`, k: 'hi' },
  ]
  const r4: BracketSeg[] = [
    { t: `3 ${padSkip(threeFour.homeId)}${scoreBox(threeFour.homeScore)} ──┬── `, k: threeFourHomeWin ? 'win' : 'muted' },
    { t: threeFourWinner, k: 'win' },
  ]
  const r5: BracketSeg[] = [{ t: '                   │      │', k: 'muted' }]
  const r6: BracketSeg[] = [
    { t: `4 ${padSkip(threeFour.awayId)}${scoreBox(threeFour.awayScore)} ──┘      │`, k: threeFourHomeWin ? 'muted' : 'win' },
  ]
  const r7: BracketSeg[] = [{ t: '                          │', k: 'muted' }]
  const r8: BracketSeg[] = [
    { t: `L1 ${padSkip(oneTwo.loserId, 9)}${scoreBox(oneTwo.winnerId === oneTwo.homeId ? oneTwo.awayScore : oneTwo.homeScore)} ─────────┴── `, k: 'muted' },
    { t: `${semiLoserName} ${scoreBox(semiLoserScore)}`, k: 'muted' },
  ]
  const r9: BracketSeg[] = [{ t: '                              │', k: 'muted' }]
  const r10: BracketSeg[] = [{ t: '                    ', k: 'muted' }, { t: `[FINAL: ${skipName(team(finalG.winnerId))} ${finalTop}-${finalBot}]`, k: 'gold' }]
  return [r1, r2, r3, r4, r5, r6, r7, r8, r9, r10]
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

function tickClock() {
  sysTime.value = new Date().toLocaleTimeString('en-US', { hour12: false })
}

onMounted(async () => {
  tickClock()
  clockTimer = window.setInterval(tickClock, 1000)
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

onUnmounted(() => {
  if (clockTimer !== undefined) window.clearInterval(clockTimer)
})
</script>
