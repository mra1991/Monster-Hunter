//Revision history:
//Mohammadreza Abolhassani      2021-12-09      Created the NormalState object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class NormalState : IState
    {
        //integers to store the original values for health points, attack, armor and freezetime
        private int origHP, origAttack, origArmor, origFreezeTime; 
        public NormalState(int piHP, int piAttck, int piArmor, int piFreezeTime)
        {
            this.origHP = piHP;
            this.origAttack = piAttck;
            this.origArmor = piArmor;
            this.origFreezeTime = piFreezeTime;
        }
        public void ChangeAttributes(int maxHP, ref int giHP, ref int giAttack, ref int giArmor, ref int freezeTime, ref bool gbIsInvisible)
        {
            try
            {
                //change health points, attack, armor and freezetime back to their original values and make hunter visible
                giHP = this.origHP;
                giAttack = this.origAttack;
                giArmor = this.origArmor;
                freezeTime = this.origFreezeTime;
                gbIsInvisible = false;
            }
            catch(Exception e)
            {
                throw new Exception("<ERROR> ", e);
            }
        }
    }
}
