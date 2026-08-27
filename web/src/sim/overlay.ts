import type { RunMode } from './types'

export const SINGLE_RUN_STAGES = [
  'Sweeping the ice…',
  'Round robin in progress…',
  'Playoffs underway…',
  'Stimulating Final…',
  'Last stone in the house…',
] as const

export const MANY_RUN_STAGES = [
  'Seeding thousands of draws…',
  'Replaying every end…',
  'Stimulating Final…',
  'Tallying the odds…',
  'Ranking the favorites…',
] as const

export const SIM_STAGE_DURATION_MS = 1100

export function stagesFor(mode: RunMode): readonly string[] {
  return mode === 'many' ? MANY_RUN_STAGES : SINGLE_RUN_STAGES
}

export function simStageDelayMs(): number {
  if (typeof window !== 'undefined' && window.matchMedia('(prefers-reduced-motion: reduce)').matches) {
    return 50
  }
  return SIM_STAGE_DURATION_MS
}

export function delay(ms: number): Promise<void> {
  return new Promise((resolve) => setTimeout(resolve, ms))
}

export async function playSimStages(
  stages: readonly string[],
  onStage: (message: string, index: number) => void,
  delayMs = simStageDelayMs(),
): Promise<void> {
  for (let i = 0; i < stages.length; i++) {
    onStage(stages[i]!, i)
    await delay(delayMs)
  }
}
