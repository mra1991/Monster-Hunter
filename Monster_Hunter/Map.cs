//Revision history:
//Mohammadreza Abolhassani      2021-12-05      Created the Map object. Did not finish LoadMapFromFile method (refrence to hunter object and monsters object missing)
//Mohammadreza Abolhassani      2021-12-06      Minor progress made on LoadMapFromFile method.
//Mohammadreza Abolhassani      2021-12-09      Finished LoadMapFromFile method.
//Mohammadreza Abolhassani      2021-12-11      LoadMapFromFile method tested from the console app for the first time and bugs were fixed. first parameter changed form string psFilename to int piMapFileIndex.

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
        const int MAX_HEIGHT = 25, MAX_WIDTH = 80; //constants for validating width and height
        private string[] gsMapFileNames; //global array of strings for holding all the names of all .map files available in the current folder
        public char[,] gcMap; //global two dimentional array of characters, to hold the data contained in the selected map file
        const string ACCEPTABLE_CHAR_LIST = "#HGM whpx"; //when reading map from text file, if a character does not match any of the characters in this string, it is and invalid input.

        public Map()
        {
            try
            {
                //Cache the list of all the names of all .map files available in the current folder
                MapFileNames = System.IO.Directory.GetFiles(@".", "*.map");
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

        private Hunter goHunter;
        private Monsters goMonsters;
        public Hunter GoHunter { get => goHunter; }
        public Monsters GoMonsters { get => goMonsters; }
        private int giMapFileIndex = 0;
        public int MapFileIndex { get => giMapFileIndex; }

        //private boolean method that gets string for filename and loads the map from that file into the two dimentional char array
        //If the map is loaded successfully returns true, otherwise returns false. 
        public bool LoadMapFromFile(int piMapFileIndex, Hunter poHunter, Monsters poMonsters) //parameter string filename must come from the array of strings (gsMapFileNames)  
        {
            try
            {
                string psFilename = MapFileNames[piMapFileIndex]; //retrieve the filename string base on it's index 

                //store all the lines of inside the text file in a local array of strings
                string[] lsLines = System.IO.File.ReadAllLines(psFilename);

                //validation of map dimentions
                Height = lsLines.Length; //the number of lines in the file is the height of the map
                for (int i = 0; i < Height - 1; i++) //for each line in the map file except the last one
                {
                    if (lsLines[i].Length != lsLines[i + 1].Length) //if that line has a different length than the next one
                    {
                        gsValidationError = "Not all lines in the map file " + psFilename + " have the same length.";
                        return false; //halt loading
                    }
                }
                Width = lsLines[1].Length; //the length of any line is the width of the map
                if (ValidationError != "") //check if there was any validation error while setting the dimentions of the map
                {
                    return false; //map file doesn't have the currect format or dimentions exceed limitations. halt loading.
                }

                //now that dimentions are valid
                gcMap = new char[Width, Height]; //create array of character representing the map
                goHunter = poHunter; //attach the parameter object hunter to the map
                goMonsters = poMonsters; //attach the parameter object monsters to the map

                //character validation and loading the chracters into the array
                for (int y = 0; y < Height; y++) //for each line in the map file
                {
                    for (int x = 0; x < Width ; x++) //go through the line character by character
                    {
                        bool isAcceptable = false; //assume the current character is not acceptable
                        for (int k = 0; k < ACCEPTABLE_CHAR_LIST.Length; k++) //go through the list of acceptable characters
                        {
                            if (lsLines[y].ElementAt(x) == ACCEPTABLE_CHAR_LIST.ElementAt(k)) 
                            {
                                //the current character equals one of acceptable characters
                                isAcceptable = true;
                            }
                        }
                        if (isAcceptable) //if current character is acceptable
                        {
                            if (lsLines[y].ElementAt(x) == 'H') //H is for Hunter
                            {
                                //Updade the position of the Hunter
                                poHunter.AssignMap(x, y, Width, Height, this);
                            }
                            else if (lsLines[y].ElementAt(x) == 'M') //M is for Monster
                            {
                                //create a new Monster object at this coordinates and pass it to the monsters onject as a parameter
                                poMonsters.AddMonster(new Monster(y, x, Height, Width, this));
                            }
                            else
                            {
                                //load the character inside the character map
                                gcMap[x, y] = lsLines[y].ElementAt(x);
                            }
                        }
                        else //in case of invalid character in map file
                        {
                            //log the position of the invalid character is error message
                            gsValidationError = "Invalid character in map file " + psFilename + " at line number " + (y + 1).ToString() + " at character number " + (x + 1).ToString() + " .";
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
