using MapDisplayOptions;
using System;

namespace SmartFunction
{
    public static class Xavier
    {
        public static int LevenshteinDistance(string s1, string s2) //this function was created using AI for the smart input analysis function
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

        public static List<string> FuzzySearch(string input)
        {
            var results = new List<string>();
            string normalizedInput = input.Trim().ToLower();
            int threshold = 3;

            foreach (var line in Maps.trainLines)
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

    }

}
