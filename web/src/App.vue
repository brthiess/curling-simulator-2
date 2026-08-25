<template>
  <div class="shell">
    <header class="top">
      <RouterLink to="/" class="brand">Curling Simulator</RouterLink>
      <span class="as-of">Rankings as of {{ snapshotAsOf }}</span>
    </header>
    <main class="body">
      <RouterView />
    </main>
  </div>
</template>

<script setup lang="ts">
import { SNAPSHOT_AS_OF } from './data/tour'
const snapshotAsOf = SNAPSHOT_AS_OF
</script>
