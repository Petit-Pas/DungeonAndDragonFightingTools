using DDFight.Game.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DDFight.Tools.Save
{
    /// <summary>
    ///     represents the list of the saved characters
    /// </summary>
    public class CharactersList : INotifyPropertyChanged
    {
        /// <summary>
        ///     The list of characters
        /// </summary>
        public ObservableCollection<Character> Characters
        {
            get => _characters;
            set
            {
                _characters = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<Character> _characters = new ObservableCollection<Character>();

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
        ///     Method to add and save a character
        /// </summary>
        /// <param name="character"></param>
        public void AddCharacter(Character character)
        {
            Characters.Add(character);
            NotifyPropertyChanged("Characters");
            Save();
        }

        /// <summary>
        ///     Method to remove a character, then save
        /// </summary>
        /// <param name="character"></param>
        public void RemoveCharacter(Character character)
        {
            Characters.Remove(character);
            NotifyPropertyChanged("Characters");
            Save();
        }

        /// <summary>
        ///     Method to save characters as they are
        /// </summary>
        public void Save()
        {
            Characters.Sort((x, y) => {
                return x.Name.CompareTo(y.Name);
            });
            SaveManager.SavePlayers(this);
        }

        /// <summary>
        ///     Method to replace a character by a new one
        /// </summary>
        /// <param name="to_update"></param>
        /// <param name="temporary"></param>
        public void Replace(Character to_replace, Character new_character)
        {
            for (int i = 0; i != Characters.Count; i += 1)
            {
                if (to_replace == Characters[i])
                {
                    Characters[i] = new_character;
                    break;
                }
            }
            NotifyPropertyChanged("Characters");
            Save();
        }
    }
}
