using System.Collections.Generic;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.BaseCommands
{
    public abstract class SuperDndCommandBase : SuperCommandBase
    {
        public List<int> LogMessages { get; set; } = new ();
    }
}
