//Revision history:
//Mohammadreza Abolhassani      2021-12-09      Created the StrongState object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class PoisonedState : IState
    {
        public PoisonedState()
        {

        }
        public void ChangeAttributes(int maxHP, ref int giHP, ref int giAttack, ref int giArmor, ref int freezeTime, ref bool gbIsInvisible)
        {
            giHP -= 5; //health drops by 5
            giAttack /= 2; //attack decreases 50%
            giArmor /= 2; //aromor decreases 50%
            freezeTime *= 5; freezeTime /= 4; //increase freezTime by 25%
        }
    }
}