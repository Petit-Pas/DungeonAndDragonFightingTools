namespace BaseToolsLibrary.Mediator
{
    /// <summary>
    ///     Describes method to handle history on the mediator itself
    /// </summary>
    interface IHistoryMediator : IMediator
    {
        /// <summary>
        ///     Execute the given command, then pushes it into the history.
        /// </summary>
        /// <param name="command">
        ///     The command to add
        /// </param>
        void AddToHistory(IMediatorCommand command);

        /// <summary>
        ///     Remove last added command in history, undoes it.
        /// </summary>
        /// <returns> 
        ///     The command that was undone
        /// </returns>
        IMediatorCommand UndoLastCommand();
    }
}
