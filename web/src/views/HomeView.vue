<template>
  <section class="hero">
    <h1>Curling Simulator</h1>
    <p class="lede">
      Load a championship field — or build your own from the Tour — then run it once
      for the full draw, or a thousand times for the odds.
    </p>
    <RouterLink class="btn primary" to="/play">Begin</RouterLink>
    <p class="fine">
      Strength comes from World Curling Team Rankings (snapshot {{ asOf }}).
      Games are simulated, not predicted.
    </p>
  </section>
</template>

<script setup lang="ts">
import { SNAPSHOT_AS_OF } from '../data/tour'
const asOf = SNAPSHOT_AS_OF
</script>
