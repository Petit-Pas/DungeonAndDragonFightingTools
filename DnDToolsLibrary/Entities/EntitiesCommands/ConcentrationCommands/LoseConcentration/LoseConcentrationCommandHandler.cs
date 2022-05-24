using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;
using DnDToolsLibrary.Status;
using System.Linq;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration
{
    public class LoseConcentrationCommandHandler : SuperDndCommandHandlerBase<LoseConcentrationCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(LoseConcentrationCommand command)
        {
            PlayableEntity entity = command.GetEntity();

            if (!entity.IsFocused)
            {
                return MediatorCommandStatii.Canceled;
            }
            command.WasFocused = true;
            entity.IsFocused = false;

            // removes the statuses that were applied by entity AND required concentration
            var statuses = StatusProvider.GetOnHitStatusesAppliedBy(entity.DisplayName).Where(x => x.EndsOnCasterLossOfConcentration).ToList();
            foreach (OnHitStatus status in statuses)
            {
                RemoveStatusCommand innerCommand = new RemoveStatusCommand(status.Id, status.Target.DisplayName);
                command.PushToInnerCommands(innerCommand);
                Mediator.Execute(innerCommand);
            }
            return MediatorCommandStatii.Success;
        }

        public override void Undo(LoseConcentrationCommand command)
        {
            PlayableEntity entity = command.GetEntity();

            entity.IsFocused = command.WasFocused;
        }
    }
}
