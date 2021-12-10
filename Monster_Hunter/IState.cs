//Revision history:
//Mohammadreza Abolhassani      2021-12-09      Created the IState interface.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public interface IState
    {
        void ChangeAttributes(int maxHP, ref int giHP, ref int giAttack, ref int giArmor, ref int freezeTime, ref bool gbIsInvisible);
    }
}
