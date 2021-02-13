using DDFight.Game.Entities;

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
