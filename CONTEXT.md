# Curling Simulator

A browser app for playing out curling championships: load or build a Field, Run it once or many times, and inspect Games or aggregate Placings.

## Language

**Tournament**:
A championship instance: a Format plus a Field. Not a single Game, and not the whole product.
_Avoid_: Simulation, event

**Format**:
The championship series a Tournament belongs to. Worlds, Scotties, Brier, Europeans, and Slam are Formats. A Format has one or more Format Variants.
_Avoid_: TournamentType

**Format Variant**:
A specific field size, pool layout, and playoff shape for a Format (for example current 18-team Brier vs classic 12-team Page).
_Avoid_: Edition (an edition is men's or women's), year (a year is a Preset's Field, not the rules)

**Worlds**:
The World Men's or Women's Championship Format.

**Scotties**:
Canada's women's national championship Format.

**Brier**:
Canada's men's national championship Format.

**Europeans**:
The European Curling Championships Format.

**Slam**:
A Grand Slam of Curling Format.

**Field**:
The Teams entered in a Tournament. Either loaded from a Preset or assembled as a Custom Field.
_Avoid_: lineup, roster (a roster is the players on a Team)

**Preset**:
A named championship Field the app can load without picking Teams one by one.
_Avoid_: automatic tournament

**Custom Field**:
A Field of Tour Teams chosen by the user. They need not have qualified for that championship in real life.
_Avoid_: invented team, fantasy roster

**Tour**:
The World Curling Tour catalog of real Teams and their Rankings. Every Field is drawn from the Tour.
_Avoid_: ranking list (that is a file, not the catalog)

**Team**:
A real Tour rink, identified by its skip. Strength is its real Ranking. There are no invented Teams.
_Avoid_: custom-created team, fictional team, user-set rating

**Pool**:
A subset of the Field that plays a round robin among itself. Scotties and Brier use Pools; Worlds does not.
_Avoid_: Division

**Run**:
One complete playing of a Tournament, from first Game to a final Placing for every Team.
_Avoid_: Simulation

**Single Run**:
One Run, after which the user can inspect every Game.

**Many-Run**:
Many independent Runs of the same Tournament. Results show how often each Team won and each Team's average Placing.
_Avoid_: Monte Carlo, batch, odds (odds is a view of a Many-Run, not the thing itself)

**Game**:
A match between two Teams, with Hammer, a winner, and a final score.
_Avoid_: end-by-end scoreboard (a Game does not show ends)

**Placing**:
A Team's final rank in a Run. 1 is the champion.
_Avoid_: Ranking (a Ranking is tour strength, not a finish)

**Ranking**:
A Team's standing on the world or tour list, used to derive strength.
_Avoid_: Placing

**Hammer**:
Last-stone advantage in a Game.

**LSD**:
Last Stone Draw. The measurement that awards Hammer and breaks round-robin ties.
