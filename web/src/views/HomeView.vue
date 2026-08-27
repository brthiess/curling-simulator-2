<template>
  <div class="home">
    <section class="hero">
      <div class="hero-copy">
        <p class="eyebrow">Build the field. Run the draw. Crown a champion.</p>
        <h1>Curling, down to the <em>last stone.</em></h1>
        <p class="lede">
          Play out iconic championships with real teams, complete scores and every
          playoff twist—or run thousands of tournaments to find the favorite.
        </p>
        <div class="hero-actions">
          <RouterLink class="btn primary btn-large" to="/play">Build a tournament <span aria-hidden="true">→</span></RouterLink>
        </div>
        <p class="fine">
          Rankings snapshot {{ asOf }} · Simulated for fun, never predicted
        </p>
      </div>
      <div class="hero-house" aria-hidden="true">
        <span class="house-ring ring-blue"></span>
        <span class="house-ring ring-white"></span>
        <span class="house-ring ring-red"></span>
        <span class="house-button"></span>
        <span class="hero-stone"><i></i></span>
      </div>
    </section>
  </div>
</template>

<script setup lang="ts">
import { SNAPSHOT_AS_OF } from '../data/tour'

const asOf = SNAPSHOT_AS_OF
</script>
