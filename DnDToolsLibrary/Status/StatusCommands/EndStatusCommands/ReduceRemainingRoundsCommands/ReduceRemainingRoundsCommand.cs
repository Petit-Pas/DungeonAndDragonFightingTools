using System;
using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.ReduceRemainingRoundsCommands
{
    public class ReduceRemainingRoundsCommand : SuperCommandBase
    {
        public ReduceRemainingRoundsCommand(Guid statusId)
        {
            StatusId = statusId;
        }

        public Guid StatusId { get; set; }
    }
}
