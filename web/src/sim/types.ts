export type Gender = 'men' | 'women'
export type RunMode = 'single' | 'many'
export type FormatId = 'worlds' | 'scotties' | 'brier' | 'europeans' | 'slam'
export type VariantId = 'current' | 'classic' | 'tko' | 'pools'

export type RoundId =
  | 'round-robin'
  | 'pool'
  | 'crossover'
  | 'qualification'
  | 'quarters'
  | 'page-1-2-qualifier'
  | 'page-3-4-qualifier'
  | 'page-1-2'
  | 'page-3-4'
  | 'semis'
  | 'bronze'
  | 'final'
  | 'tko'

export interface Team {
  id: string
  firstName: string
  lastName: string
  location: string
  rank: number
  flag: string
  gender: Gender
  poolId?: string
}

export interface TeamStats {
  team: Team
  rating: number
  wins: number
  losses: number
  lsdTotal: number
  poolId?: string
  madePlayoffs: boolean
  placing: number
}

export interface PlayedGame {
  homeId: string
  awayId: string
  winnerId: string
  loserId: string
  homeScore: number
  awayScore: number
  homeHammer: boolean
  homeLsd: number
  awayLsd: number
  round: RoundId
  label: string
}

export interface TournamentResult {
  stats: TeamStats[]
  games: PlayedGame[]
}

export interface ManyRunRow {
  team: Team
  winPct: number
  avgPlacing: number
  playoffPct: number
}

export interface FormatDef {
  id: FormatId
  name: string
  genders: Gender[]
  variants: VariantDef[]
}

export interface VariantDef {
  id: VariantId
  name: string
  blurb: string
  fieldSize: number
  poolCount: number
  poolSize: number
  ends: number
}
