using DDFight.Game;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

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
        public ObservableCollection<CharacterDataContext> Characters = new ObservableCollection<CharacterDataContext>();

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
        public void AddCharacter(CharacterDataContext character)
        {
            Characters.Add(character);
            NotifyPropertyChanged("Characters");
            Save();
        }

        /// <summary>
        ///     Method to remove a character, then save
        /// </summary>
        /// <param name="character"></param>
        public void RemoveCharacter(CharacterDataContext character)
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
            SaveManager.SavePlayers(this);
        }

        /// <summary>
        ///     Method to replace a character by a new one
        /// </summary>
        /// <param name="to_update"></param>
        /// <param name="temporary"></param>
        public void Replace(CharacterDataContext to_replace, CharacterDataContext new_character)
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
