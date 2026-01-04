# Testing

| Test number | Test description | Test data | Expected output | Actual results |
|------------:|------------------|-----------|------------------|----------------|
| 1 | Menu shows all 4 options on startup | Launch program | Menu displays: NEW GAME / RESUME GAME / READ INSTRUCTIONS / QUIT | Menu displayed all four options correctly |
| 2 | Menu highlight moves and selects correctly | Use Up Arrow/ Down Arrow then Enter | Highlight moves up and down and Enter selects option | Highlight moved correctly and selected option on Enter |
| 3 | New Game starts and shows boat placement on an 8×8 grid | Select EW GAME | 8×8 grid displayed for placing boats | 8×8 grid shown and boat placement began |
| 4 | User can place 2 Destroyers without overlap | Place two destroyers | Two single-cell destroyers placed without overlap | Destroyers placed correctly with no overlap |
| 5 | User can place Submarines and rotate with R | Place submarine, press R | Submarine occupies 2 cells; rotation changes orientation | Submarine placed correctly and rotation worked |
| 6 | User can place Carrier and rotate with R | Place carrier, press R | Carrier occupies 3 cells; rotation changes orientation | Carrier placed correctly with valid rotation |
| 7 | Boat placement prevented from going off-grid | Try placing near grid edge | Program prevents invalid placement | Off-grid placement was prevented |
| 8 | Target tracker starts blank | Reach first shooting screen | Target tracker is 8×8 and all `~` | Target tracker initialised correctly |
| 9 | User selects target using highlighted cursor | Arrow keys + Enter | Cursor highlights cell; Enter fires | Cursor selection worked without text input |
| 10 | Miss feedback updates target tracker | Shoot empty cell | Target tracker shows `M` | Miss correctly marked with `M` |
| 11 | Hit and sunk feedback updates target tracker | Sink a ship | Hits show `H`; sunk ship shows `#` | Hits and sunk ships displayed correctly |
| 12 | User cannot shoot the same cell twice | Attempt to re-select used cell | Only empty cells selectable | Previously shot cells could not be selected |
| 13 | Computer chooses an empty cell each turn | Allow multiple computer turns | Computer never shoots same cell twice | Computer only targeted empty cells |
| 14 | Computer targets adjacent cells after a hit | Allow computer to hit ship | Adjacent cells targeted next | Computer correctly targeted adjacent cells |
| 15 | Game saves and resumes correctly | Restart program → RESUME GAME | Game state loads and continues | Game resumed with correct grids and turn |
| 16 | Resume with no save file starts new game | Delete save file, resume | New game starts with message | New game started and message shown |
| 17 | Game ends and winner is displayed | Sink all ships | “You Won!” or “You Lost!” shown | End message displayed and game stopped |
