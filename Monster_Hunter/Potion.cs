//Revision history:
//Mohammadreza Abolhassani      2021-12-05      Created the Potion object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public enum POTION_TYPE { STRENGTH_POTION, POISON_POTION, INVISIBILITY_POTION, SPEED_POTION };
    public class Potion
    {
        private POTION_TYPE geType; //global enumeration for type of the potion

        public Potion()
        {
            try
            {
                //set the type of the potion randomly with a roll of dice:
                //1=poison potion, 2-3=speed potion, 4-5=invisibility potion, 6=strength potion
                switch (Randomizer.RollDice())
                {
                    case 1:
                        geType = POTION_TYPE.POISON_POTION;
                        break;
                    case 2:
                    case 3:
                        geType = POTION_TYPE.SPEED_POTION;
                        break;
                    case 4:
                    case 5:
                        geType = POTION_TYPE.INVISIBILITY_POTION;
                        break;
                    case 6:
                        geType = POTION_TYPE.STRENGTH_POTION;
                        break;
                    default:
                        break;
                }
            }
            catch(Exception e)
            {
                throw new Exception("<ERROR> ", e);
            }
        }

        //public getter only. 
        public POTION_TYPE Type { get => geType; }
    }
}
