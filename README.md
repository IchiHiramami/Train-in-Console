
# Metro Manila Commuter App

This application is designed to assist users with navigating the Metro Manila rail systems (MRT-3, LRT-1, LRT-2). The app provides features such as station searches, route planning, and landmark information.

## Features

1. **Main Menu**: The app starts by displaying a menu with the following options:
   - 1: Route planning (plan a route between stations)
   - 2: Landmark search (search for stations and their associated landmarks)
   - 3: View all stations (shows all stations across all train lines)
   - 4: Exit the program

2. **Route Planning**: Users can plan routes between two stations. The app displays the route options and transfer points.

3. **Landmark Search**: Users can search for stations by name. The app returns the station's landmarks. It handles fuzzy matching, returning close matches if an exact match isn't found.

4. **All Stations Display**: A full list of all stations, categorized by train line, is shown when users select option 3.

5. **Transfer Points**: The app includes logic to display transfer points between the different MRT and LRT lines (such as Taft Avenue between MRT-3 and LRT-1, Cubao between MRT-3 and LRT-2, etc.)

6. **User Interface**: The app uses the console for interaction, with clear color-coded outputs for readability and organization.

## Code Changes and Updates

### Transfer Logic

The app includes transfer points between the three major Metro Manila train lines:

- **Taft Station (MRT-3)** connects to **EDSA Station (LRT-1)**
- **Araneta Center Cubao** is a transfer point for both MRT-3 and LRT-2
- **Doroteo Jose (LRT-1)** and **Recto (LRT-2)** are transfer stations between LRT-1 and LRT-2.

### Station and Landmark Search

The app allows users to search for a station and view its corresponding landmarks.

### Console Layout

- The console window is set to a custom size, optimized for 1440p resolution:
  ```csharp
  Console.SetWindowSize(140, 40);
  Console.SetBufferSize(140, 40);
  ```
- This ensures the console window is large enough to display information but doesn't take up the full screen.

### Mock Map Display

A mock ASCII-style map representation of the Metro Manila train system is generated, showing the relative position of stations, including LRT-1, LRT-2, and MRT-3, with lines connecting them.

Example output of the train system map (simplified):
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

### Updates to Console Output

- The output now displays station names in color for improved readability, using yellow for the main station and white for landmarks.
- Added highlights for stations in different train lines, as per user request.

## Setup

1. Clone or download the repository.
2. Open the project in Visual Studio or your preferred C# editor.
3. Build and run the application.
4. The program will launch the console-based Metro Manila commuter app.

Enjoy navigating the Metro Manila train system!
