<template>
  <div class="play">
    <nav class="crt-steps" aria-label="Tournament setup">
      <ol>
        <template v-for="(s, index) in visibleSteps" :key="s.id">
          <li
            v-if="index > 0"
            class="crt-steps-sep"
            :class="{ dim: index > currentStepIndex }"
            aria-hidden="true"
          >&gt;</li>
          <li :class="{ on: step === s.id, done: index < currentStepIndex, dim: index > currentStepIndex }">
            <button
              type="button"
              :disabled="index > currentStepIndex"
              :aria-current="step === s.id ? 'step' : undefined"
              :aria-label="s.label"
              @click="step = s.id"
            >
              {{ stepIndexLabel(index) }} {{ s.code }}
            </button>
          </li>
        </template>
      </ol>
    </nav>

    <Transition name="step" mode="out-in">
    <section v-if="step === 'format'" :key="step" class="step-panel champ-step">
      <header class="crt-step-head">
        <h1 class="page-title">Pick a championship_</h1>
        <p class="hint crt-path">C:\SIM\SELECT_MODE.EXE</p>
      </header>
      <div class="format-grid">
        <button
          v-for="card in formatCards"
          :key="card.key"
          class="format-card"
          type="button"
          @click="chooseFormat(card.formatId, card.gender)"
        >
          <span class="format-tag" :class="`tone-${card.tagTone}`">{{ card.tag }}</span>
          <span class="format-icon" :class="`icon-${card.icon}`" aria-hidden="true">
            <svg v-if="card.icon === 'globe'" viewBox="0 0 48 48" fill="none">
              <circle cx="24" cy="24" r="15" stroke="currentColor" stroke-width="2.4" />
              <ellipse cx="24" cy="24" rx="6.5" ry="15" stroke="currentColor" stroke-width="2.4" />
              <path d="M9 24h30M12.5 16h23M12.5 32h23" stroke="currentColor" stroke-width="2.4" />
            </svg>
            <svg v-else-if="card.icon === 'map'" viewBox="0 0 48 48" fill="none">
              <path d="M8 14 20 10l12 4 8-4v24l-8 4-12-4-12 4V14Z" stroke="currentColor" stroke-width="2.4" stroke-linejoin="round" />
              <path d="M20 10v24M32 14v24" stroke="currentColor" stroke-width="2.4" />
            </svg>
            <span v-else-if="card.icon === 'euro'">€</span>
            <svg v-else-if="card.icon === 'flag'" viewBox="0 0 48 48" fill="none">
              <path d="M12 6v36" stroke="currentColor" stroke-width="2.6" stroke-linecap="square" />
              <rect x="14" y="8" width="22" height="16" stroke="currentColor" stroke-width="2" />
              <path fill="currentColor" d="M16 10h5v4h-5zm10 0h5v4h-5zM21 14h5v4h-5zM16 18h5v4h-5zm10 0h5v4h-5z" />
            </svg>
          </span>
          <span class="format-name">{{ card.label }}</span>
        </button>
      </div>
      <p class="crt-ready"><span>STATUS: READY</span><span class="crt-cursor" aria-hidden="true">_</span></p>
    </section>

    <section v-else-if="step === 'variant'" :key="step" class="step-panel">
      <h1 class="page-title">Format variant</h1>
      <p class="hint">{{ store.format.name }} · {{ store.gender }}</p>
      <div class="choice-list">
        <button
          v-for="v in store.format.variants"
          :key="v.id"
          class="choice"
          type="button"
          @click="chooseVariant(v.id)"
        >
          <strong>{{ v.name }}</strong>
          <span>{{ v.blurb }}</span>
        </button>
      </div>
      <button class="btn ghost" type="button" @click="step = 'format'">Back</button>
    </section>

    <section v-else-if="step === 'field'" :key="step" class="step-panel">
      <h1 class="page-title">The field</h1>
      <p class="hint">{{ store.variant.fieldSize }} teams · {{ store.variant.name }}</p>
      <div class="choice-list">
        <button
          v-for="p in availablePresets"
          :key="p.id"
          class="choice"
          type="button"
          @click="choosePreset(p)"
        >
          <strong>{{ p.name }}</strong>
          <span v-if="p.kind === 'top'">Uses the current snapshot ranking list.</span>
          <span v-else>The real championship field, strength from Tour ranking.</span>
        </button>
        <button class="choice" type="button" @click="chooseCustom">
          <strong>Custom field</strong>
          <span>Pick any {{ store.variant.fieldSize }} teams from the Tour.</span>
        </button>
      </div>
      <button class="btn ghost" type="button" @click="backFromField">Back</button>
    </section>

    <section v-else-if="step === 'teams'" :key="step" class="step-panel">
      <h1 class="page-title">Pick teams</h1>
      <div class="selection-status">
        <div>
          <strong>{{ store.selected.length }} of {{ store.variant.fieldSize }}</strong>
          <span>teams selected · Rankings as of {{ store.snapshotAsOf }}</span>
        </div>
        <div class="selection-track" role="progressbar" :aria-valuenow="store.selected.length" aria-valuemin="0" :aria-valuemax="store.variant.fieldSize">
          <span :style="{ width: `${selectionProgress}%` }"></span>
        </div>
      </div>
      <div class="picker-tools">
        <input v-model="query" class="search" type="search" placeholder="Search skip or location" />
        <button class="btn" type="button" @click="fillTopTeams">Pick top {{ store.variant.fieldSize }}</button>
      </div>
      <ul class="teams">
        <li v-for="team in filteredTour" :key="team.id">
          <label class="team-row" :class="{ on: isSelected(team.id), dim: pickerFull && !isSelected(team.id) }">
            <input
              type="checkbox"
              :checked="isSelected(team.id)"
              :disabled="pickerFull && !isSelected(team.id)"
              @change="store.toggleTeam(team)"
            />
            <Flag :team="team" />
            <span class="team-meta">
              <span class="team-name">{{ displayName(team) }}</span>
              <span class="team-loc">{{ team.location }} · rank {{ team.rank }}</span>
            </span>
          </label>
        </li>
      </ul>
      <div class="bar">
        <button class="btn ghost" type="button" @click="step = 'field'">Back</button>
        <button class="btn primary" type="button" :disabled="!fieldFull" @click="afterTeams">Continue</button>
      </div>
    </section>

    <section v-else-if="step === 'pools'" :key="step" class="pools-section step-panel">
      <h1 class="page-title">Assign pools</h1>
      <div class="pool-intro">
        <p class="hint">Drag teams into pools. Each pool needs {{ store.variant.poolSize }}.</p>
        <button class="btn" type="button" @click="autoAssignPools">Auto-balance pools</button>
      </div>
      <div class="pool-board">
        <div class="pool-col unassigned">
          <h2>Unassigned ({{ unassigned.length }})</h2>
          <div
            class="drop"
            @dragover.prevent
            @drop="onDrop(undefined, $event)"
          >
            <div
              v-for="team in unassigned"
              :key="team.id"
              class="chip"
              draggable="true"
              @dragstart="onDrag(team.id, $event)"
            >
              <Flag :team="team" /> {{ displayName(team) }}
            </div>
          </div>
        </div>
        <div v-for="name in store.poolNames" :key="name" class="pool-col">
          <h2>Pool {{ name }} ({{ poolTeams(name).length }}/{{ store.variant.poolSize }})</h2>
          <div
            class="drop"
            @dragover.prevent
            @drop="onDrop(name, $event)"
          >
            <div
              v-for="team in poolTeams(name)"
              :key="team.id"
              class="chip"
              draggable="true"
              @dragstart="onDrag(team.id, $event)"
            >
              <Flag :team="team" /> {{ displayName(team) }}
            </div>
          </div>
        </div>
      </div>
      <div class="bar">
        <button class="btn ghost" type="button" @click="backFromPools">Back</button>
        <button class="btn primary" type="button" :disabled="!store.poolsReady" @click="step = 'run'">Continue</button>
      </div>
    </section>

    <section v-else-if="step === 'run'" :key="step" class="step-panel run-step">
      <h1 class="page-title">How to run it</h1>
      <p class="hint">One dramatic draw, or the big-picture odds.</p>
      <div class="choice-list">
        <button class="choice" :class="{ on: store.mode === 'single' }" type="button" @click="store.mode = 'single'">
          <strong>Single run</strong>
          <span>One tournament. Inspect every game, hammer, and final score.</span>
        </button>
        <button class="choice" :class="{ on: store.mode === 'many' }" type="button" @click="store.mode = 'many'">
          <strong>Many-run</strong>
          <span>How often each team wins, and average placing.</span>
        </button>
      </div>
      <label v-if="store.mode === 'many'" class="iters">
        Iterations
        <select v-model.number="store.iterations">
          <option :value="100">100</option>
          <option :value="1000">1,000</option>
          <option :value="10000">10,000</option>
        </select>
      </label>
      <div class="bar">
        <button class="btn ghost" type="button" @click="backFromRun">Back</button>
        <button class="btn primary" type="button" :disabled="simulating" @click="go">
          {{ simulating ? 'Running…' : 'Simulate tournament' }}
        </button>
      </div>
    </section>
    </Transition>

    <SimOverlay
      :active="simulating"
      :eyebrow="store.mode === 'many' ? `${store.iterations.toLocaleString()} tournaments` : 'One tournament'"
      :message="simMessage"
      :progress="simProgress"
      :stage-index="simStageIndex"
      :stage-count="simStageCount"
      detail="Fresh ice. New seed. Anything can happen."
    />
  </div>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import Flag from '../components/Flag.vue'
import SimOverlay from '../components/SimOverlay.vue'
import { useSimOverlay } from '../composables/useSimOverlay'
import { assignSnakePools, presetsFor, type Preset } from '../data/presets'
import { topN } from '../data/tour'
import { getFormat } from '../sim/catalog'
import type { FormatId, Gender, Team, VariantId } from '../sim/types'
import { useSimStore } from '../stores/sim'

type Step = 'format' | 'variant' | 'field' | 'teams' | 'pools' | 'run'
const step = ref<Step>('format')
const query = ref('')
const custom = ref(false)
const store = useSimStore()
const router = useRouter()
const { active: simulating, message: simMessage, progress: simProgress, stageIndex: simStageIndex, stageCount: simStageCount, present, dismiss } = useSimOverlay()

const formatCards = [
  { key: 'ww', formatId: 'worlds' as const, gender: 'women' as const, label: 'Women’s Worlds', tag: '[W]', tagTone: 'w', icon: 'globe' as const },
  { key: 'wm', formatId: 'worlds' as const, gender: 'men' as const, label: 'Men’s Worlds', tag: '[M]', tagTone: 'm', icon: 'globe' as const },
  { key: 'sc', formatId: 'scotties' as const, gender: 'women' as const, label: 'Scotties', tag: '[CAN]', tagTone: 'can', icon: 'map' as const },
  { key: 'br', formatId: 'brier' as const, gender: 'men' as const, label: 'Brier', tag: '[CAN]', tagTone: 'can', icon: 'map' as const },
  { key: 'ew', formatId: 'europeans' as const, gender: 'women' as const, label: 'Women’s Europeans', tag: '[W]', tagTone: 'w', icon: 'euro' as const },
  { key: 'em', formatId: 'europeans' as const, gender: 'men' as const, label: 'Men’s Europeans', tag: '[M]', tagTone: 'm', icon: 'euro' as const },
  { key: 'sw', formatId: 'slam' as const, gender: 'women' as const, label: 'Women’s Slam', tag: '[W]', tagTone: 'w', icon: 'flag' as const },
  { key: 'sm', formatId: 'slam' as const, gender: 'men' as const, label: 'Men’s Slam', tag: '[M]', tagTone: 'm', icon: 'flag' as const },
]

const visibleSteps = computed(() => {
  const all: { id: Step; label: string; code: string }[] = [
    { id: 'format', label: 'Championship', code: '_CHAMP' },
    { id: 'variant', label: 'Variant', code: '_VAR' },
    { id: 'field', label: 'Field', code: '_FIELD' },
    { id: 'teams', label: 'Teams', code: '_TEAM' },
    { id: 'pools', label: 'Pools', code: '_POOL' },
    { id: 'run', label: 'Run', code: '_RUN' },
  ]
  return all.filter((s) => {
    if (s.id === 'variant' && getFormat(store.formatId).variants.length === 1) return false
    if (s.id === 'teams' && !custom.value) return false
    if (s.id === 'pools' && store.variant.poolCount === 0) return false
    return true
  })
})

const availablePresets = computed(() => presetsFor(store.formatId, store.variantId, store.gender))
const fieldFull = computed(() => store.selected.length === store.variant.fieldSize)
const pickerFull = computed(() => fieldFull.value)
const unassigned = computed(() => store.selected.filter((t) => !t.poolId))
const currentStepIndex = computed(() => visibleSteps.value.findIndex((s) => s.id === step.value))
const selectionProgress = computed(() => (100 * store.selected.length) / store.variant.fieldSize)

const filteredTour = computed(() => {
  const q = query.value.trim().toLowerCase()
  const list = store.tour
  if (!q) return list.slice(0, 80)
  return list
    .filter(
      (t) =>
        t.lastName.toLowerCase().includes(q) ||
        t.firstName.toLowerCase().includes(q) ||
        t.location.toLowerCase().includes(q),
    )
    .slice(0, 80)
})

function stepIndexLabel(index: number): string {
  return String(index + 1).padStart(2, '0')
}
function displayName(team: Team): string {
  return team.firstName ? `${team.firstName} ${team.lastName}` : `Team ${team.lastName}`
}
function isSelected(id: string): boolean {
  return store.selected.some((t) => t.id === id)
}
function poolTeams(name: string): Team[] {
  return store.selected.filter((t) => t.poolId === name)
}

async function chooseFormat(formatId: FormatId, gender: Gender) {
  store.setFormat(formatId, gender)
  await store.ensureTour()
  step.value = getFormat(formatId).variants.length > 1 ? 'variant' : 'field'
}

function chooseVariant(id: VariantId) {
  store.setVariant(id)
  step.value = 'field'
}

function choosePreset(preset: Preset) {
  custom.value = false
  store.applyPreset(preset)
  step.value = store.variant.poolCount > 0 ? 'pools' : 'run'
}

function chooseCustom() {
  custom.value = true
  store.selected = []
  step.value = 'teams'
}

function afterTeams() {
  step.value = store.variant.poolCount > 0 ? 'pools' : 'run'
}

function fillTopTeams() {
  store.selected = topN(store.tour, store.variant.fieldSize).map((team) => ({ ...team, poolId: undefined }))
}

function autoAssignPools() {
  store.selected = assignSnakePools(
    [...store.selected].sort((a, b) => a.rank - b.rank),
    store.variant.poolCount,
    store.variant.poolSize,
  )
}

function backFromField() {
  step.value = getFormat(store.formatId).variants.length > 1 ? 'variant' : 'format'
}
function backFromPools() {
  step.value = custom.value ? 'teams' : 'field'
}
function backFromRun() {
  if (store.variant.poolCount > 0) step.value = 'pools'
  else if (custom.value) step.value = 'teams'
  else step.value = 'field'
}

function onDrag(id: string, ev: DragEvent) {
  ev.dataTransfer?.setData('text/plain', id)
}
function onDrop(poolId: string | undefined, ev: DragEvent) {
  const id = ev.dataTransfer?.getData('text/plain')
  if (!id) return
  if (poolId) {
    const size = store.selected.filter((t) => t.poolId === poolId).length
    const already = store.selected.find((t) => t.id === id)?.poolId === poolId
    if (!already && size >= store.variant.poolSize) return
  }
  store.setPool(id, poolId)
}

async function go() {
  store.seed = 0
  await present(store.mode)
  store.simulate()
  await router.push('/results')
  dismiss()
}

onMounted(() => {
  if (!store.tour.length) store.ensureTour()
})
</script>
