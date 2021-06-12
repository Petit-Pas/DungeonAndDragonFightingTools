using DnDToolsLibrary.Entities;

namespace DnDToolsLibrary.Fight.Events
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

    public delegate void EndTurnEventHandler(object sender, TurnEndedEventArgs args);

    public class TurnEndedEventArgs
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
