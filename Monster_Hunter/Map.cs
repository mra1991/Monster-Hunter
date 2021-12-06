//Revision history:
//Mohammadreza Abolhassani      2021-12-05      Created the Map object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class Map
    {
        private string gsValidationError = ""; //global string. Check if this string is empty to make sure you have set the values of properties, properly.
        private int giHeight = 0, giWidth = 0; //global integers for height and width of the map
        const int MAX_HEIGHT = 25, MAX_WIDTH = 0; //constants for validating width and height
        private string[] gsMapFileNames; //global array of strings for holding all the names of all .map files available in the current folder
        public char[,] gcMap; //global two dimentional array of characters, to hold the data contained in the selected map file
        const string ACCEPTABLE_CHAR_LIST = "#HGM whpx"; //when reading map from text file, if a character does not match any of the characters in this string, it is and invalid input.

        public Map()
        {
            try
            {
                //Cache the list of all the names of all .map files available in the current folder
                MapFileNames = System.IO.Directory.GetFiles(".", "*.map");
            }
            catch (Exception e)
            {
                throw new Exception("<ERROR> ", e);
            }
        }

        public int Height  //property with public get and private set with validation
        {
            get
            {
                try
                {
                    return giHeight;
                }
                catch (Exception e)
                {
                    throw new Exception("<ERROR> ", e);
                }
            }
            private set
            {
                try
                {
                    gsValidationError = ""; //clear the error message
                    if (value < 0)
                    {
                        gsValidationError = "The value for map's height cannot be negative.";
                    }
                    else if (value > MAX_HEIGHT)
                    {
                        gsValidationError = "The value for map's height cannot be more than " + MAX_HEIGHT.ToString() + " .";
                    }
                    else
                    {
                        giHeight = value;
                    }

                }
                catch (Exception e)
                {
                    throw new Exception("<ERROR> ", e);
                }
            }
        }

        public int Width //property with public get and private set with validation
        {
            get
            {
                try
                {
                    return giWidth;
                }
                catch (Exception e)
                {
                    throw new Exception("<ERROR> ", e);
                }
            }
            private set
            {
                try
                {
                    gsValidationError = ""; //clear the error message
                    if (value < 0)
                    {
                        gsValidationError = "The value for map's width cannot be negative.";
                    }
                    else if (value > MAX_WIDTH)
                    {
                        gsValidationError = "The value for map's width cannot be more than " + MAX_WIDTH.ToString() + " .";
                    }
                    else
                    {
                        giWidth = value;
                    }

                }
                catch (Exception e)
                {
                    throw new Exception("<ERROR> ", e);
                }
            }
        }

        //property to provide public get for validation error
        public string ValidationError { get => gsValidationError; }

        //property to provide public get and private set for the array of string containing the list of .map filenames.
        public string[] MapFileNames { get => gsMapFileNames; private set => gsMapFileNames = value; }

        //private boolean method that gets string for filename and loads the map from that file into the two dimentional char array
        //If the map is loaded successfully returns true, otherwise returns false. 
        private bool LoadMapFromFile(string psFilename) //parameter string filename must come from the array of strings (gsMapFileNames)  
        {
            try
            {
                //store all the lines of inside the text file in a local array of strings
                string[] lsLines = System.IO.File.ReadAllLines(psFilename);

                //validation of map dimentions
                Height = lsLines.Length; //the number of lines in the file is the height of the map
                Width = lsLines[0].Length; //the length of any line is the width of the map
                if (ValidationError != "") //check if there was any validation error while setting the dimentions of the map
                {
                    return false; //map file doesn't have the currect format or dimentions exceed limitations. halt loading.
                }
                //now that dimentions are valid
                gcMap = new char[Height, Width]; //create array of character representing the map

                //character validation and loading the chracters into the array
                for (int i = 0; i < lsLines.Length; i++) //for each line in the map file
                {
                    for (int j = 0; j < lsLines[i].Length ; j++) //go through the line character by character
                    {
                        bool isAcceptable = false; //assume the current character is not acceptable
                        for (int k = 0; k < ACCEPTABLE_CHAR_LIST.Length; k++) //go through the list of acceptable characters
                        {
                            if (lsLines[i].ElementAt(j) == ACCEPTABLE_CHAR_LIST.ElementAt(k)) 
                            {
                                //the current character equals one of acceptable characters
                                isAcceptable = true;
                            }
                        }
                        if (isAcceptable) //if current character is acceptable
                        {
                            if (lsLines[i].ElementAt(j) == 'H') //H is for Hunter
                            {
                                //Updade the position of the Hunter
                                //..
                                //..
                            }
                            else if (lsLines[i].ElementAt(j) == 'M') //M is for Monster
                            {
                                //create a new Monster object at this coordinates and pass it to the monsters onject as a parameter
                                //...
                                //...
                            }
                            else
                            {
                                //load the character inside the character map
                                gcMap[i, j] = lsLines[i].ElementAt(j);
                            }
                        }
                        else //in case of invalid character in map file
                        {
                            //log the position of the invalid character is error message
                            gsValidationError = "Invalid character in map file " + psFilename + " at line number " + (i + 1).ToString() + " at character number " + (j + 1).ToString() + " .";
                            return false; //halt loading
                        }
                    }
                }
                return true; //map loaded successfully
            }
            catch (Exception e)
            {
                throw new Exception("<ERROR> ", e);
            }
        }
    }
}
