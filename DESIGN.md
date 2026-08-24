# Design dokument — Gladiator Arena

*Pracovní návrhový dokument. Hra je ve vývoji, návrh se průběžně upravuje podle
toho, co se v playtestu ukáže jako funkční nebo nefunkční.*

## Elevator pitch

Top-down aréna, kde jsi gladiátor v nekonečném přežívacím souboji proti vlnám
prostředí-jako-nepřítele (bullet hell + environmentální hazardy), ne proti
chytrým bojovníkům. Vyhráváš tempem, čtením telegrafů a rizikovým pohybem —
ne silou postavy.

## Design pilíře

*(každá nová feature se musí vejít alespoň do jednoho z nich)*

1. **Prostředí je nepřítel, ne AI** — hazardy reagují na pozici hráče při
   spawnu, nemají trvalé sledování ani pathfinding.
2. **Pohyb je zdroj i riziko** — jeden stamina pool gatuje všechny abilities
   i útok.
3. **Čitelnost před komplexitou** — každý hazard musí mít jasný telegraf
   a jasnou counteru.
4. **Skill nad výbavou** — žádný gear/power creep, jen zvládnutí mechanik.

## Nepřátelé (implementováno)

| Nepřítel | Chování | Role |
|---|---|---|
| **Walker** | Spawne na okraji, projde mapou přímočaře, zmizí | Základní tlak, výplň prostoru |
| **Archer** | Pohyb po kruhové dráze kolem arény, v intervalu se zastaví, zamíří a s telegrafem vystřelí na aktuální pozici hráče | Nutí hráče číst timing, ne jen pozici |
| **Wizard** | Zjeví se kdekoli v aréně, po telegrafu vybuchne v okruhu, pak se pohybuje a náhodně střídá dvě kouzla — plošný "Armagedon" (salva fireballů na pozici hráče) a "Firewall" (postupně budovaná stěna fireballů) | První nepřítel, co dočasně uzamyká část arény — zdroj tlaku, ne jen pohybu |
| **Mortar** | Existuje po celou dobu runu, v pravidelném intervalu hází fireball s parabolickou dráhou a telegrafem (warning kruh) na náhodnou pozici v okolí hráče | Konstantní pozadí tlaku, na které hráč musí reagovat i mezi vlnami ostatních nepřátel |

### Budoucí nepřátelé (navrženo, zatím neimplementováno)

| Nepřítel | Zamýšlené chování | Role |
|---|---|---|
| **Legionaries** | Formace legionářů drží pozici kolmo k hráči a pomalu k němu postupuje. Poražen je zásahem velitele zezadu (šíp nebo swing) — po jeho pádu formace postupně rozpadá a prchá | První "poraziteľný" nepřítel; test mechaniky "využij nepřítele proti nepříteli" |
| **Cannibal** | Generuje na mapě maso, ke kterému se pohybuje a sní ho — má interní počítadlo (count), které tím roste. Hráč může maso předem otrávit, což count sníží. Existuje po fixní dobu, pak zmizí. Plánováno více variant s opačnou závislostí rychlosti/velikosti na countu | Reaktivní nepřítel, kde se strategie mění podle konkrétní varianty |
| **Horseman and chums** | Jezdec se zrychlujícím se rozběhem směrem k hráči, doprovázený pomalu bloudícími "chums". Při dostatečné rychlosti cestou zabíjí vlastní chums a tím zrychluje. Cílem hráče je nasměrovat ho tak, aby zabil dostatek chums a odešel | Nepřítel, který nutí plánovat pozice ostatních jednotek dopředu |

Detailní rozbor variant a otevřených otázek k těmto třem je v interním
design backlogu — tady je jen zamýšlený koncept, ne finální specifikace.


## Nástroje hráče

- **WASD pohyb** — základ, zdarma
- **Dash-roll** (10 staminy) — implementováno. Krátký, levný, univerzální únik.
- **Hook swing** (30 staminy) — navrženo, zatím neimplementováno.
  Momentum-based swing na sloupy v aréně.
- **Parry** — navrženo, zatím neimplementováno. Vysoce riskantní, přesný
  timing, jistá pojistka proti špatné pozici.

## Vizuál

Aktuálně čistě placeholder — primitivní tvary (kruhy, čtverce), žádné
sprity ani animace. Je to vědomé pořadí priorit, ne přehlédnutí: nejdřív
musí sedět herní systémy a balancing (nepřátelé, stamina ekonomika,
obtížnost), teprve pak má smysl investovat čas do artu, který by se jinak
musel předělávat s každou změnou mechanik. Art je poslední bod plánu níže.

## Non-goals (co vědomě neděláme)

- Level/upgrade systém — cílem je pure skill reactive gameplay
- Chytrá AI s pathfindingem
- Narativní vrstva / postavy s dialogy
- Multiplayer

## Plán priorit

1. Doladit stávající nepřátele (vyřešit rovnovážný "safe zone" v aréně)
2. Přidat 1–2 komplexnější nepřátele, než se začne na hook/parry
3. UI a menu polish
4. Art — nahradit placeholder tvary skutečnou grafikou

## Core loop

Čti telegrafy → pozicuj se / uhýbej → v okně příležitosti použij mobility
tool na reset staminy nebo zisk pozice → přežij další vlnu.

---

## Otevřené designové otázky

Design dokument bez otevřených otázek by lhal — tohle je pracovní seznam
věcí, které vím, že ještě nejsou vyřešené, a vědomě je neřeším předčasně.
Menší výběr z delšího interního seznamu, zaměřený na systémová rozhodnutí,
ne na drobnosti.

**Stamina ekonomika**
- Hook stojí 3× víc než dash a nedává žádnou obranu během letu — proč by ho
  hráč používal, pokud má swing být jednou z hlavních mechanik?
- Při ceně 5 staminy z poolu 100 je parry prakticky zadarmo (20 pokusů na
  plný pool) — jak se zajistí, aby nebyl defaultní odpovědí na všechno?
- Existuje delay mezi utracením staminy a začátkem regenerace, nebo regeneruje
  okamžitě?
- Co se stane při nule staminy — je to nebezpečný stav (burnout), nebo jen
  chvíle bez možností?

**Parry**
- Co konkrétně se stane, když parry selže — plný damage, nebo odstupňovaný
  trest podle toho, jak moc hráč netrefil timing?
- Co všechno jde parrovat a jak to hráč pozná dopředu (vizuálně/zvukově),
  než se to naučí zpaměti?
- Odráží parry projektil zpátky (může zabít nepřítele), nebo ho jen ruší?

**Scoring**
- Skóre je aktuálně "sekundy přežito + graze bonus" — to odměňuje pasivní
  hru (nedělej nic riskantního a jen přežívej). Jak scoring přepsat tak, aby
  odměňoval riziko, ne únik?
- Je graze jediný způsob, jak si skóre zrychlit, a nepovede to k
  degenerativnímu "poletování těsně u nepřátel" bez skutečného rizika?

**Roster a obtížnost**
- Legionaries, Cannibal a Horseman (navržení, neimplementovaní) mají každý
  jedno "správné řešení" — neodporuje to záměru, aby hráč vybíral z možností
  podle situace, ne řešil pevný postup?
- Existuje garance, že hráč má vždy nějakou únikovou cestu, a jak se to ověří
  — kódem, nebo jen ručním testováním?
- Mohou nepřátelé zabíjet jeden druhého (např. Fireball od Wizarda zabije
  Walkera)? Systémové pravidlo, nebo pouze u konkrétních dvojic?

**Aréna a čitelnost**
- Je někde v aréně místo, kde se dá stát dlouho bezpečně (safe zone), a pokud
  ano, jak se to systémově zruší?
- Jak se řeší situace, kdy se překryje víc telegrafů najednou — je systém
  telegrafů dost čitelný i v hustotě?

**Rozsah projektu**
- Je cílem hru vydat, nebo je to primárně učební projekt? Odpověď mění, kolik
  nepřátel a systémů má smysl dotahovat do konce.
- Jaká metrika by řekla, že hra NENÍ repetitivní (délka runů, kdy lidi
  přestanou hrát)?

---

*Tento dokument je průběžně aktualizovaný. Aktuální implementační stav viz
[README.md](README.md).*
