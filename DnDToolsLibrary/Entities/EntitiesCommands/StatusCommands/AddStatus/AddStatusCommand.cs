using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.AddStatus
{
    // this only adds the status to the entity list, the OnApplyDamageList is not used here
    public class AddStatusCommand : EntityCommand
    {
        public AddStatusCommand(PlayableEntity target, OnHitStatus status) : base(target.DisplayName)
        {
            Status = status;
        }

        public AddStatusCommand(string targetName, OnHitStatus status) : base(targetName)
        {
            Status = status;
        }

        public OnHitStatus Status { get; }
    }
}
