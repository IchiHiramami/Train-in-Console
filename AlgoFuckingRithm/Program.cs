using System;
using System.Collections.Generic;

class MetroManilaCommuteApp
{
    static Dictionary<string, List<string>> trainLines = new Dictionary<string, List<string>>();

    static void Main()
    {
        InitializeTrainLines();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Metro Manila Train Commute App");
            Console.WriteLine("1. Display Train Map");
            Console.WriteLine("2. Plan Commute");
            Console.WriteLine("3. Track Progress");
            Console.WriteLine("4. Show Tour Guide");
            Console.WriteLine("5. Exit");
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
                    break;
                case "3":
                    Console.Write("Enter your current station: ");
                    string currentStation = Console.ReadLine();
                    Console.Write("Enter your destination station: ");
                    string dest = Console.ReadLine();
                    TrackProgress(currentStation, dest);
                    break;
                case "4":
                    Console.Write("Enter a station to see nearby landmarks: ");
                    string station = Console.ReadLine();
                    ShowTourGuide(station);
                    break;
                case "5":
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
        trainLines["LRT-2"] = new List<string> { "Recto", "Legarda", "Pureza", "V. Mapa", "J. Ruiz", "Gilmore", "Betty Go", "Cubao", "Anonas", "Katipunan", "Santolan", "Marikina", "Antipolo" };
        trainLines["MRT-3"] = new List<string> { "North Avenue", "Quezon Avenue", "GMA Kamuning", "Araneta-Cubao", "Santolan-Annapolis", "Ortigas", "Shaw Boulevard", "Boni", "Guadalupe", "Buendia", "Ayala", "Magallanes", "Taft Avenue" };

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
