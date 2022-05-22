
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommandHandlers;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.AcquireConcentration;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.InvertConcentration
{
    public class InvertConcentrationCommandHandler : SuperDndCommandHandler<InvertConcentrationCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(InvertConcentrationCommand command)
        {
            IMediatorCommand innerCommand;
            var target = command.GetEntity();

            if (target.IsFocused)
            {
                innerCommand = new LoseConcentrationCommand(target.DisplayName);
            }
            else
            {
                innerCommand = new AcquireConcentrationCommand(target.DisplayName);
            }

            command.PushToInnerCommands(innerCommand);
            Mediator.Execute(innerCommand);

            return MediatorCommandStatii.NoResponse;
        }
    }
}
