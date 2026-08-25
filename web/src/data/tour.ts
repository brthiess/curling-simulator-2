import { asset } from '../asset'
import type { Gender, Team } from '../sim/types'

interface RawRanking {
  id?: number
  rank: number
  firstName: string
  lastName: string
  location: string
  countryImage?: string
  provinceImage?: string
  image?: string
}

export const SNAPSHOT_AS_OF = 'March 2020'

export async function loadTour(gender: Gender): Promise<Team[]> {
  const file = gender === 'men' ? 'rankings-worlds-men.json' : 'rankings-worlds-women.json'
  const res = await fetch(asset(`data/${file}`))
  const raw = (await res.json()) as RawRanking[]
  const prefix = gender === 'men' ? 'm' : 'w'
  return raw.map((row, index) => ({
    id: `${prefix}-${row.id ?? index + 1}`,
    firstName: row.firstName,
    lastName: row.lastName,
    location: row.location,
    rank: row.rank,
    flag: row.countryImage || row.image || row.provinceImage || 'canada.png',
    gender,
  }))
}

function fold(s: string): string {
  return s
    .normalize('NFD')
    .replace(/\p{M}/gu, '')
    .toLowerCase()
}

const CAN_HINTS = [
  'ontario',
  'alberta',
  'manitoba',
  'saskatchewan',
  'quebec',
  'columbia',
  'nova scotia',
  'brunswick',
  'edward',
  'newfoundland',
  'labrador',
  'yukon',
  'nunavut',
  'territor',
  'canada',
]

function isCanadian(loc: string): boolean {
  const x = fold(loc)
  return CAN_HINTS.some((h) => x.includes(h))
}

function locationMatches(teamLoc: string, hint: string): boolean {
  const a = fold(teamLoc)
  const b = fold(hint)
  if (a.includes(b) || b.includes(a)) return true
  if (b.includes('canada') && isCanadian(teamLoc)) return true
  return false
}

export function findTeam(
  tour: Team[],
  lastName: string,
  location?: string,
  exclude: Set<string> = new Set(),
  firstName?: string,
): Team | undefined {
  const last = fold(lastName)
  const first = firstName ? fold(firstName) : ''
  let matches = tour.filter((t) => fold(t.lastName) === last && !exclude.has(t.id))
  if (first) {
    const named = matches.filter((t) => fold(t.firstName).includes(first) || first.includes(fold(t.firstName)))
    if (named.length) matches = named
    else return undefined
  }
  if (location) {
    const withLoc = matches.filter((t) => locationMatches(t.location, location))
    if (withLoc.length) return withLoc.sort((a, b) => a.rank - b.rank)[0]
    return undefined
  }
  return matches.sort((a, b) => a.rank - b.rank)[0]
}

export function resolveSkip(
  tour: Team[],
  gender: Gender,
  lastName: string,
  location: string,
  fallbackRank: number,
  exclude: Set<string> = new Set(),
  firstName = '',
): Team {
  const found = findTeam(tour, lastName, location, exclude, firstName || undefined)
  if (found) return { ...found }
  return {
    id: `x-${gender}-${firstName}-${lastName}-${location}`.toLowerCase().replace(/\s+/g, '-'),
    firstName,
    lastName,
    location,
    rank: fallbackRank,
    flag: flagForLocation(location),
    gender,
  }
}

const FLAGS: Record<string, string> = {
  sweden: 'sweden.png',
  scotland: 'scotland.png',
  canada: 'canada.png',
  switzerland: 'switzerland.png',
  'united states': 'states.png',
  usa: 'states.png',
  italy: 'italy.png',
  china: 'china.png',
  japan: 'japan.png',
  germany: 'germany.png',
  korea: 'korea.png',
  'south korea': 'korea.png',
  czechia: 'czech-republic.png',
  'czech republic': 'czech-republic.png',
  poland: 'poland.png',
  norway: 'norway.png',
  denmark: 'denmark.png',
  turkey: 'turkey.png',
  australia: 'australia.png',
  netherlands: 'netherlands.png',
  austria: 'austria.png',
  belgium: 'belgium.png',
  spain: 'spain.png',
  finland: 'finland.png',
  estonia: 'estonia.png',
  latvia: 'latvia.png',
  lithuania: 'lithuania.png',
  england: 'england.png',
  france: 'france.png',
  ontario: 'canada.png',
  alberta: 'canada.png',
  manitoba: 'canada.png',
  saskatchewan: 'canada.png',
  quebec: 'canada.png',
  'british columbia': 'canada.png',
  'nova scotia': 'canada.png',
  'new brunswick': 'canada.png',
  'prince edward island': 'canada.png',
  'newfoundland': 'canada.png',
  'northern ontario': 'canada.png',
  yukon: 'canada.png',
  nunavut: 'canada.png',
  'northwest territories': 'canada.png',
}

export function flagForLocation(location: string): string {
  const key = location.toLowerCase()
  for (const [needle, file] of Object.entries(FLAGS)) {
    if (key.includes(needle)) return file
  }
  return 'canada.png'
}

const EUROPE = [
  'sweden',
  'scotland',
  'switzerland',
  'italy',
  'germany',
  'norway',
  'czech',
  'poland',
  'netherlands',
  'austria',
  'belgium',
  'spain',
  'denmark',
  'finland',
  'estonia',
  'latvia',
  'lithuania',
  'england',
  'france',
  'turkey',
  'hungary',
  'ireland',
  'wales',
]

export function isEuropean(team: Team): boolean {
  const loc = team.location.toLowerCase()
  return EUROPE.some((c) => loc.includes(c))
}

export function topN(tour: Team[], n: number): Team[] {
  return [...tour].sort((a, b) => a.rank - b.rank).slice(0, n)
}

export function uniqueBySkip(teams: Team[]): Team[] {
  const seen = new Set<string>()
  const out: Team[] = []
  for (const t of teams) {
    const key = t.lastName.toLowerCase() + t.location.toLowerCase()
    if (seen.has(key)) continue
    seen.add(key)
    out.push(t)
  }
  return out
}
