//Revision history:
//Mohammadreza Abolhassani      2021-12-11      created the class TheConsoleGame containing console project's static Main() method 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Monster_Hunter;

namespace Monster_Hunter_Console
{
    class TheConsoleGame
    {
        public static Map goMap = null;
        public static Hunter goHunter = null;
        public static Monsters goMonsters = null;
        public static bool gbQuit = false;

        public static void Main(string[] args)
        {
            goMap = new Map(); //create the global map object
            goMonsters = new Monsters(); //create the global monsters object
            goHunter = new Hunter(0, 0); //create the global object hunter temporarily at position (0, 0) without attaching a map to it

            Console.WriteLine("\tWelcome to the Monster Hunter console game.\n");

            Console.WriteLine("\tPlease enter your name.");

            PromptPlayerName();
            Console.WriteLine("\tPick a map to play in:");
            //show list of available map files
            for (int i = 0; i < goMap.MapFileNames.Length; i++)
            {
                Console.WriteLine("\t" + (i + 1).ToString() + "- " + goMap.MapFileNames[i].Substring(2, goMap.MapFileNames[i].Length - 6));
            }
            //prompt the user for number of the map they want to play
            int iMapIndex = PromptForMapFileIndex();

            //try loading the map file by it's file index
            if (goMap.LoadMapFromFile(iMapIndex - 1, goHunter, goMonsters))
            {
                Console.WriteLine("\tMap loaded successfully.");
            }
            else
            {
                Console.WriteLine(goMap.ValidationError);
            }
            Console.WriteLine("\tPress any key to start the game... ");
            Console.ReadKey();
            Console.Clear();
            Display.DisplayMap(goMap);

            //Create and start a child thread to move the monsters every 2 seconds 
            ThreadStart monsterThreahRef = new ThreadStart(MoveMonstersThread);
            Thread moveMonstersThread = new Thread(monsterThreahRef);
            moveMonstersThread.Start();

            ConsoleKeyInfo userInput; //to hold user input
            //main loop. repeat until player hits Escape, dies or wins
            while (!gbQuit && !goHunter.IsDead && !goHunter.GoalFound)
            {
                Thread.Sleep(goHunter.MilliseconsBetweenMoves); //wait for player's freeze time to finish
                Console.SetCursorPosition(0, 0); //put the curser top left so it won't distract the user
                userInput = Console.ReadKey(); //receive user input
                //write blank to erase users input from screen
                Console.SetCursorPosition(0, 0);
                Console.Write(" ");
                ProcessPlayerInput(userInput);
            }
            gbQuit = true;
            if (goHunter.IsDead)
            {
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tYou're Dead! Press any key to Quit...");
            }
            else if (goHunter.GoalFound)
            {
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tYou Won! Press any key to Quit...");
            }
            Console.ReadKey();
        }

        //Thread to move the monsters every 2 seconds
        public static void MoveMonstersThread()
        {
            while (!gbQuit)
            {
                foreach (Monster oMonster in goMonsters.GListMonsters)
                {
                    if (oMonster.TryMove(Randomizer.Instance().Next(4)))
                    {
                        Display.DisplayMap(goMap);
                    }
                }
                Thread.Sleep(2000); //wait 2 seconds before trying to move monsters again
            }
        }

        private static void ProcessPlayerInput(ConsoleKeyInfo userInput)
        {
            switch (userInput.Key)
            {
                case ConsoleKey.Escape:
                    gbQuit = true;
                    break;
                case ConsoleKey.A: //left
                    if (goHunter.Move(goHunter.PosX - 1, goHunter.PosY)) //try moving the hunter left , if possible:
                    {
                        //refresh the display
                        Display.DisplayMap(goMap);
                    }
                    break;
                case ConsoleKey.D: //right
                    if (goHunter.Move(goHunter.PosX + 1, goHunter.PosY)) //try moving the hunter right , if possible:
                    {
                        //refresh the display
                        Display.DisplayMap(goMap);
                    }
                    break;
                case ConsoleKey.W: //up
                    if (goHunter.Move(goHunter.PosX, goHunter.PosY - 1)) //try moving the hunter up , if possible:
                    {
                        //refresh the display
                        Display.DisplayMap(goMap);
                    }
                    break;
                case ConsoleKey.S: //down
                    if (goHunter.Move(goHunter.PosX, goHunter.PosY + 1)) //try moving the hunter down , if possible:
                    {
                        //refresh the display
                        Display.DisplayMap(goMap);
                    }
                    break;
                default:
                    //refresh the display
                    Display.DisplayMap(goMap);
                    break;
            }
        }

        private static int PromptForMapFileIndex()
        {
            Console.WriteLine("\n\tEnter the number of the map you want to play.");
            int iMapIndex = 1; //pay attention default value for map index is one for the first map
            string sValidationError = "";
            do
            {
                //receive user input and try converting it into an integer
                if (int.TryParse(Console.ReadLine(), out iMapIndex))
                {
                    if (iMapIndex < 1)
                    {
                        sValidationError = "File index must be a possitive number.";
                    }
                    else if (iMapIndex > goMap.MapFileNames.Length)
                    {
                        sValidationError = "There aren't that many maps available. Choose a map from the ones listed above.";
                    }
                    else
                    {
                        sValidationError = "";
                    }
                }
                else
                {
                    sValidationError = "You must enter a number.";
                }
                if (sValidationError != "")
                {
                    Console.WriteLine("\t" + sValidationError);
                }
            } while (sValidationError != ""); //repeat only if input is invalid
            return iMapIndex;
        }
 
        public static void PromptPlayerName()
        {
            do
            {
                goHunter.Name = Console.ReadLine();
                if (goHunter.ValidationError != "")
                {
                    Console.WriteLine("\t" + goHunter.ValidationError);
                }
            } while (goHunter.ValidationError != ""); //repeat only if input is invalid
        }
    }
}
