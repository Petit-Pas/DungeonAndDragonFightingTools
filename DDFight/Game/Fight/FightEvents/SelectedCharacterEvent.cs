using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Fight.FightEvents
{
    public delegate void SelectedCharacterEventHandler(object sender, SelectedCharacterEventArgs args);

    public class SelectedCharacterEventArgs
    {
        /// <summary>
        ///     The selected character
        /// </summary>
        public PlayableEntity Character;

    }
}
