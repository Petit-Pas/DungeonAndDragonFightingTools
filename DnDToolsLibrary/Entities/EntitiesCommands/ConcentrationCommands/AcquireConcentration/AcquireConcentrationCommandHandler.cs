using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.AcquireConcentration
{
    public class AcquireConcentrationCommandHandler : SuperCommandHandlerBase<AcquireConcentrationCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(AcquireConcentrationCommand command)
        {
            var entity = command.GetEntity();

            command.WasFocused = entity.IsFocused;

            if (entity.IsFocused)
            {
                // will take care of removing already applied statuses
                var loseConcentrationCommand = new LoseConcentrationCommand(entity.DisplayName);
                _mediator.Value.Execute(loseConcentrationCommand);
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
