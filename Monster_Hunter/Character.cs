using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class Character
    {
        private string gsValidationError = ""; //global string. Check if this string is empty to make sure you have set the values of properties, properly.
        private int giPosY, giPosX, giHeight, giWidth; //global integers for position of character and the size of the map

        //property to provide public get for validation error
        public string ValidationError { get => gsValidationError; }

        //protected constructor means this object cannot be instantiated and it's meant only to be inherited
        //pi for parameter integers. piPosY is the position of the character from along the y axis from the top of the screen
        protected Character(int piPosY, int piPosX, int piHeight = 0, int piWidth = 0) 
        {
            try
            {
                gsValidationError = "";

                //validating and setting map's dimentions
                if (piHeight < 0) 
                {
                    gsValidationError = "The height of the map cannot be negative.";
                }
                else
                {
                    giHeight = piHeight;
                }
                if (piWidth < 0)
                {
                    gsValidationError = "The width of the map cannot be negative.";
                }
                else
                {
                    giWidth = piWidth;
                }

                //validation of position
                if (piPosY < 0)
                {
                    gsValidationError = "Position of the character along the Y axis cannot be negative.";
                }
                else if (piPosX < 0)
                {
                    gsValidationError = "Position of the character along the X axis cannot be negative.";
                }
                if (piHeight == 0) //if height is not known yet (the map hasn't loaded yet)
                {
                    //set Y position without validating against map's height
                    giPosY = piPosY; 
                }
                else if (piPosY > giHeight)
                {
                    gsValidationError = "Position of the character along the Y axis cannot be greater than the height of the map.";
                }
                else
                {
                    giPosY = piPosY;
                }
                if (piWidth == 0) //if width is not known yer (the map hasn't loaded yet)
                {
                    //set X position without validating against map's width
                    giPosX = piPosX;
                }
                else if (piPosX > giWidth)
                {
                    gsValidationError = "Position of the character along the X axis cannot be greater than the width of the map.";
                }
                else
                {
                    giPosX = piPosX;
                }
            }
            catch(Exception e)
            {
                throw new Exception("<ERROR> ", e);
            }
        }
    }
}
