using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;
using DnDToolsLibrary.Status;
using System.Linq;
using BaseToolsLibrary.IO;
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

            command.AddLog(command.GetEntityName(), FontWeightProvider.Bold);
            command.AddLog(" lost focus");

            // removes the statuses that were applied by entity AND required concentration
            var statusesToWearOff = StatusProvider.GetOnHitStatusesAppliedBy(entity.DisplayName).Where(x => x.EndsOnCasterLossOfConcentration).ToArray();

            if (statusesToWearOff.Length != 0)
            {
                command.AddLog(", so: \r\n");
                using (new Indenter(3))
                {
                    foreach (var status in statusesToWearOff)
                    {
                        var removeStatusCommand = new RemoveStatusCommand(status.Id, status.Target.DisplayName);
                        command.PushToInnerCommands(removeStatusCommand);
                        Mediator.Execute(removeStatusCommand);
                    }
                }
            }

            command.AddLog("\r\n");

            return MediatorCommandStatii.Success;
        }

        public override void Undo(LoseConcentrationCommand command)
        {
            base.Undo(command);

            var entity = command.GetEntity();

            entity.IsFocused = command.WasFocused;
        }
    }
}
