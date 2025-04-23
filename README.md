# Manila Train-App In A Console

### Option 1: Display Train Map

This option shows the map of all the Metro Manila train lines.

- **Simulated Response**:

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

Press any key to return
```


### Option 2: Plan Your Commute

This option allows users to plan their commute by specifying a starting station/landmark and a destination.

- **Simulated Response**:
    ```
    Where are you starting from? (station or landmark)
    [User Input]: "Taft Station"
    
    Where is your destination? (station or landmark)
    [User Input]: "Recto Station"
    
    Calculating route from Taft Station to Recto Station...
    
    Travel directly from Taft Station to Recto Station on the LRT 1 line.
    ```

### Option 3: Find Landmark or Station

This option allows users to search for landmarks or stations. It includes the following sub-options:

1. **Search station near a landmark**
2. **Show all landmarks near a station**
3. **Show list of all stations per line**
4. **Go back to main menu**

#### Sub-option 1: Search Station Near a Landmark

- **Simulated Response**:
    ```
    Enter a landmark to search: SM Mall of Asia
    
    Possible matches:
    - SM Mall of Asia
    Press any key to return...
    ```

#### Sub-option 2: Show All Landmarks Near a Station

- **Simulated Response**:
    ```
    Enter a station to list its landmarks: Taft Station
    
    Landmarks near Taft Station on LRT 1:
    - Mall of Asia
    - SMX Convention Center
    Press any key to return...
    ```

#### Sub-option 3: Show List of All Stations per Line

- **Simulated Response**:
    ```
    Stations per train line:
    ---------------------------
    LRT 1 Line:
    - Baclaran                 - EDSA
    - Libertad                 - Taft Station
    (etc.)

    Press any key to return...
    ```

#### Sub-option 4: Go Back to Main Menu

- **Simulated Response**:
    ```
    Returning to the main menu...
    ```

---

## Route Calculation

When planning a commute, the app will calculate the route between the origin and destination.

1. If the start and destination are on the same train line, the app will show the route directly between the two stations.
2. If the start and destination are on different lines, the app will find the nearest transfer points and suggest the best route, including the stations to transfer at.

- **Simulated Response**:
    ```
    Calculating route from Taft Station to Recto Station...
    
    Travel directly from Taft Station to Recto Station on the LRT 1 line.
    ```

---

## Transfer Points

If your commute involves transferring between different train lines, the app will show the transfer points.

- **Simulated Response**:
    ```
    Take LRT 1 from Taft Station to Araneta Center Cubao, then transfer to MRT 3 at Araneta Center Cubao to reach Recto Station.
    ```

---

## Station and Landmark Search

When you search for a station or landmark, the app will attempt to find matching locations. If no exact match is found, it will suggest similar locations based on fuzzy search algorithms.

- **Simulated Response**:
    ```
    No exact matches found. Did you mean one of these?
    1. Taft Station
    2. Taft Avenue
    Please select a number from the suggestions (1 to N):
    [User Input]: 1
    
    You selected Taft Station.
    ```

---

## Ride Progress

Once you begin your ride, the app will simulate your travel through the stations, showing your current station, passed stations, and upcoming stations. A progress bar will indicate your ride progress.

- **Simulated Response**:
    ```
    Ride Progress:
    [**Taft Station** -> Mall of Asia -> SMX Convention Center]

    Progress: [######----------] 30%
    ```

---

## Exit

To exit the app, simply choose option 4 from the main menu.

- **Simulated Response**:
    ```
    Exiting the app...
    ```

---

## Error Handling

If the user enters an invalid option or input, the app will display an error message and prompt the user to try again.

- **Simulated Response**:
    ```
    Invalid choice. Please select a valid number.
    ```

---

## Conclusion

The **Metro Manila Commuter App** is a powerful tool for navigating the Metro Manila train system. With features for planning your commute, finding landmarks, and more, it makes getting around the city easier and more efficient.
