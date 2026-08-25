import { readFileSync } from 'node:fs'
import { describe, expect, it } from 'vitest'
import { buildPresetField, PRESETS } from '../data/presets'
import type { Gender, Team } from '../sim/types'

function load(gender: Gender): Team[] {
  const file =
    gender === 'men'
      ? 'public/data/rankings-worlds-men.json'
      : 'public/data/rankings-worlds-women.json'
  const raw = JSON.parse(readFileSync(file, 'utf8')) as {
    id: number
    rank: number
    firstName: string
    lastName: string
    location: string
    countryImage?: string
  }[]
  const prefix = gender === 'men' ? 'm' : 'w'
  return raw.map((row) => ({
    id: `${prefix}-${row.id}`,
    firstName: row.firstName,
    lastName: row.lastName,
    location: row.location,
    rank: row.rank,
    flag: row.countryImage || 'canada.png',
    gender,
  }))
}

describe('2026 presets', () => {
  it('builds an 18-team Brier without borrowing the wrong King or Tanaka', () => {
    const preset = PRESETS.find((p) => p.id === 'brier-2026')
    if (!preset) throw new Error('missing preset')
    const field = buildPresetField(preset, load('men'), 18, 2, 9)
    expect(field).toHaveLength(18)
    const names = field.map((t) => `${t.firstName} ${t.lastName}`)
    expect(names.some((n) => n.includes('Jamie King'))).toBe(false)
    expect(names.some((n) => n.includes('Shun Tanaka'))).toBe(false)
    expect(names.some((n) => n.includes('Jayden') || n.includes('King'))).toBe(true)
    expect(field.filter((t) => t.poolId === 'A')).toHaveLength(9)
    expect(field.filter((t) => t.poolId === 'B')).toHaveLength(9)
  })

  it('builds a 13-team Worlds field', () => {
    const preset = PRESETS.find((p) => p.id === 'worlds-men-2026')
    if (!preset) throw new Error('missing preset')
    const field = buildPresetField(preset, load('men'), 13, 0, 0)
    expect(field).toHaveLength(13)
    expect(field.some((t) => t.lastName === 'Edin')).toBe(true)
  })
})
