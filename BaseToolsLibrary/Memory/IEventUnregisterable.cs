namespace BaseToolsLibrary.Tools
{
    /// <summary>
    ///     Used to unregister UIElement from their external EventHandler in order to avoid having zombies instance of them
    /// </summary>
    public interface IEventUnregisterable
    {
        void UnregisterToAll();
    }
}
