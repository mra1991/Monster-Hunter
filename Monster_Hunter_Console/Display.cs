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
        const int X_OFFSET = 25, Y_OFFSET = 2;

        //prints the 2D array of characters on the screen
        public static void DisplayMap(Map poMap)
        {
            Console.SetCursorPosition(0, 0); //reset cursor position
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

                    DisplayMonsters(poMap.GoMonsters);
                    DisplayHunter(poMap.GoHunter);
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
            else if(poHunter.State is StrongState)
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
            Console.SetCursorPosition(X_OFFSET + poHunter.PosX, Y_OFFSET + poHunter.PosY);
            Console.Write("H");
            Console.ResetColor(); //reset color
            Console.SetCursorPosition(0, 0); //reset cursor position
        }
    }
}
