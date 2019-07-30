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
        public List<CharacterDataContext> Characters = new List<CharacterDataContext>();

        public void AddCharacter(CharacterDataContext character)
        {
            Characters.Add(character);
            Save();
        }

        public void RemoveCharacter(CharacterDataContext character)
        {
            Characters.Remove(character);
            Save();
        }

        public void Save()
        {
            SaveManager.SavePlayers(this);
        }
    }
}
