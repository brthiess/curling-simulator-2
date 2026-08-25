import { defineStore } from 'pinia'
import { getFormat, getVariant } from '../sim/catalog'
import { manyRun } from '../sim/manyRun'
import { mulberry32, randomSeed } from '../sim/rng'
import { runTournament } from '../sim/run'
import type { FormatId, Gender, ManyRunRow, RunMode, Team, TournamentResult, VariantId } from '../sim/types'
import { SNAPSHOT_AS_OF, loadTour } from '../data/tour'
import { buildPresetField, type Preset } from '../data/presets'
import type { SharePayload } from '../share'

export const useSimStore = defineStore('sim', {
  state: () => ({
    formatId: 'worlds' as FormatId,
    variantId: 'current' as VariantId,
    gender: 'men' as Gender,
    selected: [] as Team[],
    tour: [] as Team[],
    mode: 'single' as RunMode,
    iterations: 1000,
    seed: 0,
    result: null as TournamentResult | null,
    many: null as ManyRunRow[] | null,
    snapshotAsOf: SNAPSHOT_AS_OF,
    loadingTour: false,
  }),
  getters: {
    variant: (s) => getVariant(s.formatId, s.variantId),
    format: (s) => getFormat(s.formatId),
    poolNames(): string[] {
      const n = this.variant.poolCount
      if (n === 4) return ['A', 'B', 'C', 'D']
      if (n === 2) return ['A', 'B']
      return []
    },
    poolsReady(): boolean {
      if (this.variant.poolCount === 0) return true
      return this.poolNames.every(
        (name) => this.selected.filter((t) => t.poolId === name).length === this.variant.poolSize,
      )
    },
  },
  actions: {
    async ensureTour() {
      this.loadingTour = true
      this.tour = await loadTour(this.gender)
      this.loadingTour = false
    },
    setFormat(formatId: FormatId, gender: Gender) {
      this.formatId = formatId
      this.gender = gender
      const first = getFormat(formatId).variants[0]
      this.variantId = first.id
      this.selected = []
      this.result = null
      this.many = null
    },
    setVariant(variantId: VariantId) {
      this.variantId = variantId
      this.selected = []
      this.result = null
      this.many = null
    },
    applyPreset(preset: Preset) {
      const v = getVariant(this.formatId, this.variantId)
      this.selected = buildPresetField(preset, this.tour, v.fieldSize, v.poolCount, v.poolSize)
      this.result = null
      this.many = null
    },
    toggleTeam(team: Team) {
      const i = this.selected.findIndex((t) => t.id === team.id)
      if (i >= 0) {
        this.selected.splice(i, 1)
        return
      }
      if (this.selected.length >= this.variant.fieldSize) return
      this.selected.push({ ...team, poolId: undefined })
    },
    setPool(teamId: string, poolId: string | undefined) {
      const t = this.selected.find((x) => x.id === teamId)
      if (!t) return
      t.poolId = poolId
    },
    simulate() {
      this.seed = this.seed || randomSeed()
      const rng = mulberry32(this.seed)
      if (this.mode === 'single') {
        this.result = runTournament(this.formatId, this.variantId, this.selected, rng)
        this.many = null
      } else {
        this.many = manyRun(this.formatId, this.variantId, this.selected, this.seed, this.iterations)
        this.result = null
      }
    },
    sharePayload(): SharePayload {
      return {
        formatId: this.formatId,
        variantId: this.variantId,
        gender: this.gender,
        teams: this.selected,
        mode: this.mode,
        iterations: this.iterations,
        seed: this.seed,
      }
    },
    async restore(payload: SharePayload) {
      this.formatId = payload.formatId
      this.variantId = payload.variantId
      this.gender = payload.gender
      this.mode = payload.mode
      this.iterations = payload.iterations
      this.seed = payload.seed
      await this.ensureTour()
      this.selected = payload.teams
      this.simulate()
    },
  },
})
