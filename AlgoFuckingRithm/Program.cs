using System;
using System.Collections.Generic;

class MetroManilaCommuteApp
{
    static Dictionary<string, Dictionary<string, List<string>>> trainLines = new Dictionary<string, Dictionary<string, List<string>>>();

    static void Main()
    {
        bool exit = false;
        InitializeTrainLines();
        while (exit != true)
        {
            Console.Clear();
            Console.WriteLine("Metro Manila Train Commute App");
            Console.WriteLine("1. Display Train Map");
            Console.WriteLine("2. Plan Commute");
            Console.WriteLine("3. Show Tour Guide");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":   
                    DisplayTrainMap();
                    break;
                case "2":
                    Console.Write("Enter starting station: ");
                    string start = Console.ReadLine();
                    Console.Write("Enter destination station: ");
                    string destination = Console.ReadLine();
                    PlanCommute(start, destination);
                    Console.Write("Start Journey?");
                    string input = Console.ReadLine();
                    if (input == "Y")
                    {
                        Console.WriteLine("Starting Journey Tracking");
                        TrackProgress(start, destination);
                    }

                    else { return; }

                    break;
                case "3":
                    Console.Write("Enter a station to see nearby landmarks: ");
                    string station = Console.ReadLine();
                    ShowTourGuide(station);
                    break;
                case "4":
                    exit = true;
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void InitializeTrainLines()
    {
        trainLines["MRT-3"] = new Dictionary<string, List<string>>
    {
        { "StationA", new List<string> { "Landmark1", "Landmark2" } },
        { "StationB", new List<string> { "Landmark3", "Landmark4" } },
        { "StationC", new List<string> { "Landmark5", "Landmark6" } }
    };

        trainLines["LRT-2"] = new Dictionary<string, List<string>>
    {
        { "StationD", new List<string> { "Landmark7", "Landmark8" } },
        { "StationE", new List<string> { "Landmark9", "Landmark10" } }
    };
    }

    static void DisplayTrainMap()
    {
        Console.WriteLine("\nTrain Lines in Metro Manila (2025):");
        foreach (var line in trainLines)
        {
            Console.WriteLine(line.Key + " Stations: " + string.Join(", ", line.Value));
        }
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }

    static void PlanCommute(string start, string destination)
    {
        Console.WriteLine($"\nCalculating shortest and cheapest route from {start} to {destination}...");
        Console.WriteLine("(Route calculation logic to be implemented)");
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }

    static void TrackProgress(string currentStation, string destination)
    {
        Console.WriteLine($"\nTracking progress: You are currently at {currentStation}, heading towards {destination}.");
        Console.WriteLine("(Progress tracking logic to be implemented)");
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }

    static void ShowTourGuide(string currentStation)
    {
        Console.WriteLine($"\nLandmarks near {currentStation}:...");
        Console.WriteLine("(Landmark fetching logic to be implemented)");
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }
}
