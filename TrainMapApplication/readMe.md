# Metro Manila Train Commuter App

A C# console application that helps users plan their commute using Metro Manila's major train lines: **MRT-3**, **LRT-1**, and **LRT-2**. The app allows users to view train maps, find stations or landmarks, and get commute routes including transfer logic between train lines.

## ✨ Features

- 🗺️ **Display Train Map** — Show available train lines and stations.
- 📍 **Smart Search** — Find stations or nearby landmarks using fuzzy search.
- 🔁 **Transfer Logic** — Handles station transfers between LRT-1, MRT-3, and LRT-2.
- 🧭 **Commute Planning** — Route calculation from a start station/landmark to a destination.

## 🚉 Train Lines

The app includes stations and landmarks for:
- **MRT-3** (e.g., Taft Avenue, Ayala, Cubao)
- **LRT-1** (e.g., EDSA, Pedro Gil, Baclaran)
- **LRT-2** (e.g., Katipunan, Recto, Antipolo)

## 🔁 Transfer Points

The app supports train transfers at the following stations:
- **Taft Avenue** (MRT-3) ↔️ **EDSA** (LRT-1)
- **Cubao** (MRT-3) ↔️ **Araneta Center Cubao** (LRT-2)
- **Doroteo Jose** (LRT-1) ↔️ **Recto** (LRT-2)

## 🛠️ Core Components

### `InitializeTrainLines()`
Initializes a dictionary of train lines with their respective stations and nearby landmarks.

### `FindRoute()`
Calculates a route from a start location to a destination, considering transfer stations when needed.

### `FuzzySearch()`
Uses Levenshtein Distance to match user input to station or landmark names.

### `SearchLandmark()`
Searches landmarks by exact or partial match, then displays their nearest stations.

### `GetLocationFromUser()`
Interactive user prompt to select the correct location from a list of fuzzy-matched suggestions.

### `ExtractStationName()`
Parses user input to identify relevant station names from descriptive strings.

## 🤖 Smart Input Analysis

The app includes a Levenshtein Distance implementation for typo-tolerant input, making it easier to find stations and landmarks even with slight spelling errors.

### Example:

If user types:  
`"Trinoma"`  
The app might suggest:  
`"Trinoma near North Avenue on MRT-3 line"`

## 🧪 Sample Use Case

**User selects option 2: Plan Commute**

## 📁 Dependencies

- `System.Collections.Generic`
- `System.Text.RegularExpressions`
- `MapDisplayOptions` (custom/placeholder for `Maps.ShowMaps()`)

## 📌 To-Do / Improvements

- [ ] GUI version (e.g., Windows Forms or WPF)
- [ ] Real-time train schedules
- [ ] Fare calculation
- [ ] Integration with Google Maps API for accurate routing

## 👨‍💻 Author

Built with 💡 and logic by a commuter for fellow commuters.

