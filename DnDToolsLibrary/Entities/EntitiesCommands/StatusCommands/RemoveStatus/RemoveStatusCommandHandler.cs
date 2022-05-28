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
            var target = command.GetEntity();

            command.AddLog("Status \'");
            command.AddLog(command.Status.DisplayName, FontWeightProvider.Bold);
            command.AddLog("\' of ");
            command.AddLog(target.DisplayName, FontWeightProvider.Bold);
            command.AddLog(" wears off.\r\n");

            StatusProvider.Remove(command.Status);
            var statusReference = target.AffectingStatusList.First(x => x.ActualStatusReferenceId == command.Status.Id);
            target.AffectingStatusList.Remove(statusReference);

            return MediatorCommandStatii.Success;
        }

        public override void Undo(RemoveStatusCommand command)
        {
            base.Undo(command);

            var target = command.GetEntity();

            StatusProvider.Add(command.Status);
            target.AffectingStatusList.Add(new StatusReference(command.Status));
        }
    }
}
