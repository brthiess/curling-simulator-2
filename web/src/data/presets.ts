import type { FormatId, Gender, Team, VariantId } from '../sim/types'
import { isEuropean, resolveSkip, topN, uniqueBySkip } from './tour'

export interface Preset {
  id: string
  name: string
  formatId: FormatId
  variantId: VariantId
  gender: Gender
  kind: 'named' | 'top'
  skips?: { firstName?: string; lastName: string; location: string; pool?: string }[]
}

export const PRESETS: Preset[] = [
  {
    id: 'worlds-men-2026',
    name: '2026 Men’s Worlds',
    formatId: 'worlds',
    variantId: 'current',
    gender: 'men',
    kind: 'named',
    skips: [
      { lastName: 'Edin', location: 'Sweden' },
      { lastName: 'Whyte', location: 'Scotland' },
      { lastName: 'Dunstone', location: 'Manitoba' },
      { firstName: 'Marco', lastName: 'Hösli', location: 'Switzerland' },
      { lastName: 'Shuster', location: 'United States' },
      { lastName: 'Retornaz', location: 'Italy' },
      { firstName: 'Xiaoming', lastName: 'Xu', location: 'China' },
      { lastName: 'Yamaguchi', location: 'Japan' },
      { lastName: 'Muskatewitz', location: 'Germany' },
      { firstName: 'Chang-Min', lastName: 'Kim', location: 'Korea' },
      { firstName: 'Lukas', lastName: 'Klima', location: 'Czech' },
      { firstName: 'Konrad', lastName: 'Stych', location: 'Poland' },
      { firstName: 'Andreas', lastName: 'Hårstad', location: 'Norway' },
    ],
  },
  {
    id: 'worlds-women-2026',
    name: '2026 Women’s Worlds',
    formatId: 'worlds',
    variantId: 'current',
    gender: 'women',
    kind: 'named',
    skips: [
      { firstName: 'Xenia', lastName: 'Schwaller', location: 'Switzerland' },
      { lastName: 'Einarson', location: 'Manitoba' },
      { lastName: 'Fujisawa', location: 'Japan' },
      { lastName: 'Wranå', location: 'Sweden' },
      { firstName: 'Eun-ji', lastName: 'Gim', location: 'Korea' },
      { lastName: 'Yıldız', location: 'Turkey' },
      { lastName: 'Constantini', location: 'Italy' },
      { lastName: 'Strouse', location: 'United States' },
      { firstName: 'Fay', lastName: 'Henderson', location: 'Scotland' },
      { lastName: 'Dupont', location: 'Denmark' },
      { firstName: 'Rui', lastName: 'Wang', location: 'China' },
      { lastName: 'Bjørnstad', location: 'Norway' },
      { lastName: 'Williams', location: 'Australia' },
    ],
  },
  {
    id: 'brier-2026',
    name: '2026 Brier',
    formatId: 'brier',
    variantId: 'current',
    gender: 'men',
    kind: 'named',
    skips: [
      { lastName: 'Gushue', location: 'Newfoundland', pool: 'A' },
      { lastName: 'Jacobs', location: 'Canada', pool: 'A' },
      { firstName: 'Jayden', lastName: 'King', location: 'Ontario', pool: 'A' },
      { lastName: 'Ménard', location: 'Quebec', pool: 'A' },
      { lastName: 'Smith', location: 'Prince Edward', pool: 'A' },
      { lastName: 'Knapp', location: 'Saskatchewan', pool: 'A' },
      { lastName: 'Young', location: 'Newfoundland', pool: 'A' },
      { lastName: 'Thompson', location: 'Nova Scotia', pool: 'A' },
      { lastName: 'Samagalski', location: 'Nunavut', pool: 'A' },
      { firstName: 'Kevin', lastName: 'Koe', location: 'Alberta', pool: 'B' },
      { lastName: 'Dunstone', location: 'Manitoba', pool: 'B' },
      { lastName: 'Calvert', location: 'Manitoba', pool: 'B' },
      { lastName: 'McEwen', location: 'Saskatchewan', pool: 'B' },
      { lastName: 'Grattan', location: 'Brunswick', pool: 'B' },
      { firstName: 'Sandy', lastName: 'MacEwan', location: 'Northern Ontario', pool: 'B' },
      { firstName: 'Jamie', lastName: 'Koe', location: 'Territories', pool: 'B' },
      { lastName: 'Scoffin', location: 'Yukon', pool: 'B' },
      { firstName: 'Cody', lastName: 'Tanaka', location: 'Columbia', pool: 'B' },
    ],
  },
  {
    id: 'scotties-2026',
    name: '2026 Scotties',
    formatId: 'scotties',
    variantId: 'current',
    gender: 'women',
    kind: 'named',
    skips: [
      { lastName: 'Einarson', location: 'Canada', pool: 'A' },
      { lastName: 'Reese-Hansen', location: 'Columbia', pool: 'A' },
      { lastName: 'Lawes', location: 'Manitoba', pool: 'A' },
      { lastName: 'Armstrong', location: 'Ontario', pool: 'A' },
      { lastName: 'Campbell', location: 'Saskatchewan', pool: 'A' },
      { lastName: 'Stevens', location: 'Nova Scotia', pool: 'A' },
      { lastName: 'Kaufman', location: 'Territories', pool: 'A' },
      { lastName: 'Fortin', location: 'Quebec', pool: 'A' },
      { lastName: 'Scoffin', location: 'Yukon', pool: 'A' },
      { lastName: 'Skrlik', location: 'Alberta', pool: 'B' },
      { lastName: 'Black', location: 'Nova Scotia', pool: 'B' },
      { lastName: 'Sturmay', location: 'Alberta', pool: 'B' },
      { lastName: 'Peterson', location: 'Manitoba', pool: 'B' },
      { lastName: 'Scharf', location: 'Northern Ontario', pool: 'B' },
      { lastName: 'Forsythe', location: 'Brunswick', pool: 'B' },
      { lastName: 'Power', location: 'Prince Edward', pool: 'B' },
      { lastName: 'Mitchell', location: 'Newfoundland', pool: 'B' },
      { lastName: 'Weagle', location: 'Nunavut', pool: 'B' },
    ],
  },
]

export function presetsFor(formatId: FormatId, variantId: VariantId, gender: Gender): Preset[] {
  const named = PRESETS.filter(
    (p) => p.formatId === formatId && p.variantId === variantId && p.gender === gender,
  )
  return [
    ...named,
    {
      id: `top-${formatId}-${variantId}-${gender}`,
      name: 'Top-ranked on the Tour',
      formatId,
      variantId,
      gender,
      kind: 'top',
    },
  ]
}

export function buildPresetField(
  preset: Preset,
  tour: Team[],
  fieldSize: number,
  poolCount: number,
  poolSize: number,
): Team[] {
  if (preset.kind === 'top') {
    const source =
      preset.formatId === 'europeans' ? uniqueBySkip(tour.filter(isEuropean)) : tour
    const picked = topN(source, fieldSize).map((t) => ({ ...t }))
    return assignSnakePools(picked, poolCount, poolSize)
  }
  if (!preset.skips) return []
  const used = new Set<string>()
  return preset.skips.map((skip, i) => {
    const team = resolveSkip(
      tour,
      preset.gender,
      skip.lastName,
      skip.location,
      20 + i,
      used,
      skip.firstName ?? '',
    )
    used.add(team.id)
    return { ...team, poolId: skip.pool }
  })
}

export function assignSnakePools(teams: Team[], poolCount: number, poolSize: number): Team[] {
  if (poolCount <= 0) return teams.map((t) => ({ ...t, poolId: undefined }))
  const names = poolCount === 4 ? ['A', 'B', 'C', 'D'] : ['A', 'B']
  return teams.map((t, i) => {
    const row = Math.floor(i / poolCount)
    const col = i % poolCount
    const pool = names[row % 2 === 0 ? col : poolCount - 1 - col]
    return { ...t, poolId: pool }
  }).slice(0, poolCount * poolSize || teams.length)
}
