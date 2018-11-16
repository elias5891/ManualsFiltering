# ManualsFiltering
C# Code Louisville Project

This project is used to sort through a JSON of the various modules from the game "Keep Talking and Nobody Explodes", based on name, difficulty, or point score.  Users can search for modules that fit certain criteria, and save them to a favorites list, which can be saved as .txt and .json files for future consumption.

To use this program, download the project and open it in Visual Studio.  Open Program.cs, and run via F5. (Or, you could build and use that way.)  (The startup solution needed to be set on one test of this project, that may need to be done during testing)


The purpose of this project is to use the selection options (3-6 from main menu) to give criteria, which will restrict/target specific 'modules'.  When started, any search criteria given through the selection options will target the full module list.  After the first search, future searches will target only the results from the previous search.  The results can be reset with main menu option 1.  

Once 'modules' that are enjoyed are found, they can be added to the favorites list based on their index number, and the favorites list can be viewed (and saved across executions of the app).  All non-score searches find partial or whole matches, case insensitive.


For those unfamiliar with the game, a full list of the modules can be found at http://ktane.timwi.de.  For testing purposes, the following criteria can be used:

Expert Difficulty and Defuser Difficulty is always either VeryEasy, Easy, Medium, Hard, or VeryHard.

Score ranges between 1 and 20.

For name searches, there are lots of names, but the words "Simon", "Morse", "Color", and "Maze" are words that appear in a large number of module names.

Potential testing criteria:
Search for a module containing the word color.
Choose one, and add to favorites.
Reset search.
Search for a module worth 6 points.
Choose one, and add to favorites.
Save and exit.
Reopen, and view favorites.
