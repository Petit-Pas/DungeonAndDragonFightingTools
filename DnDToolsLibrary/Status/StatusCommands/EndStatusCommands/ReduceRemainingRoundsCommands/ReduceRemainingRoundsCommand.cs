using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
