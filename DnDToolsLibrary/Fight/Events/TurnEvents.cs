namespace DnDToolsLibrary.Fight.Events
{
    public delegate void StartNewTurnEventHandler(object sender, StartNewTurnEventArgs args);

    public struct StartNewTurnEventArgs
    {
        public StartNewTurnEventArgs(string entityName)
        {
            EntityName = entityName;
        }

        /// <summary>
        ///     The entity that starts its turn
        /// </summary>
        public string EntityName { get; }
    }

    public delegate void EndTurnEventHandler(object sender, TurnEndedEventArgs args);

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
