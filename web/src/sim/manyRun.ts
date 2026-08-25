import { mulberry32 } from './rng'
import { runTournament } from './run'
import type { FormatId, ManyRunRow, Team, VariantId } from './types'

export function manyRun(
  formatId: FormatId,
  variantId: VariantId,
  teams: Team[],
  seed: number,
  iterations: number,
): ManyRunRow[] {
  const wins = new Map<string, number>()
  const placingSum = new Map<string, number>()
  const playoffs = new Map<string, number>()
  for (const t of teams) {
    wins.set(t.id, 0)
    placingSum.set(t.id, 0)
    playoffs.set(t.id, 0)
  }
  const rng = mulberry32(seed)
  for (let i = 0; i < iterations; i++) {
    const result = runTournament(formatId, variantId, teams, rng)
    for (const s of result.stats) {
      placingSum.set(s.team.id, (placingSum.get(s.team.id) ?? 0) + s.placing)
      if (s.placing === 1) wins.set(s.team.id, (wins.get(s.team.id) ?? 0) + 1)
      if (s.madePlayoffs) playoffs.set(s.team.id, (playoffs.get(s.team.id) ?? 0) + 1)
    }
  }
  return teams
    .map((team) => ({
      team,
      winPct: (100 * (wins.get(team.id) ?? 0)) / iterations,
      avgPlacing: (placingSum.get(team.id) ?? 0) / iterations,
      playoffPct: (100 * (playoffs.get(team.id) ?? 0)) / iterations,
    }))
    .sort((a, b) => b.winPct - a.winPct || a.avgPlacing - b.avgPlacing)
}
