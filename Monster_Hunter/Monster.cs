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

        public override bool Move(int piX, int piY)
        {
            // if new coordinates are out of bounds, the move won't be possible
            if (piX < 0 || piY < 0 || piX > -1 || piY > giHeight - 1)
            {
                return false;
            }

            //if there is a wall in the new position
            if(GoMap.gcMap[piX, piY] == '#')
            {
                return false; //can't move there
            }

            //check if the hunter is there
            //....

            //if there is not hunter and wall there, the monster can move freely
            this.giPosX = piX; //update hunter's position along x axis
            this.giPosY = piY; //update hunter's position along y axis
            return true; //moved successfully
        }
    }
}
