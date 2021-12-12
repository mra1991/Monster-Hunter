//Revision history:
//Mohammadreza Abolhassani      2021-12-11      created the static class Display

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monster_Hunter;

namespace Monster_Hunter_Console
{
    public static class Display
    {
        const int X_OFFSET = 50, Y_OFFSET = 5, INFOS_TO_DISPLAY = 15;

        //prints the 2D array of characters on the screen
        public static void DisplayMap(Map poMap)
        {
            Console.SetCursorPosition(0, 0); //reset cursor position
            Console.Write("                                                                                                             "); //erase whatever is written in the first line
            //display button layout:
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(X_OFFSET, 1);
            Console.Write("Use WASD buttons to move the hunter around.");
            Console.SetCursorPosition(X_OFFSET, 2);
            Console.Write("Press Escape button any time to quit.");
            for (int y = 0; y < poMap.Height; y++) 
            {
                for (int x = 0; x < poMap.Width; x++)
                {
                    //change the foreground color based on what needs to be printed
                    switch (poMap.gcMap[x, y])
                    {
                        case '#':
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            break;
                        case 'G':
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case 'w':
                        case 'h':
                        case 'x':
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case 'p':
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }

                    //set the cursor to the correct position and print the character
                    Console.SetCursorPosition(X_OFFSET + x, Y_OFFSET + y); 
                    Console.Write(poMap.gcMap[x, y]);
                    Console.SetCursorPosition(0, 0); //reset cursor position
                }
            }
            DisplayInfos(poMap);
            DisplayMonsters(poMap.GoMonsters);
            DisplayHunter(poMap.GoHunter);
            Console.ResetColor(); //reset color
            Console.SetCursorPosition(0, 0); //reset cursor position
        }

        private static void DisplayInfos(Map poMap)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(1, 2);
            Console.Write("Player:\t" + poMap.GoHunter.Name);
            Console.SetCursorPosition(1, 3);
            Console.Write("Map:\t\t" + poMap.MapFileNames[poMap.MapFileIndex].Substring(2, poMap.MapFileNames[poMap.MapFileIndex].Length - 6));
            Console.SetCursorPosition(1, 4);
            Console.Write("HP:\t\t" + poMap.GoHunter.HP.ToString() + "  ");
            Console.SetCursorPosition(1, 5);
            Console.Write("Attack:\t" + poMap.GoHunter.Attack.ToString());
            Console.SetCursorPosition(1, 6);
            Console.Write("Armor:\t\t" + poMap.GoHunter.Armor.ToString());
            if (!(poMap.GoHunter.GoGadget is null))
            {
                Console.SetCursorPosition(1, 7);
                Console.Write("Gadget:\t" + poMap.GoHunter.GoGadget.ToString());
            }
            else
            {
                Console.SetCursorPosition(1, 7);
                Console.Write("                                   "); //write blank over what is already there
            }

            Console.SetCursorPosition(1, Y_OFFSET + poMap.Height + 2);
            Console.Write("Infos:");
            for (int i = 0; i < INFOS_TO_DISPLAY; i++)
            {
                if ((poMap.GoHunter.gsAttackLogs.Count - i - 1) >= 0)
                {
                    Console.SetCursorPosition(1, Y_OFFSET + poMap.Height + 3 + i);
                    Console.Write("-----------------------------------------------------------------------------------------------");
                    Console.SetCursorPosition(1, Y_OFFSET + poMap.Height + 3 + i);
                    Console.Write(poMap.GoHunter.gsAttackLogs.ElementAt(poMap.GoHunter.gsAttackLogs.Count - i - 1));
                }
            }
            Console.ResetColor(); //reset color
            Console.SetCursorPosition(0, 0); //reset cursor position
        }

        //Display a red M wherever there is a monster
        private static void DisplayMonsters(Monsters poMonsters)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (Monster oMonster in poMonsters.GListMonsters)
            {
                Console.SetCursorPosition(X_OFFSET + oMonster.PosX, Y_OFFSET + oMonster.PosY);
                Console.Write("M");
            }
            Console.ResetColor(); //reset color
            Console.SetCursorPosition(0, 0); //reset cursor position
        }


        //Display a H where the hunter is
        private static void DisplayHunter(Hunter poHunter)
        {
            //set the color according to hunter's state
            if(poHunter.State is NormalState)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            if(poHunter.State is StrongState)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            else if(poHunter.State is PoisonedState)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else if(poHunter.State is InvisibleState)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else if(poHunter.State is FastState)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.SetCursorPosition(1, 8);
            Console.Write("Hunter's state:" + poHunter.State.ToString() + "    ");

            Console.SetCursorPosition(X_OFFSET + poHunter.PosX, Y_OFFSET + poHunter.PosY);
            Console.Write("H");
           
            Console.ResetColor(); //reset color
            Console.SetCursorPosition(0, 0); //reset cursor position
        }
    }
}
