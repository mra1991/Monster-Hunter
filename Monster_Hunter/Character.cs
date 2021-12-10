//Revision history:
//Mohammadreza Abolhassani      2021-12-06      Started creating the character object.
//Mohammadreza Abolhassani      2021-12-09      Finished creating the character object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public abstract class Character
    {
        protected string gsValidationError = ""; //global string. Check if this string is empty to make sure you have set the values of properties, properly.
        protected int giPosY, giPosX, giHeight, giWidth; //global integers for position of character and the size of the map

        //property to provide public get for validation error
        public string ValidationError { get => gsValidationError; }

        //protected constructor means this object cannot be instantiated and it's meant only to be inherited
        //pi for parameter integers. piPosY is the position of the character from along the y axis from the top of the screen
        //position of the character is mandatory in order to create it, but the dimentions of the map are optional and can be set later
        protected Character(int piPosY, int piPosX, int piHeight = 0, int piWidth = 0, Map  poMap = null) 
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

                if (piPosX < 0)
                {
                    gsValidationError = "Position of the character along the X axis cannot be negative.";
                }

                if (piHeight == 0 || piPosY < giHeight) //if height is not known yet (the map hasn't loaded yet) OR position along Y is less than the height
                {
                    //set Y position without validating against map's height
                    giPosY = piPosY;
                }
                else if (piPosY >= giHeight)
                {
                    gsValidationError = "Position of the character along the Y axis cannot be greater than the height of the map minus one.";
                }

                if (piWidth == 0 || piPosX < giWidth) //if width is not known yer (the map hasn't loaded yet) OR position along X is less than the width
                {
                    //set X position without validating against map's width
                    giPosX = piPosX;
                }
                else if (piPosX >= giWidth)
                {
                    gsValidationError = "Position of the character along the X axis cannot be greater than the width of the map minus one.";
                }

                //set the character to full health
                HP = MAX_HP;

                //store a reference to the map to be used in Move(int, int) method.
                goMap = poMap; 
            }
            catch(Exception e)
            {
                throw new Exception("<ERROR> ", e);
            }
        }

        protected Map goMap; //the map that the character is placed and moves
        public Map GoMap { get => goMap; set => goMap = value; } 


        protected int giHP; //global integer for current amount of Health Points
        public const int MAX_HP = 20; //max amount for giHP
        public int HP  //property with public get and protected set with validation for Health Points
        {
            get
            {
                try
                {
                    return giHP;
                }
                catch (Exception e)
                {
                    throw new Exception("<ERROR> ", e);
                }
            }
            protected set
            {
                try
                {
                    //clear the error message
                    gsValidationError = "";

                    /* allow negative valus for HP
                    if (value < 0)
                    {
                        gsValidationError = "The value for HP cannot be negative.";
                    }                   
                    else 
                    */

                    if (value > MAX_HP)
                    {
                        gsValidationError = "The value for HP cannot be more than " + MAX_HP.ToString() + " .";
                    }
                    else
                    {
                        giHP = value;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("<ERROR> ", e);
                }
            }
        } 
        public bool IsDead { get => this.HP <= 0; } // property to check if the character is dead

        protected int giAttack; //global integer for character's Attack Power (or Strength)
        const int MAX_ATTACK = 7; //max amount for giAttack
        public int Attack  //property with public get and protected set with validation for Attack
        {
            get
            {
                try
                {
                    return giAttack;
                }
                catch (Exception e)
                {
                    throw new Exception("<ERROR> ", e);
                }
            }
            protected set
            {
                try
                {
                    //clear the error message
                    gsValidationError = "";

                    if (value < 0)
                    {
                        gsValidationError = "The value for Attack cannot be negative.";
                    }                   
                    else if (value > MAX_ATTACK)
                    {
                        gsValidationError = "The value for Attack cannot be more than " + MAX_ATTACK.ToString() + " .";
                    }
                    else
                    {
                        giAttack = value;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("<ERROR> ", e);
                }
            }
        }

        protected int giArmor; //global integer for character's Armor (or Defense)
        const int MAX_ARMOR = 4; //max amount for giArmor
        public int Armor  //property with public get and protected set with validation for Armor
        {
            get
            {
                try
                {
                    return giArmor;
                }
                catch (Exception e)
                {
                    throw new Exception("<ERROR> ", e);
                }
            }
            protected set
            {
                try
                {
                    //clear the error message
                    gsValidationError = "";

                    if (value < 0)
                    {
                        gsValidationError = "The value for Armor cannot be negative.";
                    }
                    else if (value > MAX_ARMOR)
                    {
                        gsValidationError = "The value for Armor cannot be more than " + MAX_ARMOR.ToString() + " .";
                    }
                    else
                    {
                        giArmor = value;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("<ERROR> ", e);
                }
            }
        }

        protected int giMilliseconsBetweenMoves = 1000; //When the character moves, how long should we wait before we let them move again. Default set to one second.

        public int MilliseconsBetweenMoves { get => giMilliseconsBetweenMoves; set => giMilliseconsBetweenMoves = value; }
        public int PosY { get => giPosY; }
        public int PosX { get => giPosX; }

        //takes an int and subtracts from HP
        //won't do anything for negative values
        public void TakeDamage(int piDamage)
        {
            if (piDamage > 0)
            {
                giHP -= piDamage;
            }
        }

        public abstract bool Move(int piX, int piY);
    }
}
