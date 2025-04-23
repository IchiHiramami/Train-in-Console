
# Code Changes and Updates

## v0.0.3-alpha

### Issues Fixed
- identified root cause of `MRT-3` and `LRT-2` being unable to transfer lines. Caused due to both lines having the transfer points as `cubao` instead of `Araneta-Center Cubao` for `LRT-2`.

## v0.0.2-alpha

### Station and Landmark Search

The app now allows users to search for a station and view its corresponding landmarks. Redid the landmark-to-station search function and finally added the station-to-landmark function

### Mock Map Display

A mock ASCII-style map representation of the Metro Manila train system is generated, showing the relative position of stations, including LRT-1, LRT-2, and MRT-3, with lines connecting them.

Example output of the train system map:
```
                                             [Antipolo]
                                                |
                                        [Marikina-Pasig]
                                                 |
                                            [Santolan]
                                                  |
                                            [Katipunan]
                                                  |
                                             [Anonas]=======
                                                  |     |||
         North Ave -- Quezon Ave -- GMA Kamuning -- Araneta Cubao ---------- Santolan-Annapolis ---------- Ortigas ----------- Shaw ----------- Boni -----------|
                                                  |                                                                                                             |
[FPJ]                                   [Betty-Go Belmonte]                                                                                                 [Buendia]
   |                                              |                                                                                                             |
[Balintawak]                                  [Gilmore]                                                                                                         |
   |                                              |                                                                                                             |
   |                                          [J. Ruiz]                                                                                                      [Ayala]
[Monumento]                                       |                                                                                                             |
   |                                          [V. Mapa]                                                                                                         |
   |                                              |                                                                                                             |
[5th Avenue]                                   [Pureza]                                                                                                   [Magallanes]
   |                                              |                                                                                                             |
   |                                          [Legarda]                                                                                                         |
[R. Papa]                                         |                                                                                                             |
   |                                           [Recto]                                                                                                    [Taft Avenue]
   |                                             |||                                                                                                          |||
A. Santos -- Blumentritt -- Tayuman -- Bambang -- Doroteo Jose -- Carriedo -- Central -- UN  -- Pedro Gil -- Quirino -- Vito Cruz -- Gil Puyat -- Libertad -- EDSA -- Baclaran -- Redemptorist
```

### Code Refactoring

 - Moved permanent functions such as `InitializeTrainLines()` to external C# classes for easier reading of `Program.cs`

### Updates to Console Output and other QOL updates

 - Additional context guide added
 - Removed comments in the code itself

### Issues

1. Lines `MRT-3` and `LRT-2` are currently unable to make a transfer.

## v0.0.1-alpha-test

### Transfer Logic

- Implemented transfer points between the three major Metro Manila train lines:
- 
```- **Taft Station (MRT-3)** connects to **EDSA Station (LRT-1)**

- **Araneta Center Cubao** is a transfer point for both MRT-3 and LRT-2 [currently debugging]
- **Doroteo Jose (LRT-1)** and **Recto (LRT-2)** are transfer stations between LRT-1 and LRT-2.

```


### "Smart" search functionality

 - Simulated a smart system that allows users to still obtain their necessary information even if they have typographical errors when typing in `landmarks` and `station` names.

### Creation of UI-Progress Bar

 - Added a simulated progress bar for the ride Progress.

### UI Enhancements

 - Implemented Basic UI for Readability

### Issues

1. Lines `MRT-3` and `LRT-2` are currently unable to make a transfer.
2. ASCII Map is too massive to be displayed in full in the terminal
3. Program.cs is unreadable when debugging due to inconsistencies in naming and logic.
