<template>
  <div class="shell">
    <header class="top">
      <RouterLink to="/" class="brand"><span class="brand-stone" aria-hidden="true"></span>Curling Simulator</RouterLink>
      <span class="as-of">Rankings as of {{ snapshotAsOf }}</span>
    </header>
    <main class="body">
      <RouterView v-slot="{ Component, route }">
        <Transition name="page" mode="out-in">
          <component :is="Component" :key="route.path" />
        </Transition>
      </RouterView>
    </main>
  </div>
</template>

<script setup lang="ts">
import { SNAPSHOT_AS_OF } from './data/tour'
const snapshotAsOf = SNAPSHOT_AS_OF
</script>
