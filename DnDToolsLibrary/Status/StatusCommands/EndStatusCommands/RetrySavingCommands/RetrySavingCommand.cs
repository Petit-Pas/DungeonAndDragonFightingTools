using System;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.RetrySavingCommands
{
    public class RetrySavingCommand : SuperCommandBase
    {
        public RetrySavingCommand(Guid statusId)
        {
            StatusId = statusId;
        }

        public Guid StatusId { get; set; }
    }
}
