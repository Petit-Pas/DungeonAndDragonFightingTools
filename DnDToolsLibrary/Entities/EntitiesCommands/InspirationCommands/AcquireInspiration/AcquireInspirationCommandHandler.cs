﻿using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.InspirationCommands.AcquireInspiration
{
    public class AcquireInspirationCommandHandler : DndCommandHandlerBase<AcquireInspirationCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(AcquireInspirationCommand command)
        {
            if (command.GetEntity() is not Character character)
            {
                command.Status = MediatorCommandStatii.Canceled;
            }
            else if (character.HasInspiration)
            {
                command.Status = MediatorCommandStatii.Canceled;
            }
            else
            {
                character.HasInspiration = true;
                command.Status = MediatorCommandStatii.Success;
            }

            return command.Status;
        }

        public override void Undo(AcquireInspirationCommand command)
        {
            base.Undo(command);

            if (command.GetEntity() is Character character && command.Status == MediatorCommandStatii.Success)
                character.HasInspiration = false;
        }
    }
}
