﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using MapDisplayOptions;

class MetroManilaCommuteApp
{
    static Dictionary<string, Dictionary<string, List<string>>> trainLines = new Dictionary<string, Dictionary<string, List<string>>>();
    bool pass = false;

    static void Main()
    {
        bool exit = false;
        InitializeTrainLines();
        while (exit != true)
        {
            Console.Clear();
            Console.WriteLine("\nMETRO MANILA TRAIN COMMUTER APP\n===============================\n1. Display Train Map\n2. Plan Your Commute\n3. Find Landmark or Station\n4. Exit\n===============================");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Maps.ShowMaps();
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


    static void DisplayTrainMap()
    {
        Console.WriteLine("\nTrain Lines in Metro Manila (2025):");
        Console.WriteLine("");
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
