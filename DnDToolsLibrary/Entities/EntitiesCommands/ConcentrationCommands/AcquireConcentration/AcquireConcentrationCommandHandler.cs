using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommandHandlers;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.AcquireConcentration
{
    public class AcquireConcentrationCommandHandler : SuperDndCommandHandler<AcquireConcentrationCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(AcquireConcentrationCommand command)
        {
            var entity = command.GetEntity();

            command.WasFocused = entity.IsFocused;

            if (entity.IsFocused)
            {
                // will take care of removing already applied statuses
                var loseConcentrationCommand = new LoseConcentrationCommand(entity.DisplayName);
                Mediator.Execute(loseConcentrationCommand);
                command.PushToInnerCommands(loseConcentrationCommand);
            }

            entity.IsFocused = true;

            return MediatorCommandStatii.NoResponse;
        }

        public override void Undo(AcquireConcentrationCommand command)
        {
            var entity = command.GetEntity();

            // resets the possible status linked to the concentration
            base.Undo(command);
            
            // resets the focus status to the previous state
            entity.IsFocused = command.WasFocused;
        }
    }
}
