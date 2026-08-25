<template>
  <span class="flag" :title="team.location">
    <img :src="src" :alt="team.location" />
  </span>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { asset } from '../asset'
import type { Team } from '../sim/types'

const props = defineProps<{ team: Pick<Team, 'location' | 'flag'> }>()
const src = computed(() => asset(`images/locations/${props.team.flag}`))
</script>
