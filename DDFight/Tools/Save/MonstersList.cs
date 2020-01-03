using DDFight.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Tools.Save
{
    public class MonstersList
    {
        /// <summary>
        ///     The list of Monsters
        /// </summary>
        public List<MonsterDataContext> Monsters = new List<MonsterDataContext>();

        /// <summary>
        ///     Method to add and save a Monster
        /// </summary>
        /// <param name="Monster"></param>
        public void AddMonster(MonsterDataContext Monster)
        {
            Monsters.Add(Monster);
            Save();
        }

        /// <summary>
        ///     Method to remove a Monster, then save
        /// </summary>
        /// <param name="Monster"></param>
        public void RemoveMonster(MonsterDataContext Monster)
        {
            Monsters.Remove(Monster);
            Save();
        }

        /// <summary>
        ///     Method to save Monsters as they are
        /// </summary>
        public void Save()
        {
            SaveManager.SaveMonsters(this);
        }

        /// <summary>
        ///     Method to replace a Monster by a new one
        /// </summary>
        /// <param name="to_update"></param>
        /// <param name="temporary"></param>
        public void Replace(MonsterDataContext to_replace, MonsterDataContext new_Monster)
        {
            for (int i = 0; i != Monsters.Count; i += 1)
            {
                if (to_replace == Monsters[i])
                {
                    Monsters[i] = new_Monster;
                    break;
                }
            }
            Save();
        }
    }
}
