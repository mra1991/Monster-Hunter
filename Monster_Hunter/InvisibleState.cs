//Revision history:
//Mohammadreza Abolhassani      2021-12-10      Created the InvisibleState object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class InvisibleState : IState
    {

        public InvisibleState() { }
        public void ChangeAttributes(int maxHP, ref int giHP, ref int giAttack, ref int giArmor, ref int freezeTime, ref bool gbIsInvisible)
        {
            gbIsInvisible = false;
        }
    }
}
