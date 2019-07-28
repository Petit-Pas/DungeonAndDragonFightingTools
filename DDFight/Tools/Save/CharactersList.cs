using DDFight.Game;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DDFight.Tools.Save
{
    public class CharactersList
    {
        public List<CharacterDataContext> Characters = new List<CharacterDataContext> ();

        internal void AddCharacter(CharacterDataContext character)
        {
            Characters.Add(character);
            SaveManager.SavePlayers(this);
        }
    }
}
