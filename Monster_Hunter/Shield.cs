//Revision history:
//Mohammadreza Abolhassani      2021-12-05      Created the Shield object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class Shield : IGadget
    {
        private int giDefenceStrength; //global integer for defence power of the shield
        private const int MIN_DEFENCE = 3, MAX_DEFENCE = 6; //range of defence power
        private const int BREAK_CHANCE_ONE_IN = 4;

        public Shield()
        {
            try
            {
                //generating a random value for defence within the specified range
                giDefenceStrength = Randomizer.Instance().Next(MIN_DEFENCE, MAX_DEFENCE + 1);
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


        //property with public getter only, for offence power of the shield
        public int DefenceStrength { get => giDefenceStrength; }
    }
}
