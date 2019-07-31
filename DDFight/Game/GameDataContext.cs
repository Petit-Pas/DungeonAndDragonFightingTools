using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
