<template>
  <Transition name="overlay">
    <div v-if="active" class="sim-overlay" role="status" aria-live="polite">
      <div class="sim-card">
        <div class="stone-spinner" aria-hidden="true"><span></span></div>
        <p class="eyebrow">{{ eyebrow }}</p>
        <Transition name="sim-msg" mode="out-in">
          <strong :key="message">{{ message }}</strong>
        </Transition>
        <div class="sim-progress" aria-hidden="true">
          <span :style="{ width: `${progress}%` }"></span>
        </div>
        <small>{{ stageIndex + 1 }} of {{ stageCount }}{{ detail ? ` · ${detail}` : '' }}</small>
      </div>
    </div>
  </Transition>
</template>

<script setup lang="ts">
defineProps<{
  active: boolean
  eyebrow: string
  message: string
  progress: number
  stageIndex: number
  stageCount: number
  detail?: string
}>()
</script>
