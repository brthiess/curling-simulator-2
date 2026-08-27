import { computed, nextTick, ref } from 'vue'
import { SIM_STAGE_DURATION_MS, delay, playSimStages, stagesFor } from '../sim/overlay'
import type { RunMode } from '../sim/types'

export function useSimOverlay() {
  const active = ref(false)
  const message = ref('')
  const stageIndex = ref(0)
  const stageCount = ref(0)
  const progress = computed(() =>
    stageCount.value ? ((stageIndex.value + 1) / stageCount.value) * 100 : 0,
  )

  async function present(mode: RunMode) {
    const stages = stagesFor(mode)
    stageCount.value = stages.length
    stageIndex.value = 0
    message.value = stages[0] ?? ''
    active.value = true
    await nextTick()
    // Let the overlay paint before the first timed stage.
    await delay(80)
    await playSimStages(
      stages,
      (next, index) => {
        message.value = next
        stageIndex.value = index
      },
      SIM_STAGE_DURATION_MS,
    )
  }

  function dismiss() {
    active.value = false
  }

  return { active, message, stageIndex, stageCount, progress, present, dismiss }
}
