namespace DnDToolsLibrary.Fight.Events
{
    // this event is dedicated to any UI that should catch this event => it should not be used to do any domain logic, only UI
    public delegate void FighterSelected(object sender, FighterSelectedEventArgs args);

    public struct FighterSelectedEventArgs
    {
        public FighterSelectedEventArgs(string entityName)
        {
            EntityName = entityName;
        }

        /// <summary>
        ///     The entity that starts its turn
        /// </summary>
        public string EntityName { get; }
    }
}
