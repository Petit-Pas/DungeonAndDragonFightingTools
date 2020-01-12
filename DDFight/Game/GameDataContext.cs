using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDFight.Game
{
    /// <summary>
    ///     This class is intended to contain all the generic (and saved) informations:
    ///         - players
    ///         - ennemies schemes
    /// </summary>
    public class GameDataContext
    {
        /// <summary>
        ///     The list of existing character
        /// </summary>
        public CharactersList CharacterList = new CharactersList();

        /// <summary>
        ///     The list of all possible ennemies encountered
        /// </summary>
        public MonstersList MonsterList = new MonstersList();

        /// <summary>
        ///     The list of the entities that shall fight when the Fight button is pressed
        /// </summary>
        [XmlIgnore]
        public ObservableCollection<PlayableEntity> FightingCharacters = new ObservableCollection<PlayableEntity>();
    }
}
