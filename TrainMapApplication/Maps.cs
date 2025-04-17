using System;

namespace MapDisplayOptions
{
    public static class Maps
    {
        public static async void ShowMaps()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "mapart.txt");
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine(content);
                Console.Write("Press Enter to Continue");
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("File not found!");
                Console.Write("Press Enter to Continue");
                Console.ReadKey(true);
            }
        }
    }
}
