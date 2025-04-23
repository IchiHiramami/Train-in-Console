using System.Text.RegularExpressions;
using MapDisplayOptions;
using SmartFunction;

class MetroManilaCommuteApp
{

    static void Main()
    {
        bool exit = false;
        Maps.InitializeTrainLines();
        while (!exit)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nMETRO MANILA TRAIN COMMUTER APP\n===============================\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. Display Train Map");
            Console.WriteLine("2. Plan Your Commute");
            Console.WriteLine("3. Find Landmark or Station");
            Console.WriteLine("4. Exit");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Maps.ShowMaps();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Press Any key to continue");
                    Console.ReadKey();
                    break;
                case "2":
                    origin_destinationHandler();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Press any key to return to main menu");
                    Console.ReadKey();
                    break;
                case "3":
                    SearchLandmarkMenu();
                    break;
                case "4":
                    exit = true;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void origin_destinationHandler()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Where are you starting from? (station or landmark)");
        string startInput = Console.ReadLine().Trim();
        string start = GetLocationFromUser(startInput);
        if (start == null) return;

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Where is your destination? (station or landmark)");
        string destInput = Console.ReadLine().Trim();
        string destination = GetLocationFromUser(destInput);
        if (destination == null) return;

        FindRoute(start, destination);
    }

    static void SearchLandmarkMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Landmark and Station Options:");
            Console.WriteLine("1. Search station near a landmark");
            Console.WriteLine("2. Show all landmarks near a station");
            Console.WriteLine("3. Show list of all stations per line");
            Console.WriteLine("4. Go back to main menu");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter a landmark to search: ");
                    string landmarkInput = Console.ReadLine();
                    List<string> landmarkResults = Xavier.FuzzySearch(landmarkInput);

                    if (landmarkResults.Count == 0)
                    {
                        // If no matches found, suggest alternatives using Levenshtein Distance
                        List<string> allLandmarks = new List<string>();

                        // Collect landmarks from all stations in all train lines
                        foreach (var line in Maps.trainLines)
                        {
                            foreach (var station in line.Value)
                            {
                                allLandmarks.AddRange(station.Value);  // Add landmarks associated with the station
                            }
                        }

                        // Suggest landmarks based on Levenshtein distance
                        var suggestedLandmarks = allLandmarks
                                                  .Distinct()  // Remove duplicate landmarks
                                                  .Select(landmark => new { Landmark = landmark, Distance = Xavier.LevenshteinDistance(landmarkInput.ToLower(), landmark.ToLower()) })
                                                  .Where(x => x.Distance <= 3)  // Allow some tolerance for typo
                                                  .OrderBy(x => x.Distance)
                                                  .Take(5)  // Limit suggestions to top 5
                                                  .ToList();

                        if (suggestedLandmarks.Any())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No exact matches found. Did you mean one of these?");
                            Console.ResetColor();
                            foreach (var suggestion in suggestedLandmarks)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"- {suggestion.Landmark} (Distance: {suggestion.Distance})");
                            }
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No matching landmark found.");
                            Console.ResetColor();
                        }

                        Console.WriteLine("\nPress any key to return...");
                        Console.ReadKey();
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Possible matches:");
                    foreach (var result in landmarkResults)
                        Console.WriteLine("- " + result);
                    Console.ResetColor();
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                    break;

                case "2":
                    Console.Write("Enter a station to list its landmarks: ");
                    string stationInput = Console.ReadLine().Trim().ToLower(); // Normalize input

                    bool found = false;

                    foreach (var line in Maps.trainLines)
                    {
                        foreach (var station in line.Value.Keys)
                        {
                            if (station.ToLower() == stationInput)
                            {
                                found = true;
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"\nLandmarks near {station} on {line.Key}:");
                                foreach (var landmark in line.Value[station])
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("- " + landmark);
                                }
                                break;
                            }
                        }
                        if (found) break;
                    }

                    if (!found)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No matching station found.");
                        Console.ResetColor();
                    }

                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                    break;


                case "3":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Stations per train line:\n");

                    foreach (var line in Maps.trainLines)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"{line.Key} Line:");
                        Console.ForegroundColor = ConsoleColor.White;

                        int count = 0;
                        foreach (var station in line.Value.Keys)
                        {
                            Console.Write($"{station.PadRight(30)}"); // Adjust the number to control column width
                            count++;

                            if (count % 3 == 0) // Wrap after 3 stations per line
                                Console.WriteLine();
                        }

                        if (count % 3 != 0)
                            Console.WriteLine(); // Ensure line break after each section
                        Console.WriteLine(); // Extra spacing between lines
                    }

                    Console.ResetColor();
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    break;

                case "4":
                    return;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Try again.");
                    Console.ResetColor();
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void FindRoute(string start, string destination)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nCalculating route from {0} to {1}...\n", start, destination);

        string startStation = ExtractStationName(start);
        string destStation = ExtractStationName(destination);
        string startLine = GetLineForStation(startStation);
        string destLine = GetLineForStation(destStation);

        if (startLine == null || destLine == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Unable to determine train lines for the provided stations.");
            Console.ResetColor();
            return;
        }

        List<string> route = new List<string>();

        if (startLine == destLine)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Travel directly from {startStation} to {destStation} on the {startLine} line.");
            Console.ResetColor();

            route = GetStationsBetween(startLine, startStation, destStation);
        }
        else
        {
            bool transferFound = false;
            foreach (var transfer in Maps.transferPoints)
            {
                string station1 = transfer.Key;
                foreach (string station2 in transfer.Value)
                {
                    if ((IsLandmark(startStation) || IsLandmark(destStation) || GetLineForStation(station1) == startLine)
                        && GetLineForStation(station2) == destLine)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Take {startLine} from {startStation} to {station1}, then transfer to {destLine} at {station2} to reach {destStation}.");
                        Console.ResetColor();
                        transferFound = true;

                        route.AddRange(GetStationsBetween(startLine, startStation, station1));
                        route.AddRange(GetStationsBetween(destLine, station2, destStation));
                        break;
                    }
                    if ((IsLandmark(startStation) || IsLandmark(destStation) || GetLineForStation(station2) == startLine)
                        && GetLineForStation(station1) == destLine)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Take {startLine} from {startStation} to {station2}, then transfer to {destLine} at {station1} to reach {destStation}.");
                        Console.ResetColor();
                        transferFound = true;

                        route.AddRange(GetStationsBetween(startLine, startStation, station2));
                        route.AddRange(GetStationsBetween(destLine, station1, destStation));
                        break;
                    }
                }
                if (transferFound) break;
            }

            if (!transferFound)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No available transfer found between the two lines.");
                Console.ResetColor();
            }
        }

        if (route.Count > 0)
        {
            ShowRideProgress(route);
        }
    }
        
    static string ExtractStationName(string input)
    {
        var match = Regex.Match(input, @"near (?<station>.+?) on");
        if (match.Success)
            return match.Groups["station"].Value.Trim();

        match = Regex.Match(input, @"^(?<station>.+?) on");
        if (match.Success)
            return match.Groups["station"].Value.Trim();

        foreach (var line in Maps.trainLines)
        {
            if (line.Value.ContainsKey(input))
                return input;
        }

        foreach (var line in Maps.trainLines)
        {
            foreach (var station in line.Value)
            {
                if (station.Value.Any(landmark =>
                    string.Equals(landmark, input, StringComparison.OrdinalIgnoreCase)))
                {
                    return station.Key;
                }
            }
        }

        return input;
    }

    static List<string> GetStationsBetween(string line, string start, string end)
    {
        if (!Maps.trainLines.ContainsKey(line)) return new List<string>();

        var stations = Maps.trainLines[line].Keys.ToList();
        int startIndex = stations.IndexOf(start);
        int endIndex = stations.IndexOf(end);

        if (startIndex == -1 || endIndex == -1) return new List<string>();

        List<string> path;
        if (startIndex <= endIndex)
            path = stations.GetRange(startIndex, endIndex - startIndex + 1);
        else
            path = stations.GetRange(endIndex, startIndex - endIndex + 1).AsEnumerable().Reverse().ToList();

        List<string> transferStations = path.Where(station => Maps.transferPoints.ContainsKey(station)).ToList();

        if (transferStations.Any())
        {
            Console.WriteLine("Transfer points found: ");
            foreach (var transfer in transferStations)
            {
                Console.WriteLine($"- Transfer at {transfer}");
            }
        }

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\nSimulating travel with progress...\n");

        int total = path.Count;
        for (int i = 0; i < total; i++)
        {
            string station = path[i];

            string passedStations = string.Join(" ", path.Take(i));
            string currentStation = path[i];
            string upcomingStations = string.Join(" ", path.Skip(i + 1));

            string progressBar = $"[{GetColoredStationString(passedStations, ConsoleColor.Green)}" +
                                  $"{GetColoredStationString(currentStation, ConsoleColor.Red)}" +
                                  $"{GetColoredStationString(upcomingStations, ConsoleColor.Yellow)}]";

            Console.Clear();
            Console.Write(progressBar.PadRight(Console.WindowWidth));
        }
        Console.WriteLine();

        return path;
    }

    static string GetColoredStationString(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        string coloredText = text;
        Console.ResetColor();
        return coloredText;
    }

    static string GetLineForStation(string stationName)
    {
        foreach (var line in Maps.trainLines)
        {
            if (line.Value.ContainsKey(stationName))
            {
                return line.Key;
            }
        }
        return null;
    }

    static string GetLocationFromUser(string input)
    {
        List<string> suggestions = Xavier.FuzzySearch(input);

        if (suggestions.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No matching station or landmark found.");
            Console.ResetColor();
            return null;
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Did you mean any of these?");
        for (int i = 0; i < suggestions.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {suggestions[i]}");
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Please select a number from the suggestions (1 to N):");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > suggestions.Count)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid choice. Please select a valid number.");
            Console.ResetColor();
        }

        return suggestions[choice - 1];
    }

    static bool IsLandmark(string station)
    {
        return station.Contains("landmark");
    }

    public static void ShowRideProgress(List<string> route)
    {
        Console.WriteLine("\nStart ride? (y/n): ");
        string confirm = Console.ReadLine()?.ToLower();

        if (confirm != "y") return;

        for (int i = 0; i < route.Count; i++)
        {
            Console.Clear();
            Console.WriteLine("Ride Progress:\n");

            // Loop through the route and display each station's status
            for (int j = 0; j < route.Count; j++)
            {
                string station = route[j];
                bool isTransfer = Maps.transferPoints.ContainsKey(station);

                // Adjust colors based on the station's status
                if (j == 0 || j == route.Count - 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Start/end
                }
                else if (j < i)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray; // Passed
                }
                else if (j == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Current
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White; // Upcoming
                }

                // Add (Transfer) tag if it's a transfer point
                string label = isTransfer ? $"{station} (Transfer)" : station;
                Console.Write($"**{label}**");

                if (j < route.Count - 1)
                    Console.Write(" -> ");
            }

            Console.ResetColor();

            // Calculate progress and display a progress bar
            double progress = ((i + 1) / (double)route.Count) * 100;
            int barLength = 20;
            int filled = (int)(progress / 100 * barLength);
            string bar = "[" + new string('#', filled) + new string(' ', barLength - filled) + "]";

            Console.WriteLine($"\n\nProgress: {bar} {progress:0}%");

            if (i < route.Count - 1)
            {
                Thread.Sleep(1000); // Delay to simulate the ride
            }
            else
            {
                Console.WriteLine("\nYou have reached your destination!");
            }
        }
    }
}
