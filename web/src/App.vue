<template>
  <div class="shell" :class="{ 'theme-crt': isHome }">
    <header v-if="!isHome" class="top">
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
import { computed, onUnmounted, watch } from 'vue'
import { useRoute } from 'vue-router'
import { SNAPSHOT_AS_OF } from './data/tour'

const route = useRoute()
const snapshotAsOf = SNAPSHOT_AS_OF
const isHome = computed(() => route.path === '/')

watch(isHome, (home) => {
  document.documentElement.classList.toggle('theme-crt', home)
}, { immediate: true })

onUnmounted(() => {
  document.documentElement.classList.remove('theme-crt')
})
</script>
