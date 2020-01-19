using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Fight.FightEvents
{
    public delegate void EndTurnEventHandler(object sender, EndTurnEventArgs args);

    public class EndTurnEventArgs
    {
        /// <summary>
        ///     The character that ends its turn
        /// </summary>
        public PlayableEntity Character;

        /// <summary>
        ///     The index of the character that ends its turn among the Fighter list
        /// </summary>
        public int CharacterIndex;
    }
}
