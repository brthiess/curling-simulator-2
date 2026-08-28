<template>
  <div class="crt-home">
    <header class="crt-bar">
      <span class="crt-bar-title">CURLING SIMULATOR</span>
      <span class="crt-bar-marks" aria-hidden="true">
        <svg class="crt-glyph" viewBox="0 0 24 24" fill="none">
          <circle cx="12" cy="12" r="3" stroke="currentColor" stroke-width="1.6" />
          <path
            d="M12 3.5v2.2M12 18.3v2.2M3.5 12h2.2M18.3 12h2.2M6.1 6.1l1.6 1.6M16.3 16.3l1.6 1.6M17.9 6.1l-1.6 1.6M7.7 16.3l-1.6 1.6"
            stroke="currentColor"
            stroke-width="1.6"
            stroke-linecap="square"
          />
        </svg>
        <svg class="crt-glyph" viewBox="0 0 24 24" fill="none">
          <rect x="3.5" y="5" width="17" height="14" stroke="currentColor" stroke-width="1.6" />
          <path d="M7 9.5 10.2 12 7 14.5M12.5 14.5H17" stroke="currentColor" stroke-width="1.6" stroke-linecap="square" />
        </svg>
      </span>
    </header>

    <section class="crt-console">
      <span class="crt-badge crt-badge-top">[ SYSTEM_READY ]</span>
      <span class="crt-badge crt-badge-bottom">[ V1.0.84-STABLE ]</span>

      <div class="crt-console-body">
        <div class="crt-stone crt-stone-pixel" aria-hidden="true">
          <pre class="crt-glow">      ██████
    ██████████
  ██████████████
  ██████████████
    ██████████
      ██████</pre>
          <span class="crt-stone-status">HANDLE_RDY</span>
        </div>

        <div class="crt-stone crt-stone-ascii" aria-hidden="true">
          <pre class="crt-glow">      .----.
    .'      '.
   /          \
  |   (0)(0)   |
  |    \__/    |
   \          /
    '.      .'
      '----'</pre>
        </div>

        <div class="crt-copy">
          <p class="crt-prompt">&gt; BUILD THE FIELD. RUN THE DRAW. CROWN A CHAMPION.</p>
          <h1 class="crt-title crt-glow">
            <span class="crt-title-desktop">CURLING, DOWN TO<br />THE LAST STONE.</span>
            <span class="crt-title-mobile">CURLING, DOWN TO THE LAST STONE</span>
          </h1>
          <p class="crt-lede crt-lede-desktop">
            A technical simulator for tournament dynamics and win probability. Execute massive
            brackets, track ice conditions, and analyze team variants in a zero-tolerance
            computational environment.
          </p>
          <p class="crt-lede crt-lede-mobile">
            [SYS.MSG] Initialize simulation parameters. Awaiting user input to construct tournament matrix.
          </p>

          <div class="crt-cta-wrap">
            <RouterLink class="crt-cta" to="/play">
              <span class="crt-play" aria-hidden="true"></span>
              [ BUILD A TOURNAMENT ]
            </RouterLink>
          </div>

          <div class="crt-meta">
            <p>
              <svg class="crt-glyph crt-glyph-sm" viewBox="0 0 24 24" fill="none" aria-hidden="true">
                <circle cx="12" cy="12" r="8.25" stroke="currentColor" stroke-width="1.6" />
                <path d="M12 10.5V16.5M12 7.6h.01" stroke="currentColor" stroke-width="1.8" stroke-linecap="square" />
              </svg>
              Rankings snapshot {{ asOf }}
            </p>
            <p>Simulated for fun, never predicted.</p>
          </div>
        </div>
      </div>

      <div class="crt-seq">
        <span>SEQ: 001</span>
        <span>MEM: 640K OK</span>
      </div>
    </section>

    <div class="crt-status">
      <div class="crt-status-cell">
        <svg class="crt-glyph" viewBox="0 0 24 24" fill="none" aria-hidden="true">
          <rect x="4" y="7" width="16" height="10" stroke="currentColor" stroke-width="1.6" />
          <path d="M8 7V5M12 7V4M16 7V5M8 17v2M12 17v3M16 17v2" stroke="currentColor" stroke-width="1.6" stroke-linecap="square" />
        </svg>
        <span>SYS: ONLINE</span>
      </div>
      <div class="crt-status-cell">
        <svg class="crt-glyph" viewBox="0 0 24 24" fill="none" aria-hidden="true">
          <ellipse cx="12" cy="7" rx="7" ry="2.4" stroke="currentColor" stroke-width="1.6" />
          <path d="M5 7v5c0 1.3 3.1 2.4 7 2.4s7-1.1 7-2.4V7M5 12v5c0 1.3 3.1 2.4 7 2.4s7-1.1 7-2.4v-5" stroke="currentColor" stroke-width="1.6" />
        </svg>
        <span>DB: SYNCED</span>
      </div>
    </div>

    <footer class="crt-foot">v1.0.84-STABLE | (C) 1988 CURLING_SYS</footer>
  </div>
</template>

<script setup lang="ts">
import { SNAPSHOT_AS_OF } from '../data/tour'

const asOf = SNAPSHOT_AS_OF
</script>
