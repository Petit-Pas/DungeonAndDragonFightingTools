using DnDToolsLibrary.Fight.Events;

namespace DnDToolsLibrary.Fight
{
    public interface ITurnManager
    {
        void SetTurnOrders();

        public int TurnIndex { get; set; }

        public uint RoundCount { get; set; }

        // these events are dedicated to any UI that should catch these event => it should not be used to do any domain logic, only UI
        event TurnStarted TurnStarted;
        event TurnEnded TurnEnded;
        
        void InvokeTurnStarted(TurnStartedEventArgs args);
        void InvokeTurnEnded(TurnEndedEventArgs args);
    }
}
