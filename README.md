# Curling Simulator

Browser app for playing out curling championships. Load a preset field or pick teams from the World Curling Tour, then run the event once (standings, games, scores) or many times (win rate and average placing).

## Run locally

```bash
cd web
npm install
npm test
npm run dev
```

## Rankings

Team strength comes from a frozen World Curling Team Rankings snapshot (`web/public/data/`, currently dated March 2020). Replace those JSON files and update `SNAPSHOT_AS_OF` in `web/src/data/tour.ts` when you refresh.

## Deploy

Push to `main`. GitHub Actions builds the Vue app and publishes `web/dist` to the `gh-pages` branch.
