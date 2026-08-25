import { ratingFromRank } from './rating'
import { intInRange, type Rng } from './rng'
import type { PlayedGame, RoundId, Team, TeamStats } from './types'

export function makeStats(team: Team, poolId?: string): TeamStats {
  return {
    team,
    rating: ratingFromRank(team.rank),
    wins: 0,
    losses: 0,
    lsdTotal: 0,
    poolId,
    madePlayoffs: false,
    placing: 0,
  }
}

function drawLsd(rating: number, rng: Rng): number {
  const roll = intInRange(rng, 1, 200)
  let lsd = 1000 / (Math.sqrt(rating) * roll * Math.sqrt(rating)) - 5.7
  if (lsd < 0) lsd = 0
  if (lsd > 144) lsd = 144
  return lsd
}

function homeWinProbability(home: TeamStats, away: TeamStats, homeHammer: boolean): number {
  const h = home.rating
  const a = away.rating
  const initial = ((1 - a) * h) / ((1 - a) * h + (1 - h) * a)
  return initial + (homeHammer ? 0.05 : -0.05)
}

/** Plausible final score given a winner. Favorites blow out more; upsets stay close. */
export function sampleScore(
  winner: TeamStats,
  loser: TeamStats,
  rng: Rng,
): { winner: number; loser: number } {
  const gap = winner.rating - loser.rating
  const close = gap < 0.06 || rng() < 0.28
  if (close) {
    const low = rng() < 0.45
    if (low) {
      const w = 5 + intInRange(rng, 0, 3)
      return { winner: w, loser: w - 1 }
    }
    const w = 6 + intInRange(rng, 0, 3)
    return { winner: w, loser: w - 2 }
  }
  const w = 7 + intInRange(rng, 0, 4)
  let l = 2 + intInRange(rng, 0, 4)
  if (l >= w) l = w - 2
  return { winner: w, loser: Math.max(1, l) }
}

export function playGame(
  home: TeamStats,
  away: TeamStats,
  rng: Rng,
  round: RoundId,
  label: string,
  options?: { hammerToBetterRecord?: boolean; countLsd?: boolean; countRecord?: boolean },
): PlayedGame {
  const countLsd = options?.countLsd ?? true
  const countRecord = options?.countRecord ?? true

  const homeLsd = drawLsd(home.rating, rng)
  const awayLsd = drawLsd(away.rating, rng)
  if (countLsd) {
    home.lsdTotal += homeLsd
    away.lsdTotal += awayLsd
  }

  let homeHammer: boolean
  if (options?.hammerToBetterRecord && home.wins !== away.wins) {
    homeHammer = home.wins > away.wins
  } else {
    homeHammer = homeLsd <= awayLsd
  }

  const homeWinsGame = rng() < homeWinProbability(home, away, homeHammer)
  const winner = homeWinsGame ? home : away
  const loser = homeWinsGame ? away : home
  const score = sampleScore(winner, loser, rng)

  if (countRecord) {
    winner.wins += 1
    loser.losses += 1
  }

  const homeScore = homeWinsGame ? score.winner : score.loser
  const awayScore = homeWinsGame ? score.loser : score.winner

  return {
    homeId: home.team.id,
    awayId: away.team.id,
    winnerId: winner.team.id,
    loserId: loser.team.id,
    homeScore,
    awayScore,
    homeHammer,
    homeLsd,
    awayLsd,
    round,
    label,
  }
}

export function byId(stats: TeamStats[]): Map<string, TeamStats> {
  return new Map(stats.map((s) => [s.team.id, s]))
}

export function sortStandings(stats: TeamStats[]): TeamStats[] {
  return [...stats].sort((a, b) => b.wins - a.wins || a.lsdTotal - b.lsdTotal)
}

export function roundRobin(
  group: TeamStats[],
  rng: Rng,
  round: RoundId,
  label: string,
  games: PlayedGame[],
): void {
  for (let i = 0; i < group.length; i++) {
    for (let j = i + 1; j < group.length; j++) {
      games.push(playGame(group[j], group[i], rng, round, label))
    }
  }
}

export function ranked(stats: TeamStats[], n?: number): TeamStats[] {
  const sorted = sortStandings(stats)
  return n === undefined ? sorted : sorted.slice(0, n)
}

export function setPlacings(ordered: TeamStats[], start = 1): void {
  ordered.forEach((s, i) => {
    s.placing = start + i
  })
}

export function winnerOf(game: PlayedGame, stats: TeamStats[]): TeamStats {
  const found = stats.find((s) => s.team.id === game.winnerId)
  if (!found) throw new Error('winner missing')
  return found
}

export function loserOf(game: PlayedGame, stats: TeamStats[]): TeamStats {
  const found = stats.find((s) => s.team.id === game.loserId)
  if (!found) throw new Error('loser missing')
  return found
}
