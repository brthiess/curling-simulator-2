# Curling Simulator

A browser app for playing out curling championships: load or build a Field, Run it once or many times, and inspect Games or aggregate Placings.

## Language

**Tournament**:
A championship instance: a Format plus a Field. Not a single Game, and not the whole product.
_Avoid_: Simulation, event

**Format**:
The structure of a championship — field size, pools, round robin, and playoff shape. Worlds, Scotties, Brier, Europeans, and Slam are Formats.
_Avoid_: TournamentType

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
A Field the user assembles, including Teams that are not on the ranking list.

**Team**:
A competing entry, usually named for its skip. Strength comes from a Ranking or a user-set rating.
_Avoid_: rink, country (a Team is a skip's rink, not always a nation)

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
A match between two Teams.

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
