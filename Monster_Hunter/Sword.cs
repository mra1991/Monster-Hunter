//Revision history:
//Mohammadreza Abolhassani      2021-12-05      Created the Sword object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class Sword
    {
        private int giOffenceStrength; //global integer for offence power of the sword
        private const int MIN_OFFENCE = 5, MAX_OFFENCE = 10; //range of offence power
        private const int BREAK_CHANCE_ONE_IN = 5;

        public Sword()
        {
            try
            {
                //generating a random value for offence within the specified range
                giOffenceStrength = Randomizer.Instance().Next(MIN_OFFENCE, MAX_OFFENCE + 1);
            }
            catch (Exception e)
            {
                throw new Exception("<ERROR> ", e);
            }
        }

        //returns true if the sword breaks or false if it does not.
        //generates the return value randomely
        public bool Break()
        {
            try
            {
                if (Randomizer.Instance().Next(BREAK_CHANCE_ONE_IN) == BREAK_CHANCE_ONE_IN - 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception("<ERROR> ", e);
            }
        }


        //property with public getter only, for offence power of the sword
        public int OffenceStrength { get => giOffenceStrength; }
    }
}
