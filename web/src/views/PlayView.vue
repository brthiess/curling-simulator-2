<template>
  <div class="play">
    <ol class="steps">
      <li v-for="s in visibleSteps" :key="s.id" :class="{ on: step === s.id }">{{ s.label }}</li>
    </ol>

    <section v-if="step === 'format'">
      <h1 class="page-title">Pick a championship</h1>
      <div class="format-grid">
        <button
          v-for="card in formatCards"
          :key="card.key"
          class="format-card"
          type="button"
          @click="chooseFormat(card.formatId, card.gender)"
        >
          <span
            class="format-logo"
            :class="card.logo"
            :style="card.image ? { backgroundImage: `url(${asset(card.image)})` } : undefined"
          ></span>
          <span>{{ card.label }}</span>
        </button>
      </div>
    </section>

    <section v-else-if="step === 'variant'">
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

    <section v-else-if="step === 'field'">
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

    <section v-else-if="step === 'teams'">
      <h1 class="page-title">Pick teams</h1>
      <p class="hint">
        {{ store.selected.length }}/{{ store.variant.fieldSize }} · Rankings as of {{ store.snapshotAsOf }}
      </p>
      <input v-model="query" class="search" type="search" placeholder="Search skip or location" />
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

    <section v-else-if="step === 'pools'" class="pools-section">
      <h1 class="page-title">Assign pools</h1>
      <p class="hint">Drag teams into pools. Each pool needs {{ store.variant.poolSize }}.</p>
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

    <section v-else-if="step === 'run'">
      <h1 class="page-title">How to run it</h1>
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
        <button class="btn primary" type="button" @click="go">Simulate</button>
      </div>
    </section>
  </div>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import Flag from '../components/Flag.vue'
import { asset } from '../asset'
import { presetsFor, type Preset } from '../data/presets'
import { getFormat } from '../sim/catalog'
import type { FormatId, Gender, Team, VariantId } from '../sim/types'
import { useSimStore } from '../stores/sim'

type Step = 'format' | 'variant' | 'field' | 'teams' | 'pools' | 'run'
const step = ref<Step>('format')
const query = ref('')
const custom = ref(false)
const store = useSimStore()
const router = useRouter()

const formatCards = [
  { key: 'ww', formatId: 'worlds' as const, gender: 'women' as const, label: 'Women’s Worlds', logo: 'world-women', image: 'images/world-women-logo.png' },
  { key: 'wm', formatId: 'worlds' as const, gender: 'men' as const, label: 'Men’s Worlds', logo: 'world-men', image: 'images/world-men-logo.png' },
  { key: 'sc', formatId: 'scotties' as const, gender: 'women' as const, label: 'Scotties', logo: 'scotties', image: 'images/scotties-logo.png' },
  { key: 'br', formatId: 'brier' as const, gender: 'men' as const, label: 'Brier', logo: 'brier', image: 'images/brier-logo.png' },
  { key: 'ew', formatId: 'europeans' as const, gender: 'women' as const, label: 'Women’s Europeans', logo: 'europeans' },
  { key: 'em', formatId: 'europeans' as const, gender: 'men' as const, label: 'Men’s Europeans', logo: 'europeans' },
  { key: 'sw', formatId: 'slam' as const, gender: 'women' as const, label: 'Women’s Slam', logo: 'slam' },
  { key: 'sm', formatId: 'slam' as const, gender: 'men' as const, label: 'Men’s Slam', logo: 'slam' },
]

const visibleSteps = computed(() => {
  const all: { id: Step; label: string }[] = [
    { id: 'format', label: 'Championship' },
    { id: 'variant', label: 'Variant' },
    { id: 'field', label: 'Field' },
    { id: 'teams', label: 'Teams' },
    { id: 'pools', label: 'Pools' },
    { id: 'run', label: 'Run' },
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

function go() {
  store.seed = 0
  store.simulate()
  router.push('/results')
}

onMounted(() => {
  if (!store.tour.length) store.ensureTour()
})
</script>
