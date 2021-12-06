//Revision history:
//Mohammadreza Abolhassani      2021-12-05      Created the Randomizer object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class Randomizer : Random
    {
        //singleton instance
        private static Randomizer instance = null;

        private Randomizer(): base()
        {

        }


        //implements singleton design and provides public access to the singleton instance
        public static Randomizer Instance()
        {
            try
            {
                if (instance == null)
                {
                    instance = new Randomizer();
                }
                return instance;
            }
            catch(Exception e)
            {
                throw new Exception("<ERROR> ", e);
            }
        }

        //the following method returns a random integer between 1 and six
        public static int RollDice()
        {
            return Instance().Next(6) + 1;
        }
    }
}
