using System.Collections.Generic;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.BaseCommands
{
    public abstract class DndCommandBase : IMediatorCommand
    {
        public List<int> LogMessages { get; set; } = new ();
    }
}
