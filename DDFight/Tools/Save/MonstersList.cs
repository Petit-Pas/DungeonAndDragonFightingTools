using DDFight.Game.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DDFight.Tools.Save
{
    public class MonstersList : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


        /// <summary>
        ///     The list of Monsters
        /// </summary>
        public ObservableCollection<Monster> Monsters
        {
            get => _monsters;
            set
            {
                _monsters = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<Monster> _monsters = new ObservableCollection<Monster>();

        /// <summary>
        ///     Method to add and save a Monster
        /// </summary>
        /// <param name="Monster"></param>
        public void AddMonster(Monster Monster)
        {
            Monsters.Add(Monster);
            Save();
        }

        /// <summary>
        ///     Method to remove a Monster, then save
        /// </summary>
        /// <param name="Monster"></param>
        public void RemoveMonster(Monster Monster)
        {
            Monsters.Remove(Monster);
            Save();
        }

        /// <summary>
        ///     Method to save Monsters as they are
        /// </summary>
        public void Save()
        {
            Monsters.Sort((x, y) => {
                return x.Name.CompareTo(y.Name);
            });

            //SaveManager.SaveMonsters(this);
        }

        /// <summary>
        ///     Method to replace a Monster by a new one
        /// </summary>
        /// <param name="to_update"></param>
        /// <param name="temporary"></param>
        public void Replace(Monster to_replace, Monster new_Monster)
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
