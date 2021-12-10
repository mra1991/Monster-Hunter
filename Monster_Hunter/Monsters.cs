using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class Monsters
    {
        private List<Monster> gListMonsters;

        public Monsters()
        {
            gListMonsters = new List<Monster>();
        }
        public void AddMonster(Monster poMonster)
        {
            gListMonsters.Add(poMonster);
        }

        public List<Monster> FindMonsters(int piX,int piY)
        {
            List<Monster> listMonstersFound = new List<Monster>();
            foreach(Monster oM in gListMonsters)
            {
                if(oM.PosX==piX && oM.PosY == piY) //the monster is found in position [piX, piY]
                {
                    listMonstersFound.Add(oM);
                }
            }
            return listMonstersFound;
        }
    }
}
