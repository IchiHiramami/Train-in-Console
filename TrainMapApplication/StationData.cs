using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class StationData
{
    public Dictionary<string, Dictionary<string, List<string>>> TrainLines { get; set; }

    // Method to load data from JSON
    public static StationData LoadFromJson(string path)
    {
        try
        {
            // Read the content of the JSON file
            string json = File.ReadAllText(path);

            // Debug: Print the contents of the JSON file
            Console.WriteLine("=== JSON File Content ===");
            Console.WriteLine(json);
            Console.WriteLine("=========================");

            // Deserialize the JSON into the StationData object
            return JsonConvert.DeserializeObject<StationData>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading JSON file: {ex.Message}");
            throw; // Rethrow the exception to be handled by the caller
        }
    }

    // Method to find stations that match the user input
    public List<string> FindStationSuggestions(string input)
    {
        var suggestions = new List<string>();

        foreach (var line in TrainLines)
        {
            foreach (var station in line.Value)
            {
                if (station.Key.ToLower().Contains(input.ToLower()))
                {
                    suggestions.Add(station.Key);
                }
            }
        }

        return suggestions;
    }

    // Method to show landmarks near a station
    public List<string> GetLandmarksForStation(string lineName, string stationName)
    {
        if (TrainLines.ContainsKey(lineName) && TrainLines[lineName].ContainsKey(stationName))
        {
            return TrainLines[lineName][stationName];
        }
        return new List<string> { "No landmarks found for this station." };
    }
}
