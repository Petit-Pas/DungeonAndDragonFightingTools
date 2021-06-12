using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseToolsLibrary.Mediator
{

    /// <summary>
    ///     will serve to do history, for a fight for instance
    /// </summary>
    public class BaseHistoryMediator : BaseMediator, IHistoryMediator
    {
        private Stack<IMediatorCommand> _history = new Stack<IMediatorCommand>();

        public void AddToHistory(IMediatorCommand command)
        {
            base.Execute(command);
            _history.Push(command);
        }

        public IMediatorCommand UndoLastCommand()
        {
            IMediatorCommand command = _history.Pop();
            base.Undo(command);
            return command;
        }
    }
}
