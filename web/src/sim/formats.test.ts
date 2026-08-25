import { describe, expect, it } from 'vitest'
import { ratingFromRank } from './rating'
import { mulberry32 } from './rng'
import { runTournament } from './run'
import { manyRun } from './manyRun'
import type { Team } from './types'

function fakeTeams(n: number, pools?: string[]): Team[] {
  return Array.from({ length: n }, (_, i) => ({
    id: `t${i + 1}`,
    firstName: 'Skip',
    lastName: `Team${i + 1}`,
    location: 'Test',
    rank: i + 1,
    flag: 'canada.png',
    gender: 'men' as const,
    poolId: pools ? pools[i] : undefined,
  }))
}

function pools18(): string[] {
  return [...Array(9).fill('A'), ...Array(9).fill('B')]
}

function pools12euro(): string[] {
  return [...Array(6).fill('A'), ...Array(6).fill('B')]
}

function poolsSlam(): string[] {
  return ['A', 'A', 'A', 'A', 'B', 'B', 'B', 'B', 'C', 'C', 'C', 'C', 'D', 'D', 'D', 'D']
}

describe('ratingFromRank', () => {
  it('gives a higher rating to rank 1 than rank 50', () => {
    expect(ratingFromRank(1)).toBeGreaterThan(ratingFromRank(50))
    expect(ratingFromRank(1)).toBeGreaterThan(0.4)
    expect(ratingFromRank(200)).toBeGreaterThanOrEqual(0.01)
  })
})

describe('formats', () => {
  it('runs worlds with 13 teams and a champion', () => {
    const result = runTournament('worlds', 'current', fakeTeams(13), mulberry32(1))
    expect(result.stats).toHaveLength(13)
    expect(result.stats.filter((s) => s.placing === 1)).toHaveLength(1)
    expect(result.stats.filter((s) => s.madePlayoffs)).toHaveLength(6)
    expect(result.games.some((g) => g.round === 'final')).toBe(true)
    expect(result.games.some((g) => g.round === 'bronze')).toBe(true)
    expect(result.games.every((g) => g.homeScore !== g.awayScore)).toBe(true)
  })

  it('is deterministic for the same seed', () => {
    const a = runTournament('worlds', 'current', fakeTeams(13), mulberry32(42))
    const b = runTournament('worlds', 'current', fakeTeams(13), mulberry32(42))
    expect(a.games.map((g) => g.winnerId)).toEqual(b.games.map((g) => g.winnerId))
  })

  it('runs classic 12-team Page with bronze', () => {
    const result = runTournament('brier', 'classic', fakeTeams(12), mulberry32(2))
    expect(result.stats.filter((s) => s.madePlayoffs)).toHaveLength(4)
    expect(result.games.some((g) => g.round === 'bronze')).toBe(true)
  })

  it('runs current 18-team Brier without bronze', () => {
    const result = runTournament('brier', 'current', fakeTeams(18, pools18()), mulberry32(3))
    expect(result.stats.filter((s) => s.madePlayoffs)).toHaveLength(6)
    expect(result.games.some((g) => g.round === 'page-1-2-qualifier')).toBe(true)
    expect(result.games.some((g) => g.round === 'bronze')).toBe(false)
    expect(result.stats.filter((s) => s.placing === 1)).toHaveLength(1)
  })

  it('runs current Europeans 12', () => {
    const result = runTournament('europeans', 'current', fakeTeams(12, pools12euro()), mulberry32(4))
    expect(result.stats.filter((s) => s.madePlayoffs)).toHaveLength(4)
    expect(result.games.some((g) => g.round === 'bronze')).toBe(true)
  })

  it('runs classic Europeans 10', () => {
    const result = runTournament('europeans', 'classic', fakeTeams(10), mulberry32(5))
    expect(result.stats.filter((s) => s.madePlayoffs)).toHaveLength(4)
  })

  it('runs slam triple knockout to 8 playoff teams', () => {
    const result = runTournament('slam', 'tko', fakeTeams(16), mulberry32(6))
    expect(result.stats.filter((s) => s.madePlayoffs)).toHaveLength(8)
    expect(result.stats.filter((s) => s.wins === 3 && s.losses === 0).length).toBeGreaterThanOrEqual(1)
    expect(result.games.some((g) => g.round === 'quarters')).toBe(true)
  })

  it('runs slam pool play', () => {
    const result = runTournament('slam', 'pools', fakeTeams(16, poolsSlam()), mulberry32(7))
    expect(result.stats.filter((s) => s.madePlayoffs)).toHaveLength(8)
    expect(result.games.some((g) => g.round === 'crossover')).toBe(true)
  })
})

describe('manyRun', () => {
  it('gives the top-ranked team the highest win rate', () => {
    const rows = manyRun('worlds', 'current', fakeTeams(13), 99, 80)
    expect(rows[0].team.rank).toBe(1)
    expect(rows[0].winPct).toBeGreaterThan(rows[rows.length - 1].winPct)
    const sum = rows.reduce((a, r) => a + r.winPct, 0)
    expect(sum).toBeGreaterThan(99)
    expect(sum).toBeLessThan(101)
  })
})
