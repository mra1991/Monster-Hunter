//Revision history:
//Mohammadreza Abolhassani      2021-12-09      Created the StrongState object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class StrongState : IState
    {
        public StrongState()
        {

        }
        public void ChangeAttributes(int maxHP, ref int giHP, ref int giAttack, ref int giArmor, ref int freezeTime, ref bool gbIsInvisible)
        {
            giHP = maxHP; //health to full
            giAttack *= 2; //attack to 2 times power
            giArmor *= 3; giArmor /= 2; //aromor to 1.5 times power
        }
    }
}
