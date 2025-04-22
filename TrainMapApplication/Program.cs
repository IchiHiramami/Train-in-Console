using System.Text.RegularExpressions;
using MapDisplayOptions;

class MetroManilaCommuteApp
{
    static Dictionary<string, Dictionary<string, List<string>>> trainLines = new Dictionary<string, Dictionary<string, List<string>>>();
    
    static Dictionary<string, List<string>> transferPoints = new Dictionary<string, List<string>>()
{
    { "Taft Avenue", new List<string> { "EDSA" } },
    { "EDSA", new List<string> { "Taft Avenue" } },
    { "Araneta Center Cubao", new List<string> { "Cubao" } },
    { "Cubao", new List<string> { "Araneta Center Cubao" } },
    { "Doroteo Jose", new List<string> { "Recto" } },
    { "Recto", new List<string> { "Doroteo Jose" } },
};

    static void Main()
    {
        bool exit = false;
        InitializeTrainLines();
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
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Enter a landmark to search: ");
                    string input = Console.ReadLine();
                    Console.Write("Use exact match? (y/n): ");
                    bool exact = Console.ReadLine().Trim().ToLower() == "y";
                    SearchLandmark(input, exact);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Press any key to return to main menu");
                    Console.ReadKey();
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
            foreach (var transfer in transferPoints)
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

        foreach (var line in trainLines)
        {
            if (line.Value.ContainsKey(input))
                return input;
        }

        foreach (var line in trainLines)
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
    if (!trainLines.ContainsKey(line)) return new List<string>();

    var stations = trainLines[line].Keys.ToList();
    int startIndex = stations.IndexOf(start);
    int endIndex = stations.IndexOf(end);

    if (startIndex == -1 || endIndex == -1) return new List<string>();

    List<string> path;
    if (startIndex <= endIndex)
        path = stations.GetRange(startIndex, endIndex - startIndex + 1);
    else
        path = stations.GetRange(endIndex, startIndex - endIndex + 1).AsEnumerable().Reverse().ToList();

    List<string> transferStations = path.Where(station => transferPoints.ContainsKey(station)).ToList();

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
        Thread.Sleep(1000);
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

    static string GetColoredString(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        return text;
    }

    static string GetLineForStation(string stationName)
    {
        foreach (var line in trainLines)
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
        List<string> suggestions = FuzzySearch(input);

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

    static int LevenshteinDistance(string s1, string s2) //this function was created using AI for the smart input analysis function
    {
        int lenS1 = s1.Length;
        int lenS2 = s2.Length;
        var dp = new int[lenS1 + 1, lenS2 + 1];

        for (int i = 0; i <= lenS1; dp[i, 0] = i++) { }
        for (int j = 0; j <= lenS2; dp[0, j] = j++) { }

        for (int i = 1; i <= lenS1; i++)
        {
            for (int j = 1; j <= lenS2; j++)
            {
                int cost = s1[i - 1] == s2[j - 1] ? 0 : 1;
                dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
            }
        }

        return dp[lenS1, lenS2];
    }

    static List<string> FuzzySearch(string input)
    {
        var results = new List<string>();
        string normalizedInput = input.Trim().ToLower();
        int threshold = 3;

        foreach (var line in trainLines)
        {
            foreach (var station in line.Value)
            {
                string stationName = station.Key;

                if (stationName.ToLower().Contains(normalizedInput))
                {
                    results.Add($"{stationName} on {line.Key} line");
                }

                foreach (var landmark in station.Value)
                {
                    if (landmark.ToLower().Contains(normalizedInput) || LevenshteinDistance(normalizedInput, landmark.ToLower()) <= threshold)
                    {
                        results.Add($"{landmark} near {stationName} on {line.Key} line");
                    }
                }
            }
        }

        return results;
    }

    static void SearchLandmark(string input, bool exact)
    {
        var matches = new List<string>();

        foreach (var line in trainLines)
        {
            foreach (var station in line.Value)
            {
                foreach (var landmark in station.Value)
                {
                    if ((exact && landmark.Equals(input, StringComparison.OrdinalIgnoreCase)) ||
                        (!exact && landmark.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        matches.Add($"{landmark} near {station.Key} on {line.Key} line");
                    }
                }
            }
        }

        if (matches.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Search results:");
            foreach (var match in matches)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("- " + match);
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No results found.");
            Console.ResetColor();
        }
    }

    static bool IsLandmark(string station)
    {
        return station.Contains("landmark");
    }

    public static void ShowRideProgress(List<string> route)
    {
        Console.WriteLine("\nStart ride? (yes/no): ");
        string confirm = Console.ReadLine()?.ToLower();

        if (confirm != "yes") return;

        for (int i = 0; i < route.Count; i++)
        {
            Console.Clear();

            Console.WriteLine("Ride Progress:");
            for (int j = 0; j < route.Count; j++)
            {
                if (j < i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"[{route[j]}] ");
                }
                else if (j == i)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($">>{route[j]}<< ");
                }
                else
                {
                    Console.ResetColor();
                    Console.Write($"{route[j]} ");
                }
            }
            Console.ResetColor();

            double progress = ((i + 1) / (double)route.Count) * 100;
            int barLength = 20;
            int filled = (int)(progress / 100 * barLength);
            string bar = "[" + new string('#', filled) + new string(' ', barLength - filled) + "]";
            Console.WriteLine($"\n\nProgress: {bar} {progress:0}%");

            if (i < route.Count - 1)
            {
                Console.WriteLine("Press Enter to continue to the next station...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nYou have reached your destination!");
            }
        }
    }

    static void InitializeTrainLines()
    {

        trainLines["MRT-3"] = new Dictionary<string, List<string>>
    {
        { "Taft Avenue", new List<string> { "Transfer to LRT-1 (EDSA)" } },
        { "Magallanes", new List<string> { "Southgate Mall", "WalterMart Makati" } },
        { "Ayala", new List<string> { "Glorietta Malls", "Greenbelt Malls", "Makati Central Business District", "Holiday Inn", "Dusit Thani" } },
        { "Buendia", new List<string> { "Makati Central Business District" } },
        { "Guadalupe", new List<string> { "San Carlos Seminary", "Our Lady of Guadalupe Minor Seminary" } },
        { "Boni", new List<string> { "Rizal Technological University", "Victor R. Potenciano Medical Center" } },
        { "Shaw Boulevard", new List<string> { "SM Megamall", "Shangri-La Plaza", "Greenfield" } },
        { "Ortigas", new List<string> { "SM Megamall", "Robinsons Galleria", "Greenhills Shopping Center", "La Salle Greenhills" } },
        { "Santolan-Annapolis", new List<string> { "Philippine National Police (PNP)", "Camp Aguinaldo", "Camp Crame", "Greenhills Shopping Center" } },
        { "Cubao", new List<string> { "Transfer to LRT-2 (Cubao)", "Gateway", "Araneta Center", "Araneta Colosseum" } },
        { "GMA Kamuning", new List<string> { "GMA Network Center", "Manuel L. Quezon University", "Department of Public Works and Highways (DPWH)" } },
        { "Quezon Avenue", new List<string> { "ABS-CBN Broadcasting Center", "Centris Mall", "DILG-NAPOLCOM Center" } },
        { "North Avenue", new List<string> { "Transfer to LRT-1 (Fernando Poe Jr.)", "SM North EDSA", "Trinoma" } }
    };

        trainLines["LRT-2"] = new Dictionary<string, List<string>>
    {
        { "Antipolo", new List<string> { "SM Masinag", "APT Studios" } },
        { "Marikina-Pasig", new List<string> { "Sta. Lucia Mall", "Robinsons Metro East", "Ayala Malls Feliz" } },
        { "Santolan", new List<string> { "SM Marikina", "Ayala Malls Feliz", "Dee Hwa Liong Academy (DHLA)" } },
        { "Katipunan", new List<string> { "Philippine School of Business Administration (PSBA)", "Ateneo de Manila University (ADMU)", "Miriam College", "University of the Philippines (UP) Diliman", "St. Bridget School" } },
        { "Anonas", new List<string> { "Technological Institute of the Philippines (TIP)", "Saint Joseph Shrine" } },
        { "Cubao", new List<string> { "Transfer to MRT-3 (Cubao)", "Araneta Center", "Araneta Colosseum", "Gateway", "Cubao Cathedral Church" } },
        { "Betty-Go Belmonte", new List<string> { "Kalayaan College", "Cubao Cathedral Church", "Holy Buddhist Temple" } },
        { "Gilmore", new List<string> { "St. Paul University", "Trinity University of Asia" } },
        { "J. Ruiz", new List<string> { "Central Colleges of the Philippines" } },
        { "V. Mapa", new List<string> { "SM Sta. Mesa", "Central Colleges of the Philippines", "Immaculate Heart of Mary College" } },
        { "Pureza", new List<string> { "De Ocampo Memorial College", "Pio del Pilar Elementary School", "Polytechnic University of the Philippines (PUP)" } },
        { "Legarda", new List<string> { "Centro Escolar University (CEU)", "San Beda University", "National University (NU)", "University of Santo Tomas (UST)", "University of the East (UE)" } },
        { "Recto", new List<string> { "Transfer to LRT-1 (Doroteo Jose)", "University of Santo Tomas (UST)", "Far Eastern University (FEU)", "University of the East (UE)", "Binondo" } }
    };

        trainLines["LRT-1"] = new Dictionary<string, List<string>>
    {
        { "Redemptorist", new List<string> { "Seaside Market Baclaran", "S&R Membership Shopping - Aseana", "DFA Office of Consular Affairs" } },
        { "Baclaran", new List<string> { "National Shrine of our Mother of Perpetual Help (Baclaran Church)", "Baclaran Market", "MyMall", "Baclaran LRT Shopping Mall", "Resorts World Manila", "SM Mall of Asia", "SMX Convention Center" } },
        { "EDSA", new List<string> { "Transfer to MRT-3 (Taft Avenue)", "Metro Point Mall", "EDSA Shrine", "Saver's Square", "Winston Lodge" } },
        { "Libertad", new List<string> { "Department of Foreign Affairs (DFA)", "United States (US) Embassy", "Japanese Embassy", "Pasay City Hall", "Cuneta Astrodome", "Pamantasan ng Lungsod ng Pasay", "Wellcome Plaza" } },
        { "Gil Puyat", new List<string> { "Cartimar Shopping Station", "Arellano University", "CCP Center", "GSIS Complex", "Star City", "World Trade Center", "The Philippine Senate" } },
        { "Vito Cruz", new List<string> { "De la Salle University (DLSU)", "College of St. Benilde", "Bangko Sentral ng Pilipinas Complex", "University Mall Shopping Center", "Rizal Memorial Sports Complex", "Cultural Center of the Philippines (CCP) Complex" } },
        { "Quirino", new List<string> { "Luneta Park", "National Museum of the Philippines", "Manila Planetarium", "Robinsons Place Manila", "SM Manila", "Divisoria Market", "Manila Zoo" } },
        { "Pedro Gil", new List<string> { "University of the Philippines (UP) Manila", "Philippine General Hospital", "Philippine Christian University", "Philippine Women's University", "Robinsons Manila", "St. Paul University Manila" } },
        { "UN Avenue", new List<string> { "Supreme Court of the Philippines", "National Bureau of Investigation (NBI)", "Department of Justice (DOJ)", "World Health Organization Regional Headquarters", "Philippine Department of Tourism", "Rizal Park", "National Museum Complex", "Paco Park", "Intramuros", "Fort Santiago", "San Agustin Church", "Casa Manila", "Adamson University", "Philippine Normal University", "Technological University of the Philippines", "Emilio Aguinaldo College", "University of the Philippines (UP) Manila", "Colegio de San Juan de Letran", "Mapúa University", "Lyceum of the Philippines University", "Pamantasan ng Lungsod ng Maynila", "Manila Ocean Park", "Times Plaza", "Bayview Park", "Hotel Manila", "Manila Doctors Hospital", "Araullo High School", "Central United Methodist Church", "San Vicente de Paul Church", "Mehan Gardens", "Manila Metropolitan Theater", "Liwasang Bonifacio", "Manila Central Post Office", "Manila Hall of Justice", "Bonifacio Shrine", "Plaza Salamanca", "Plaza Rueda", "The Philippine National Library" } },
        { "Central Station", new List<string> { "SM Manila", "Fort Santiago", "San Agustin Church", "Manila Cathedral", "Mehan Gardens", "Manila Metropolitan Theater", "Manila City Hall", "Liwasang Bonifacio", "Manila Central Post Office", "Manila Hall of Justice", "Bonifacio Shrine", "National Museum of Fine Arts", "Manila Police District" } },
        { "Carriedo", new List<string> { "Quiapo Church", "Escolta Street", "Binondo (Chinatown)", "Santa Cruz Church", "Carriedo Fountain", "Plaza Lacson", "Plaza Miranda", "FEATI University", "Universidad de Manila Henry Sy Sr. Campus", "Good Earth Plaza", "Isetann Carriedo shopping centers", "SM Quiapo and Plaza Fair", "Arroceros Forest Park", "Liwasang Bonifacio or Plaza Lawton", "Far Eastern University (FEU)", "The Philippine Postal Corporation", "Divisoria Market", "Quinta Market & Fish Port", "Ongpin North Bridge Arch", "Pasig River Esplanade", "Jones Bridge", "Minor Basilica and National Shrine of Saint Lorenzo Ruiz (Binondo Church)" } },
        { "Doroteo Jose", new List<string> { "Transfer to LRT-2 (Recto)", "Binondo", "Isetann Cinema Recto", "Fabella Memorial Hospital" } },
        { "Bambang", new List<string> { "Santa Cruz Church", "Binondo Church", "Escolta Street" } },
        { "Tayuman", new List<string> { "SM City San Lazaro", "San Lazaro Hospital", "Philippine Department of Health Headquarters", "Dangwa Flower Market", "Espiritu Santo Church" } },
        { "Blumentritt", new List<string> { "Blumentritt Market", "SM City San Lazaro", "San Lazaro Tourism and Business Park", "Chinese General Hospital", "Manila North Cemetery", "San Roque de Manila Parish Church" } },
        { "Abad Santos", new List<string> { "Rizal Avenue", "Jose Abad Santos Avenue Interchange", "Manila Zoo", "Divisoria Market", "Seng Guan Buddhist Temple" } },
        { "R. Papa", new List<string> { "Lumang Simbahan", "Manila North Cemetery" } },
        { "5th Avenue", new List<string> { "Thai To Taoist Temple Pagoda", "Ung Siu Si Buddhist Temple", "Northern Rizal Yorkin Chinese School", "Philippine Cultural College" } },
        { "Monumento", new List<string> { "Bonifacio Monument", "SM City Grand Central", "Araneta Square Mall", "Puregold Monumento Branch", "Shrine for Our Lady of Grace", "Caloocan City Public Library", "Malabon Zoo", "Victory Central Mall" } },
        { "Balintawak", new List<string> { "Balintawak Market", "EDSA-Cloverleaf Interchange", "Monument of the Cry of Balintawak", "Parish Church for Joseph the Worker", "Balintawak Home Depot", "Ayala Cloverleaf Mall", "Puregold Balintawak", "Juliana Wet & Dry Market" } },
        { "Fernando Poe Jr.", new List<string> { "Transfer to MRT-3 (North Avenue)", "SM North EDSA", "Trinoma", "WalterMart North EDSA", "Jackman Plaza Muñoz", "Muñoz Market", "S&R Membership Shopping - Congressional" } }
    };
    }
}
