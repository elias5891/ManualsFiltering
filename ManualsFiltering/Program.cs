using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ManualsFiltering;

class Program
{

    static void Main(string[] args)
    {
        //Initialize necessary Lists.
        List<Module> fullModuleList = new List<Module>();
        List<Module> partialModuleList = new List<Module>();
        List<Module> favoriteModuleList = new List<Module>();
        var command = "";
        fullModuleList = getModuleList("ListOfModules.json");
        indexAll(fullModuleList);
        partialModuleList.AddRange(fullModuleList);
        if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Favorites.json"))) //If Favorites list already exists, load it up as well.
            favoriteModuleList = getModuleList("Favorites.json");
        if(File.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\", "Favorites.json")))
            favoriteModuleList = getModuleList("Favorites.json");

        //Main loop -- run through menu options until the program is exited.
        do
        {
            command = displayOptions(); 
            switch (command)
            {
                case "1":
                    partialModuleList.AddRange(fullModuleList);
                    Console.WriteLine("Module List Reset.  Press enter to continue.");
                    break;

                case "2":
                    displayPartialModules(partialModuleList);
                    break;

                case "3":
                    partialModuleList = searchModuleList("name", partialModuleList);
                    displayPartialModules(partialModuleList);
                    break;

                case "4":
                    partialModuleList = searchModuleList("expertdifficulty", partialModuleList);
                    displayPartialModules(partialModuleList);
                    break;

                case "5":
                    partialModuleList = searchModuleList("defuserdifficulty", partialModuleList);
                    displayPartialModules(partialModuleList);
                    break;

                case "6":
                    partialModuleList = searchModuleList("twitchplaysscore", partialModuleList);
                    displayPartialModules(partialModuleList);
                    break;

                case "7":
                    favoriteModuleList = addFavorite(favoriteModuleList, partialModuleList, fullModuleList);
                    break;

                case "8":
                    favoriteModuleList = removeFavorite(favoriteModuleList);
                    break;

                case "9":
                    displayPartialModules(favoriteModuleList);
                    break;
                
                case "0":
                    writeFavorites(favoriteModuleList);
                    break;

                default:
                    Console.WriteLine("That is not a valid command, please enter a new command.");
                    break;
            }
        } while (command != "0");

        Console.WriteLine("Thank you for using this program.  Press enter to exit.");
        Console.ReadLine();
    }


    //Method to write the favorites list.
    public static void writeFavorites(List<Module> favList)
    {
        {
            //Write the objects to a JSON file for reloading on future runs.
            var fileLocation = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Favorites.json")); 
            using (var writer = new StreamWriter(fileLocation))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(jsonWriter, favList);
            }
            //Also, write to a plaintext a user-friendly version of the data that is displayed.
            using (System.IO.StreamWriter writeFile = 
                new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "Favorites.txt")))  
           {
               foreach (Module mod in favList)
               {
                    writeFile.WriteLine("Module: " + mod.Name + " (" + mod.DefuserDifficulty + "/" + mod.ExpertDifficulty + ") {" + mod.TwitchPlaysScore + "}");
               }
           }
        }
        return;
    }

    //Method to remove a module from the favorites list.
    public static List<Module> removeFavorite(List<Module> favList)
    {
        Console.WriteLine("Please enter the index of the module to remove.  Leave blank if you don't know it.");
        int currentIndex = Convert.ToInt32(Console.ReadLine());
        foreach(Module mod in favList)
        {
            if (mod.indexPosition == currentIndex)
            {
                favList.Remove(mod);
                Console.WriteLine("Module removed from favorites.");
                Console.ReadLine();
                return favList;
            }
        }
        return favList;
    }


    //Method to add a module to the favorites list.
    public static List<Module> addFavorite(List<Module> favList, List<Module> tempList, List<Module> fullList)
    {
        Console.WriteLine("Please enter the index of the module to add.  Leave blank if you don't know it.");
        int currentIndex = Convert.ToInt32(Console.ReadLine());
        if (currentIndex < fullList.Count)
        {
            if (fullList[currentIndex] != null)
            {
                if (!favList.Contains(fullList[currentIndex]))
                {
                    favList.Add(fullList[currentIndex]);
                    Console.WriteLine("Added to favorites.");
                    Console.ReadLine();
                }
            }
        }
        return favList;
    }

    //Method to display the current list
    public static void displayPartialModules(List<Module> targetModules)
    {
        Console.Clear();
        Console.WriteLine("Selected Modules:");
        Console.WriteLine("");
        Console.WriteLine("{Index, Name, D.Difficulty, E.Difficulty, Score}");
        foreach (Module module in targetModules)
        {
            Console.WriteLine("{" + module.indexPosition + ", " + module.Name + ", " + module.DefuserDifficulty + ", " + module.ExpertDifficulty + ", " + module.TwitchPlaysScore + "}");
        }
        Console.WriteLine("");
        Console.WriteLine("Press enter to continue.");
        Console.ReadLine();
        return;
    }

    //Method to perform a search for modules that contain certain criteria.
    public static List<Module> searchModuleList(string criteria, List<Module> allModules)
    {
        var tempModuleList = new List<Module>();
        Console.WriteLine("Please enter the criteria you are looking for.");
        var searchString = Console.ReadLine();
        switch (criteria) // split exact search method by type of search.
        {
            case "name":
                for (int i = 0; i < allModules.Count; i++)
                {
                    if (!tempModuleList.Contains(allModules[i]) && allModules[i].Name.ToUpper().Contains(searchString.ToUpper())) //remove case sensitivity from search, and don't re-catch if it's already in the list from a previous search.
                    {
                        tempModuleList.Add(allModules[i]);
                    }
                }
                break;

            case "defuserdifficulty":
                for (int i = 0; i < allModules.Count; i++)
                {
                    if (!tempModuleList.Contains(allModules[i]) && allModules[i].DefuserDifficulty != null && allModules[i].DefuserDifficulty.ToUpper().Contains(searchString.ToUpper())) //In addition to above, also exclude items with no difficulties.
                    {
                        tempModuleList.Add(allModules[i]);
                    }
                }
                break;

            case "expertdifficulty":
                for (int i = 0; i < allModules.Count; i++)
                {
                    if (!tempModuleList.Contains(allModules[i]) && allModules[i].ExpertDifficulty != null && allModules[i].ExpertDifficulty.ToUpper().Contains(searchString.ToUpper()))
                    {
                        tempModuleList.Add(allModules[i]);
                    }
                }
                break;

            case "twitchplaysscore":
                for (int i = 0; i < allModules.Count; i++)
                {
                    if (!tempModuleList.Contains(allModules[i]) && allModules[i].TwitchPlaysScore > 0 && allModules[i].TwitchPlaysScore == Convert.ToInt32(searchString))
                    {
                        tempModuleList.Add(allModules[i]);
                    }
                }
                break;


            default:
                break;
        }
        return tempModuleList;
    }


    //Set Index position for each module to the module's location in the master list.
    public static void indexAll(List<Module> fullModuleList)
    {
        for (int i= 0;i < fullModuleList.Count; i++)
        {
            fullModuleList[i].indexPosition = i;
        }
        return;
    }

    //Method to convert json file to a set of Module objects in fullModuleList
    public static List<Module> getModuleList(string fileName)
    {
        var serializer = new JsonSerializer();
        var fileLocation = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), fileName));
        if (!File.Exists(fileLocation))
        {
            fileLocation = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\", fileName));
        }
        {
            using (var reader = new StreamReader(fileLocation))
            using (var jsonReader = new JsonTextReader(reader))

            {
                return serializer.Deserialize<List<Module>>(jsonReader);
            }
        }


    }

    //Method to show options for how to interact with the program.
    public static string displayOptions()
    {
        Console.Clear();
        Console.WriteLine("----------------------");
        Console.WriteLine("------MAIN MENU-------");
        Console.WriteLine("----------------------");
        Console.WriteLine("1. Reset selected criteria.");
        Console.WriteLine("2. Show currently selected modules.");
        Console.WriteLine("3. Select modules by name.");
        Console.WriteLine("4. Select modules by expert difficulty.");
        Console.WriteLine("5. Select modules by diffuser difficulty.");
        Console.WriteLine("6. Select modules by score.");
        Console.WriteLine("7. Add module to favorites.");
        Console.WriteLine("8. Remove module from favorites.");
        Console.WriteLine("9. View favorites.");
        Console.WriteLine("0. Save favorites and exit.");
        string command = Console.ReadLine();
        return command;
    }
}
