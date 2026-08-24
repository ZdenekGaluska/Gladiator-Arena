# Gladiator Arena

Top-down 2D aréna, ve které hráč čelí nekonečným vlnám prostředí, které funguje
jako nepřítel — bez klasické AI a pathfindingu. Inspirováno bullet-hell/danmaku
žánrem a hrami jako Vampire Survivors: přežíváš čtením telegrafovaných hrozeb
a rizikovým pohybem, ne silou postavy.

Sólo projekt, aktuálně v aktivním vývoji (WIP). Vizuál je zatím čistě
placeholder (primitivní tvary, žádné sprity) — vývoj se zatím soustředí na
herní systémy a logiku, ne na art.

## Stav projektu

**Hotovo:**
- WASD pohyb s hranicí arény (eliptická aréna, clamping)
- Dash-roll (stamina-gated úhyb)
- Systém staminy
- Čtyři typy nepřátel:
  - **Walker** — jednoduchý průchod mapou
  - **Archer** — pohyb po kruhové dráze, zaměří a vystřelí na pozici hráče s telegrafem
  - **Wizard** — dva typy kouzel (plošný "Armagedon" výbuch, "Firewall" formace ohnivých koulí)
  - **Mortar** — po celou dobu runu vrhá telegrafované fireballs do okolí hráče
- HP systém s invincibility frames podle typu poškození
- UI: zdraví, stamina, časovač skóre

**Rozpracováno / TODO:**
- Hook swing a parry (zatím jen připravený stav v pohybovém systému, bez funkčnosti)
- Další typy nepřátel (Legionaries, Cannibal, Horseman — navrženo v DESIGN.md)
- Menu, art (aktuálně placeholder sprity a primitiva)
- Vyvážení obtížnosti a spawn systému

Návrhový dokument s kompletním rozborem mechanik a nepřátel je v [DESIGN.md](DESIGN.md).

## Technologie

- Unity 6 (Universal Render Pipeline, 2D)
- C#
- Legacy Input Manager

## Spuštění

1. Naklonuj repo
2. Otevři složku `no-name-game/` v Unity Hubu (vyžaduje Unity `6000.5.2f1` nebo novější Unity 6)
3. Otevři scénu `Assets/Scenes/ArenaScene.unity` a spusť Play

## Licence

MIT — viz [LICENSE](LICENSE)
