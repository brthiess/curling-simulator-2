import type { FormatDef, FormatId, VariantDef, VariantId } from './types'

export const FORMATS: FormatDef[] = [
  {
    id: 'worlds',
    name: 'Worlds',
    genders: ['men', 'women'],
    variants: [
      {
        id: 'current',
        name: 'Current (13-team)',
        blurb: 'Round robin, top 6. 1 and 2 bye to semis; 3v6 and 4v5; bronze and gold.',
        fieldSize: 13,
        poolCount: 0,
        poolSize: 0,
        ends: 10,
      },
    ],
  },
  {
    id: 'scotties',
    name: 'Scotties',
    genders: ['women'],
    variants: [
      {
        id: 'current',
        name: 'Current (18-team)',
        blurb: 'Two pools of 9. Top 3, qualifier games, then Page playoff. No bronze.',
        fieldSize: 18,
        poolCount: 2,
        poolSize: 9,
        ends: 10,
      },
      {
        id: 'classic',
        name: 'Classic (12-team Page)',
        blurb: 'Full round robin. Top 4 Page 1v2 / 3v4, semi, final, and bronze.',
        fieldSize: 12,
        poolCount: 0,
        poolSize: 0,
        ends: 10,
      },
    ],
  },
  {
    id: 'brier',
    name: 'Brier',
    genders: ['men'],
    variants: [
      {
        id: 'current',
        name: 'Current (18-team)',
        blurb: 'Two pools of 9. Top 3, qualifier games, then Page playoff. No bronze.',
        fieldSize: 18,
        poolCount: 2,
        poolSize: 9,
        ends: 10,
      },
      {
        id: 'classic',
        name: 'Classic (12-team Page)',
        blurb: 'Full round robin. Top 4 Page 1v2 / 3v4, semi, final, and bronze.',
        fieldSize: 12,
        poolCount: 0,
        poolSize: 0,
        ends: 10,
      },
    ],
  },
  {
    id: 'europeans',
    name: 'Europeans',
    genders: ['men', 'women'],
    variants: [
      {
        id: 'current',
        name: 'Current (12-team, 8 ends)',
        blurb: 'Two pools of 6. A1vB2 and B1vA2 semis, bronze and gold.',
        fieldSize: 12,
        poolCount: 2,
        poolSize: 6,
        ends: 8,
      },
      {
        id: 'classic',
        name: 'Classic (10-team)',
        blurb: 'Full round robin. Top 4: 1v4 and 2v3 semis, bronze and gold.',
        fieldSize: 10,
        poolCount: 0,
        poolSize: 0,
        ends: 10,
      },
    ],
  },
  {
    id: 'slam',
    name: 'Slam',
    genders: ['men', 'women'],
    variants: [
      {
        id: 'tko',
        name: 'Triple knockout',
        blurb: '16 teams. Win 3 before losing 3. 2 A / 3 B / 3 C into quarters.',
        fieldSize: 16,
        poolCount: 0,
        poolSize: 0,
        ends: 8,
      },
      {
        id: 'pools',
        name: 'Pool play',
        blurb: 'Four pools of 4, one crossover, top 8 into quarters.',
        fieldSize: 16,
        poolCount: 4,
        poolSize: 4,
        ends: 8,
      },
    ],
  },
]

export function getFormat(id: FormatId): FormatDef {
  const found = FORMATS.find((f) => f.id === id)
  if (!found) throw new Error(`Unknown format ${id}`)
  return found
}

export function getVariant(formatId: FormatId, variantId: VariantId): VariantDef {
  const found = getFormat(formatId).variants.find((v) => v.id === variantId)
  if (!found) throw new Error(`Unknown variant ${formatId}/${variantId}`)
  return found
}

