using System;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.RetrySavingCommands
{
    public class RetrySavingCommand : SuperDndCommandBase
    {
        public RetrySavingCommand(Guid statusId)
        {
            StatusId = statusId;
        }

        public Guid StatusId { get; set; }
    }
}
