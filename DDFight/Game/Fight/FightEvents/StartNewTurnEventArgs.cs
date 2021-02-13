using DDFight.Game.Entities;

namespace DDFight.Game.Fight.FightEvents
{

    public delegate void StartNewTurnEventHandler(object sender, StartNewTurnEventArgs args);

    public class StartNewTurnEventArgs
    {
        /// <summary>
        ///     The character that starts its turn
        /// </summary>
        public PlayableEntity Character;

        /// <summary>
        ///     The index of the character that starts its turn among the Fighter list
        /// </summary>
        public int CharacterIndex;
    }
}
