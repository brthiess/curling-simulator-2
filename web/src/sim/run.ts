import { getVariant } from './catalog'
import { makeStats } from './game'
import {
  runCanada18,
  runClassic12,
  runEuropeans10,
  runEuropeans12,
  runSlamPools,
  runSlamTko,
  runWorlds,
} from './formats'
import type { FormatId, Team, TournamentResult, VariantId } from './types'
import type { Rng } from './rng'

export function runTournament(
  formatId: FormatId,
  variantId: VariantId,
  teams: Team[],
  rng: Rng,
): TournamentResult {
  const variant = getVariant(formatId, variantId)
  if (teams.length !== variant.fieldSize) {
    throw new Error(`Expected ${variant.fieldSize} teams, got ${teams.length}`)
  }
  const stats = teams.map((t) => makeStats(t, t.poolId))
  if (formatId === 'worlds') return runWorlds(stats, rng)
  if ((formatId === 'scotties' || formatId === 'brier') && variantId === 'current') {
    return runCanada18(stats, rng)
  }
  if ((formatId === 'scotties' || formatId === 'brier') && variantId === 'classic') {
    return runClassic12(stats, rng)
  }
  if (formatId === 'europeans' && variantId === 'current') return runEuropeans12(stats, rng)
  if (formatId === 'europeans' && variantId === 'classic') return runEuropeans10(stats, rng)
  if (formatId === 'slam' && variantId === 'tko') return runSlamTko(stats, rng)
  if (formatId === 'slam' && variantId === 'pools') return runSlamPools(stats, rng)
  throw new Error(`No engine for ${formatId}/${variantId}`)
}
