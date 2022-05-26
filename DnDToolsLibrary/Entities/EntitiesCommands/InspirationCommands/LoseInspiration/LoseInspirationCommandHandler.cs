using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.InspirationCommands.LoseInspiration
{
    internal class LoseInspirationCommandHandler : DndCommandHandlerBase<LoseInspirationCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(LoseInspirationCommand command)
        {
            var character = command.GetEntity() as Character;

            if (character == null)
            {
                command.Status = MediatorCommandStatii.Canceled;
            }
            else if (!character.HasInspiration)
            {
                command.Status = MediatorCommandStatii.Canceled;
            }
            else
            {
                character.HasInspiration = false;
                command.Status = MediatorCommandStatii.Success;
            }

            return command.Status;
        }

        public override void Undo(LoseInspirationCommand command)
        {
            base.Undo(command);

            var character = command.GetEntity() as Character;

            if (command.Status == MediatorCommandStatii.Success)
                character.HasInspiration = true;
        }
    }
}
