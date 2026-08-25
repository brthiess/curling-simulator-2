/** Inverse-regression curve from the original C# sim. Rank 1 is strongest. */
export function ratingFromRank(rank: number): number {
  const rating = (-1833.15 / (-1.05558 * rank - 23.703) + 12.144) / 100
  if (rating < 0.01) return 0.01
  if (rating >= 1) return 0.99
  return rating
}
