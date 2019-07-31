using DDFight.Game;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DDFight.Tools.Save
{
    /// <summary>
    ///     represents the list of the saved characters
    /// </summary>
    public class CharactersList
    {
        /// <summary>
        ///     The list of characters
        /// </summary>
        public List<CharacterDataContext> Characters = new List<CharacterDataContext>();

        /// <summary>
        ///     Method to add and save a character
        /// </summary>
        /// <param name="character"></param>
        public void AddCharacter(CharacterDataContext character)
        {
            Characters.Add(character);
            Save();
        }

        /// <summary>
        ///     Method to remove a character, then save
        /// </summary>
        /// <param name="character"></param>
        public void RemoveCharacter(CharacterDataContext character)
        {
            Characters.Remove(character);
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
            Save();
        }
    }
}
