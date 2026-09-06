# Gladiator Arena

A top-down 2D arena in which the player faces endless waves of the environment
acting as the enemy — no conventional AI, no pathfinding. Inspired by the
bullet-hell/danmaku genre and games like Vampire Survivors: you survive by
reading telegraphed threats and moving riskily, not through character power.

A solo project, currently in active development (WIP). The visuals are pure
placeholder for now (primitive shapes, no sprites) — development is focused on
game systems and logic rather than art.

## Project status

**Done:**
- WASD movement with arena bounds (elliptical arena, clamping)
- Dash-roll (stamina-gated dodge)
- Stamina system
- Four enemy types:
  - **Walker** — a simple pass across the map
  - **Archer** — moves along a circular path, aims and fires at the player's position with a telegraph
  - **Wizard** — two spell types (the "Armageddon" area blast and the "Firewall" formation of fireballs)
  - **Mortar** — throws telegraphed fireballs around the player for the entire run
- HP system with invincibility frames per damage type
- UI: health, stamina, score timer

**In progress / TODO:**
- Hook swing and parry (only a prepared state in the movement system so far, no functionality)
- More enemy types (Legionaries, Cannibal, Horseman — designed in DESIGN.md)
- Menus, art (currently placeholder sprites and primitives)
- Difficulty and spawn system balancing

The design document with a full breakdown of the mechanics and enemies is in
[DESIGN.md](DESIGN.md).

## Tech

- Unity 6 (Universal Render Pipeline, 2D)
- C#
- Legacy Input Manager

## Running the project

1. Clone the repo
2. Open the `no-name-game/` folder in Unity Hub (requires Unity `6000.5.2f1` or newer Unity 6)
3. Open the scene `Assets/Scenes/ArenaScene.unity` and hit Play

## License

MIT — see [LICENSE](LICENSE)