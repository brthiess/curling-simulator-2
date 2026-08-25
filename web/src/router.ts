import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from './views/HomeView.vue'
import PlayView from './views/PlayView.vue'
import ResultsView from './views/ResultsView.vue'

export const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    { path: '/', component: HomeView },
    { path: '/play', component: PlayView },
    { path: '/results', component: ResultsView },
    { path: '/s/:payload', component: ResultsView, props: true },
  ],
  scrollBehavior() {
    return { top: 0 }
  },
})
