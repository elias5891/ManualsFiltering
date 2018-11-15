using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using ManualsFiltering;

class Program
{

    static void Main(string[] args)
    {

        var fullModuleList = new List<Module>();
        List<Module> partialModuleList = new List<Module>();
        var favoriteModuleList = new List<Module>();
        var command = "";

        //TODO: Pull from the previously saved version.
        fullModuleList = getModuleList("ListOfModules.json");
        partialModuleList.AddRange(fullModuleList);

        do
        {
            command = displayOptions(); //Display the table of options of what to do with the data.
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
                    favoriteModuleList = addFavorite(partialModuleList, fullModuleList);
                    break;

                case "8":
                    break;

                default:
                    Console.WriteLine("That is not a valid command, please enter a new command.");
                    break;
            }
        } while (command != "8");

        Console.WriteLine("Thank you for using this program.  Press enter to exit.");
        Console.ReadLine();
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
        var indexPos = 0;
        Console.WriteLine("Please enter the criteria you are looking for.");
        var searchString = Console.ReadLine();
        switch (criteria)
        {
            case "name":
                foreach (Module module in allModules)
                {
                    if (module.Name.ToUpper().Contains(searchString.ToUpper()))
                    {
                        module.indexPosition = indexPos++;
                        tempModuleList.Add(module);
                    }
                }
                break;

            case "defuserdifficulty":
                foreach (Module module in allModules)
                {
                    if (module.DefuserDifficulty != null && module.DefuserDifficulty.ToUpper().Contains(searchString.ToUpper()))
                    {
                        module.indexPosition = indexPos++;
                        tempModuleList.Add(module);
                    }
                }
                break;

            case "expertdifficulty":
                foreach (Module module in allModules)
                {
                    if (module.ExpertDifficulty != null && module.ExpertDifficulty.ToUpper().Contains(searchString.ToUpper()))
                    {
                        module.indexPosition = indexPos++;
                        tempModuleList.Add(module);
                    }
                }
                break;

            case "twitchplaysscore":
                foreach (Module module in allModules)
                {
                    if (module.TwitchPlaysScore != null && module.TwitchPlaysScore == Convert.ToInt32(searchString))
                    {
                        module.indexPosition = indexPos++;
                        tempModuleList.Add(module);
                    }
                }
                break;


            default:
                break;
        }
        return tempModuleList;
    }


    //Method to convert json file to a set of Module objects in fullModuleList
    public static List<Module> getModuleList(string fileName)
    {
        var serializer = new JsonSerializer();
        var fileLocation = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\", fileName));
        using (var reader = new StreamReader(fileLocation))
        using (var jsonReader = new JsonTextReader(reader))
        {
            return serializer.Deserialize<List<Module>>(jsonReader);
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
        Console.WriteLine("7. Add module to favourites.");
        Console.WriteLine("----------------------");
        Console.WriteLine("8. Exit Program");
        string command = Console.ReadLine();
        return command;
    }
}