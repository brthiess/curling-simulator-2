import {
  loserOf,
  playGame,
  ranked,
  roundRobin,
  setPlacings,
  sortStandings,
  winnerOf,
} from './game'
import type { PlayedGame, TeamStats, TournamentResult } from './types'
import type { Rng } from './rng'

const PLAYOFF = { countLsd: false, countRecord: false, hammerToBetterRecord: true }

function pagePlayoff(
  top4: TeamStats[],
  rng: Rng,
  games: PlayedGame[],
  includeBronze: boolean,
): void {
  const [first, second, third, fourth] = top4
  const oneTwo = playGame(first, second, rng, 'page-1-2', 'Page 1v2', PLAYOFF)
  const threeFour = playGame(third, fourth, rng, 'page-3-4', 'Page 3v4', PLAYOFF)
  games.push(oneTwo, threeFour)
  const semi = playGame(
    loserOf(oneTwo, top4),
    winnerOf(threeFour, top4),
    rng,
    'semis',
    'Semifinal',
    PLAYOFF,
  )
  games.push(semi)
  const final = playGame(
    winnerOf(oneTwo, top4),
    winnerOf(semi, top4),
    rng,
    'final',
    'Final',
    PLAYOFF,
  )
  games.push(final)
  winnerOf(final, top4).placing = 1
  loserOf(final, top4).placing = 2
  loserOf(semi, top4).placing = 3
  if (includeBronze) {
    const bronze = playGame(
      loserOf(semi, top4),
      loserOf(threeFour, top4),
      rng,
      'bronze',
      'Bronze',
      PLAYOFF,
    )
    games.push(bronze)
    winnerOf(bronze, top4).placing = 3
    loserOf(bronze, top4).placing = 4
  } else {
    loserOf(threeFour, top4).placing = 4
  }
}

export function runWorlds(stats: TeamStats[], rng: Rng): TournamentResult {
  const games: PlayedGame[] = []
  roundRobin(stats, rng, 'round-robin', 'Round robin', games)
  const order = sortStandings(stats)
  const top6 = order.slice(0, 6)
  top6.forEach((s) => {
    s.madePlayoffs = true
  })
  setPlacings(order.slice(6), 7)

  const q1 = playGame(top6[2], top6[5], rng, 'qualification', '3 vs 6', PLAYOFF)
  const q2 = playGame(top6[3], top6[4], rng, 'qualification', '4 vs 5', PLAYOFF)
  games.push(q1, q2)
  loserOf(q1, top6).placing = 5
  loserOf(q2, top6).placing = 6

  const s1 = playGame(top6[0], winnerOf(q1, top6), rng, 'semis', 'Semifinal', PLAYOFF)
  const s2 = playGame(top6[1], winnerOf(q2, top6), rng, 'semis', 'Semifinal', PLAYOFF)
  games.push(s1, s2)

  const bronze = playGame(loserOf(s1, top6), loserOf(s2, top6), rng, 'bronze', 'Bronze', PLAYOFF)
  const final = playGame(winnerOf(s1, top6), winnerOf(s2, top6), rng, 'final', 'Final', PLAYOFF)
  games.push(bronze, final)
  winnerOf(final, top6).placing = 1
  loserOf(final, top6).placing = 2
  winnerOf(bronze, top6).placing = 3
  loserOf(bronze, top6).placing = 4
  return { stats, games }
}

export function runClassic12(stats: TeamStats[], rng: Rng): TournamentResult {
  const games: PlayedGame[] = []
  roundRobin(stats, rng, 'round-robin', 'Round robin', games)
  const order = sortStandings(stats)
  const top4 = order.slice(0, 4)
  top4.forEach((s) => {
    s.madePlayoffs = true
  })
  setPlacings(order.slice(4), 5)
  pagePlayoff(top4, rng, games, true)
  return { stats, games }
}

export function runCanada18(stats: TeamStats[], rng: Rng): TournamentResult {
  const games: PlayedGame[] = []
  const poolA = stats.filter((s) => s.poolId === 'A')
  const poolB = stats.filter((s) => s.poolId === 'B')
  if (poolA.length !== 9 || poolB.length !== 9) {
    throw new Error('Current Scotties/Brier needs two pools of 9')
  }
  roundRobin(poolA, rng, 'pool', 'Pool A', games)
  roundRobin(poolB, rng, 'pool', 'Pool B', games)
  const a = ranked(poolA)
  const b = ranked(poolB)
  const playoff = [a[0], a[1], a[2], b[0], b[1], b[2]]
  playoff.forEach((s) => {
    s.madePlayoffs = true
  })
  const rest = sortStandings(stats.filter((s) => !s.madePlayoffs))
  setPlacings(rest, 7)

  const q12a = playGame(a[0], b[1], rng, 'page-1-2-qualifier', 'A1 vs B2', PLAYOFF)
  const q12b = playGame(b[0], a[1], rng, 'page-1-2-qualifier', 'B1 vs A2', PLAYOFF)
  games.push(q12a, q12b)
  const q34a = playGame(loserOf(q12a, stats), a[2], rng, 'page-3-4-qualifier', 'Loser A1/B2 vs A3', PLAYOFF)
  const q34b = playGame(loserOf(q12b, stats), b[2], rng, 'page-3-4-qualifier', 'Loser B1/A2 vs B3', PLAYOFF)
  games.push(q34a, q34b)
  loserOf(q34a, stats).placing = 6
  loserOf(q34b, stats).placing = 5

  pagePlayoff(
    [
      winnerOf(q12a, stats),
      winnerOf(q12b, stats),
      winnerOf(q34a, stats),
      winnerOf(q34b, stats),
    ],
    rng,
    games,
    false,
  )
  return { stats, games }
}

export function runEuropeans12(stats: TeamStats[], rng: Rng): TournamentResult {
  const games: PlayedGame[] = []
  const poolA = stats.filter((s) => s.poolId === 'A')
  const poolB = stats.filter((s) => s.poolId === 'B')
  if (poolA.length !== 6 || poolB.length !== 6) {
    throw new Error('Current Europeans needs two pools of 6')
  }
  roundRobin(poolA, rng, 'pool', 'Pool A', games)
  roundRobin(poolB, rng, 'pool', 'Pool B', games)
  const a = ranked(poolA)
  const b = ranked(poolB)
  const four = [a[0], a[1], b[0], b[1]]
  four.forEach((s) => {
    s.madePlayoffs = true
  })
  setPlacings(sortStandings(stats.filter((s) => !s.madePlayoffs)), 5)

  const s1 = playGame(a[0], b[1], rng, 'semis', 'A1 vs B2', PLAYOFF)
  const s2 = playGame(b[0], a[1], rng, 'semis', 'B1 vs A2', PLAYOFF)
  games.push(s1, s2)
  const bronze = playGame(loserOf(s1, stats), loserOf(s2, stats), rng, 'bronze', 'Bronze', PLAYOFF)
  const final = playGame(winnerOf(s1, stats), winnerOf(s2, stats), rng, 'final', 'Final', PLAYOFF)
  games.push(bronze, final)
  winnerOf(final, stats).placing = 1
  loserOf(final, stats).placing = 2
  winnerOf(bronze, stats).placing = 3
  loserOf(bronze, stats).placing = 4
  return { stats, games }
}

export function runEuropeans10(stats: TeamStats[], rng: Rng): TournamentResult {
  const games: PlayedGame[] = []
  roundRobin(stats, rng, 'round-robin', 'Round robin', games)
  const order = sortStandings(stats)
  const top4 = order.slice(0, 4)
  top4.forEach((s) => {
    s.madePlayoffs = true
  })
  setPlacings(order.slice(4), 5)
  const s1 = playGame(top4[0], top4[3], rng, 'semis', '1 vs 4', PLAYOFF)
  const s2 = playGame(top4[1], top4[2], rng, 'semis', '2 vs 3', PLAYOFF)
  games.push(s1, s2)
  const bronze = playGame(loserOf(s1, top4), loserOf(s2, top4), rng, 'bronze', 'Bronze', PLAYOFF)
  const final = playGame(winnerOf(s1, top4), winnerOf(s2, top4), rng, 'final', 'Final', PLAYOFF)
  games.push(bronze, final)
  winnerOf(final, top4).placing = 1
  loserOf(final, top4).placing = 2
  winnerOf(bronze, top4).placing = 3
  loserOf(bronze, top4).placing = 4
  return { stats, games }
}

export function runSlamTko(stats: TeamStats[], rng: Rng): TournamentResult {
  const games: PlayedGame[] = []
  if (stats.length !== 16) throw new Error('Triple knockout needs 16 teams')
  const seed = new Map(stats.map((s, i) => [s.team.id, i]))
  const active = [...stats]
  const qualified: TeamStats[] = []

  const key = (s: TeamStats) => `${s.wins}-${s.losses}`
  while (qualified.length < 8) {
    const groups = new Map<string, TeamStats[]>()
    for (const s of active) {
      if (s.wins >= 3 || s.losses >= 3) continue
      const list = groups.get(key(s)) ?? []
      list.push(s)
      groups.set(key(s), list)
    }
    let played = false
    for (const group of groups.values()) {
      group.sort((a, b) => (seed.get(a.team.id) ?? 0) - (seed.get(b.team.id) ?? 0))
      for (let i = 0; i + 1 < group.length; i += 2) {
        games.push(playGame(group[i], group[i + 1], rng, 'tko', `${group[i].wins}-${group[i].losses}`))
        played = true
      }
    }
    for (const s of active) {
      if (s.wins >= 3 && !qualified.includes(s)) {
        s.madePlayoffs = true
        qualified.push(s)
      }
    }
    if (!played) break
  }

  const eliminated = sortStandings(stats.filter((s) => !s.madePlayoffs))
  setPlacings(eliminated, 9)

  const qfSeeds = sortStandings(qualified)
  const qf: PlayedGame[] = []
  const pairs: [number, number][] = [
    [0, 7],
    [3, 4],
    [1, 6],
    [2, 5],
  ]
  for (const [i, j] of pairs) {
    qf.push(playGame(qfSeeds[i], qfSeeds[j], rng, 'quarters', 'Quarterfinal', PLAYOFF))
  }
  games.push(...qf)
  const sf1 = playGame(winnerOf(qf[0], stats), winnerOf(qf[1], stats), rng, 'semis', 'Semifinal', PLAYOFF)
  const sf2 = playGame(winnerOf(qf[2], stats), winnerOf(qf[3], stats), rng, 'semis', 'Semifinal', PLAYOFF)
  games.push(sf1, sf2)
  const final = playGame(winnerOf(sf1, stats), winnerOf(sf2, stats), rng, 'final', 'Final', PLAYOFF)
  games.push(final)
  winnerOf(final, stats).placing = 1
  loserOf(final, stats).placing = 2
  loserOf(sf1, stats).placing = 3
  loserOf(sf2, stats).placing = 4
  const qfLosers = sortStandings(qf.map((g) => loserOf(g, stats)))
  setPlacings(qfLosers, 5)
  return { stats, games }
}

export function runSlamPools(stats: TeamStats[], rng: Rng): TournamentResult {
  const games: PlayedGame[] = []
  const pools = ['A', 'B', 'C', 'D'].map((id) => stats.filter((s) => s.poolId === id))
  if (pools.some((p) => p.length !== 4)) throw new Error('Pool Slam needs four pools of 4')
  for (const pool of pools) {
    roundRobin(pool, rng, 'pool', `Pool ${pool[0].poolId}`, games)
  }
  const crossPairs: [string, string][] = [
    ['A', 'D'],
    ['B', 'C'],
  ]
  for (const [left, right] of crossPairs) {
    const l = ranked(stats.filter((s) => s.poolId === left))
    const r = ranked(stats.filter((s) => s.poolId === right))
    games.push(playGame(l[0], r[3], rng, 'crossover', `${left}1 vs ${right}4`))
    games.push(playGame(l[1], r[2], rng, 'crossover', `${left}2 vs ${right}3`))
    games.push(playGame(r[0], l[3], rng, 'crossover', `${right}1 vs ${left}4`))
    games.push(playGame(r[1], l[2], rng, 'crossover', `${right}2 vs ${left}3`))
  }
  const table = sortStandings(stats)
  const qfSeeds = table.slice(0, 8)
  qfSeeds.forEach((s) => {
    s.madePlayoffs = true
  })
  setPlacings(table.slice(8), 9)
  const qf = [
    playGame(qfSeeds[0], qfSeeds[7], rng, 'quarters', 'Quarterfinal', PLAYOFF),
    playGame(qfSeeds[3], qfSeeds[4], rng, 'quarters', 'Quarterfinal', PLAYOFF),
    playGame(qfSeeds[1], qfSeeds[6], rng, 'quarters', 'Quarterfinal', PLAYOFF),
    playGame(qfSeeds[2], qfSeeds[5], rng, 'quarters', 'Quarterfinal', PLAYOFF),
  ]
  games.push(...qf)
  const sf1 = playGame(winnerOf(qf[0], stats), winnerOf(qf[1], stats), rng, 'semis', 'Semifinal', PLAYOFF)
  const sf2 = playGame(winnerOf(qf[2], stats), winnerOf(qf[3], stats), rng, 'semis', 'Semifinal', PLAYOFF)
  games.push(sf1, sf2)
  const final = playGame(winnerOf(sf1, stats), winnerOf(sf2, stats), rng, 'final', 'Final', PLAYOFF)
  games.push(final)
  winnerOf(final, stats).placing = 1
  loserOf(final, stats).placing = 2
  loserOf(sf1, stats).placing = 3
  loserOf(sf2, stats).placing = 4
  setPlacings(sortStandings(qf.map((g) => loserOf(g, stats))), 5)
  return { stats, games }
}
