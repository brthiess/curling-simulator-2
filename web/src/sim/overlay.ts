import type { RunMode } from './types'

export const SINGLE_RUN_STAGES = [
  'Sweeping the ice…',
  'Round robin in progress…',
  'Playoffs underway…',
  'Simulating Final…',
  'Last stone in the house…',
] as const

export const MANY_RUN_STAGES = [
  'Seeding thousands of draws…',
  'Replaying every end…',
  'Simulating Final…',
  'Tallying the odds…',
  'Ranking the favorites…',
] as const

/** How long each theatrical stage stays on screen. */
export const SIM_STAGE_DURATION_MS = 1200

export function stagesFor(mode: RunMode): readonly string[] {
  return mode === 'many' ? MANY_RUN_STAGES : SINGLE_RUN_STAGES
}

export function delay(ms: number): Promise<void> {
  return new Promise((resolve) => setTimeout(resolve, ms))
}

export async function playSimStages(
  stages: readonly string[],
  onStage: (message: string, index: number) => void,
  delayMs: number,
): Promise<void> {
  for (let i = 0; i < stages.length; i++) {
    onStage(stages[i]!, i)
    await delay(delayMs)
  }
}
