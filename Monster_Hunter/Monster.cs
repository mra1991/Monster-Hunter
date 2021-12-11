//Revision history:
//Mohammadreza Abolhassani      2021-12-06      Started creating the monster object.
//Mohammadreza Abolhassani      2021-12-09      Finished creating the monster object.
//Mohammadreza Abolhassani      2021-12-11      Added the (bool TryMove(int)) Method.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class Monster : Character
    {
        public Monster(int piPosY, int piPosX, int piHeight = 0, int piWidth = 0, Map poMap = null) : base(piPosY, piPosX, piHeight, piWidth, poMap)
        {
            this.MilliseconsBetweenMoves = 2000; //default freezeTime is 2 seconds
        }


        //feed this method with a random number between 0 an 3 for random direction index
        //it will try moving the monster in four directions starting with piSrartdirectionIndex
        public bool TryMove(int piStartDirectionIndex)
        {
            for (int i = 0; i < 4; i++) //to try all four directions
            {
                //Direction Index =  ( (0 for LEFT) , (1 for RIGHT) , (2 for UP) , (3 for DOWN) )
                switch (piStartDirectionIndex)
                {
                    case 0: //Try moving the monster left
                        if (this.Move(giPosX - 1, giPosY))
                        {
                            return true;
                        }
                        break;
                    case 1: //Try moving the monster right
                        if (this.Move(giPosX + 1, giPosY))
                        {
                            return true;
                        }
                        break;
                    case 2: //Try moving the monster up
                        if (this.Move(giPosX, giPosY - 1))
                        {
                            return true;
                        }
                        break;
                    case 3: //Try moving the monster down
                        if (this.Move(giPosX, giPosY + 1))
                        {
                            return true;
                        }
                        break;
                    default:
                        break;
                }
                piStartDirectionIndex++; //try next direction index
                piStartDirectionIndex %= 4; //make sure direction index stays between 0 and 3
            }
            return false;
        }

        public override bool Move(int piX, int piY)
        {
            // if new coordinates are out of bounds, the move won't be possible
            if (piX < 0 || piY < 0 || piX > giWidth - 1 || piY > giHeight - 1)
            {
                return false;
            }

            //if there is a wall in the new position
            if(GoMap.gcMap[piX, piY] == '#')
            {
                return false; //can't move there
            }

            //check if the hunter is there
            if(GoMap.GoHunter.PosX == piX && GoMap.GoHunter.PosY == piY)
            {
                string attackLog = "A monster just attacked the hunter ";
                GoMap.GoHunter.TakeDamage(this.Attack);
                if (GoMap.GoHunter.IsDead)
                {
                    attackLog += "and killed him.";
                }
                else
                {
                    attackLog += ("... Remaining HP = " + GoMap.GoHunter.HP.ToString());
                }
                GoMap.GoHunter.gsAttackLogs.Add(attackLog);
            }

            //if there is no wall there, the monster can move there
            this.giPosX = piX; //update hunter's position along x axis
            this.giPosY = piY; //update hunter's position along y axis
            return true; //moved successfully
        }
    }
}
