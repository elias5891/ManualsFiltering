# ManualsFiltering
C# Code Louisville Project

This project is used to sort through a JSON of the various modules from the game "Keep Talking and Nobody Explodes", based on name, difficulty, or point score.  Users can search for modules that fit certain criteria, and save them to a favorites list, which can be saved as .txt and .json files for future consumption.

To use this program, download the project and open it in Visual Studio.  Executing directly from Visual Studio will allow the user full access to all necessary operations.

While using this program, a filter list will be initialized upon start.  Search criteria will limit results, and further searches will limit within the previous results list only.  To start a new search from the full list, use the option to reset the 'selected' list.

For those unfamiliar with the game, a full list of the modules can be found at http://ktane.timwi.de.  For testing purposes, the following criteria can be used:

  Expert Difficulty and Defuser Difficulty is either VeryEasy, Easy, Medium, Hard, or VeryHard.

 Score ranges between 1 and 20.

 There are lots of various names, but words "Simon", "Morse", "Color", and "Maze" are words that appear in a large number of modules.
