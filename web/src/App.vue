<template>
  <div class="shell theme-crt">
    <header v-if="!isHome" class="crt-appbar">
      <div class="crt-appbar-inner">
        <RouterLink to="/" class="crt-appbar-brand">
          <span class="crt-appbar-mark" aria-hidden="true">.X.</span>
          CURLING SIMULATOR
        </RouterLink>
        <span class="crt-appbar-tools" aria-hidden="true">
          <span class="crt-appbar-tool">
            <svg class="crt-glyph" viewBox="0 0 24 24" fill="none">
              <circle cx="12" cy="12" r="3" stroke="currentColor" stroke-width="1.6" />
              <path
                d="M12 3.5v2.2M12 18.3v2.2M3.5 12h2.2M18.3 12h2.2M6.1 6.1l1.6 1.6M16.3 16.3l1.6 1.6M17.9 6.1l-1.6 1.6M7.7 16.3l-1.6 1.6"
                stroke="currentColor"
                stroke-width="1.6"
                stroke-linecap="square"
              />
            </svg>
          </span>
          <span class="crt-appbar-tool">
            <svg class="crt-glyph" viewBox="0 0 24 24" fill="none">
              <rect x="3.5" y="5" width="17" height="14" stroke="currentColor" stroke-width="1.6" />
              <path d="M7 9.5 10.2 12 7 14.5M12.5 14.5H17" stroke="currentColor" stroke-width="1.6" stroke-linecap="square" />
            </svg>
          </span>
        </span>
      </div>
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
import { computed, onUnmounted } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const isHome = computed(() => route.path === '/')

document.documentElement.classList.add('theme-crt')

onUnmounted(() => {
  document.documentElement.classList.remove('theme-crt')
})
</script>
