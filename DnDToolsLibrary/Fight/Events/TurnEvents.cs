namespace DnDToolsLibrary.Fight.Events
{
    // this event is dedicated to any UI that should catch this event => it should not be used to do any domain logic, only UI
    public delegate void TurnStarted(object sender, TurnStartedEventArgs args);

    public struct TurnStartedEventArgs
    {
        public TurnStartedEventArgs(string entityName)
        {
            EntityName = entityName;
        }

        /// <summary>
        ///     The entity that starts its turn
        /// </summary>
        public string EntityName { get; }
    }

    // this event is dedicated to any UI that should catch this event => it should not be used to do any domain logic, only UI
    public delegate void TurnEnded(object sender, TurnEndedEventArgs args);

    public struct TurnEndedEventArgs
    {
        public TurnEndedEventArgs(string entityName)
        {
            EntityName = entityName;
        }

        /// <summary>
        ///     The Entity that ends its turn
        /// </summary>
        public string EntityName { get; }
    }
}
