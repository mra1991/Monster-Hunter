//Revision history:
//Mohammadreza Abolhassani      2021-12-10      Created the FastState object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class FastState : IState
    {
        public FastState() { }
        public void ChangeAttributes(int maxHP, ref int giHP, ref int giAttack, ref int giArmor, ref int freezeTime, ref bool gbIsInvisible)
        {
            //decrease freezeTime by 50%
            freezeTime /= 2;
        }

        public override string ToString()
        {
            return "Fast";
        }
    }
}
