using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Status;
using System.Linq;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus
{
    public class RemoveStatusCommandHandler : DndCommandHandlerBase<RemoveStatusCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(RemoveStatusCommand command)
        {
            PlayableEntity target = command.GetEntity();

            StatusProvider.Remove(command.Status);
            StatusReference statusReference = target.AffectingStatusList.First(x => x.ActualStatusReferenceId == command.Status.Id);
            target.AffectingStatusList.Remove(statusReference);

            return MediatorCommandStatii.Success;
        }

        public override void Undo(RemoveStatusCommand command)
        {
            PlayableEntity target = command.GetEntity();

            StatusProvider.Add(command.Status);
            target.AffectingStatusList.Add(new StatusReference(command.Status));
        }
    }
}
