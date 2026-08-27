import { describe, expect, it, vi } from 'vitest'
import { MANY_RUN_STAGES, SINGLE_RUN_STAGES, playSimStages, stagesFor } from './overlay'

describe('simulation overlay stages', () => {
  it('uses distinct stage lists for single and many runs', () => {
    expect(stagesFor('single')).toEqual([...SINGLE_RUN_STAGES])
    expect(stagesFor('many')).toEqual([...MANY_RUN_STAGES])
  })

  it('includes the fake final stage in both modes', () => {
    expect(SINGLE_RUN_STAGES).toContain('Stimulating Final…')
    expect(MANY_RUN_STAGES).toContain('Stimulating Final…')
  })

  it('plays every stage in order and waits between them', async () => {
    vi.useFakeTimers()
    const seen: string[] = []
    const done = playSimStages(['One…', 'Two…', 'Stimulating Final…'], (message) => seen.push(message), 1000)
    expect(seen).toEqual(['One…'])
    await vi.advanceTimersByTimeAsync(1000)
    expect(seen).toEqual(['One…', 'Two…'])
    await vi.advanceTimersByTimeAsync(1000)
    expect(seen).toEqual(['One…', 'Two…', 'Stimulating Final…'])
    await vi.advanceTimersByTimeAsync(1000)
    await done
    vi.useRealTimers()
  })
})
