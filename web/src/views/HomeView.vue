<template>
  <div class="home">
    <section class="hero">
      <div class="hero-copy">
        <p class="eyebrow">Build the field. Run the draw. Crown a champion.</p>
        <h1>Curling, down to the <em>last stone.</em></h1>
        <p class="lede">
          Play out iconic championships with real teams, complete scores and every
          playoff twist—or run thousands of tournaments to find the favorite.
        </p>
        <div class="hero-actions">
          <RouterLink class="btn primary btn-large" to="/play">Build a tournament <span aria-hidden="true">→</span></RouterLink>
          <a class="text-link" href="#quick-start">Or jump straight to the ice</a>
        </div>
        <p class="fine">
          Rankings snapshot {{ asOf }} · Simulated for fun, never predicted
        </p>
      </div>
      <div class="hero-house" aria-hidden="true">
        <span class="house-ring ring-blue"></span>
        <span class="house-ring ring-white"></span>
        <span class="house-ring ring-red"></span>
        <span class="house-button"></span>
        <span class="hero-stone"><i></i></span>
      </div>
    </section>

    <section id="quick-start" class="quick-start">
      <div class="section-heading">
        <div>
          <p class="eyebrow">Quick draw</p>
          <h2>Championships ready to go</h2>
        </div>
        <RouterLink class="text-link" to="/play">See every format →</RouterLink>
      </div>
      <div class="quick-grid">
        <button
          v-for="preset in featuredPresets"
          :key="preset.id"
          class="quick-card"
          type="button"
          :disabled="quickStarting !== null"
          @click="startPreset(preset)"
        >
          <span
            class="quick-logo"
            :class="{ monogram: !logoFor(preset) }"
            :style="logoFor(preset) ? { backgroundImage: `url(${asset(logoFor(preset)!)})` } : undefined"
          >{{ logoFor(preset) ? '' : 'WCF' }}</span>
          <span class="quick-copy">
            <strong>{{ preset.name }}</strong>
            <small>{{ preset.skips?.length }} teams · Single tournament</small>
          </span>
          <span class="quick-arrow" aria-hidden="true">{{ quickStarting === preset.id ? '•••' : '→' }}</span>
        </button>
      </div>
    </section>

    <Transition name="overlay">
      <div v-if="quickStarting" class="sim-overlay" role="status" aria-live="polite">
        <div class="sim-card">
          <div class="stone-spinner" aria-hidden="true"><span></span></div>
          <p class="eyebrow">Preparing the ice</p>
          <strong>Running the draw…</strong>
        </div>
      </div>
    </Transition>
  </div>
</template>

<script setup lang="ts">
import { nextTick, ref } from 'vue'
import { useRouter } from 'vue-router'
import { asset } from '../asset'
import { PRESETS, type Preset } from '../data/presets'
import { SNAPSHOT_AS_OF } from '../data/tour'
import { useSimStore } from '../stores/sim'

const asOf = SNAPSHOT_AS_OF
const store = useSimStore()
const router = useRouter()
const quickStarting = ref<string | null>(null)
const featuredPresets = PRESETS.slice(0, 4)

function logoFor(preset: Preset): string | null {
  if (preset.formatId === 'brier') return 'images/brier-logo.png'
  if (preset.formatId === 'scotties') return 'images/scotties-logo.png'
  if (preset.formatId === 'worlds') {
    return preset.gender === 'women' ? 'images/world-women-logo.png' : 'images/world-men-logo.png'
  }
  return null
}

async function startPreset(preset: Preset) {
  quickStarting.value = preset.id
  store.setFormat(preset.formatId, preset.gender)
  store.setVariant(preset.variantId)
  await store.ensureTour()
  store.applyPreset(preset)
  store.mode = 'single'
  store.seed = 0
  await nextTick()
  await new Promise((resolve) => setTimeout(resolve, 450))
  store.simulate()
  await router.push('/results')
}
</script>
