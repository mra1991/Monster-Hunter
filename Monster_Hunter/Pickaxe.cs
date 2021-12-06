//Revision history:
//Mohammadreza Abolhassani      2021-12-05      Created the Pickaxe object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class Pickaxe
    {
        private const int BREAK_CHANCE_ONE_IN = 3;

        public Pickaxe()
        {
            try
            {
               
            }
            catch (Exception e)
            {
                throw new Exception("<ERROR> ", e);
            }
        }

        //returns true if the pickaxe breaks or false if it does not.
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
    }
}
