import type { FormatId, Gender, RunMode, Team, VariantId } from './sim/types'

export interface SharePayload {
  formatId: FormatId
  variantId: VariantId
  gender: Gender
  teams: Team[]
  mode: RunMode
  iterations: number
  seed: number
}

export function encodeShare(payload: SharePayload): string {
  return btoa(unescape(encodeURIComponent(JSON.stringify(payload))))
    .replace(/\+/g, '-')
    .replace(/\//g, '_')
    .replace(/=+$/, '')
}

export function decodeShare(raw: string): SharePayload {
  let b64 = raw.replace(/-/g, '+').replace(/_/g, '/')
  while (b64.length % 4) b64 += '='
  return JSON.parse(decodeURIComponent(escape(atob(b64)))) as SharePayload
}
