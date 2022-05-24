using System;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.ReduceRemainingRoundsCommands
{
    public class ReduceRemainingRoundsCommand : SuperDndCommandBase
    {
        public ReduceRemainingRoundsCommand(Guid statusId)
        {
            StatusId = statusId;
        }

        public Guid StatusId { get; set; }
    }
}
