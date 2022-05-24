using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.InspirationCommands.AcquireInspiration;
using DnDToolsLibrary.Entities.EntitiesCommands.InspirationCommands.LoseInspiration;

namespace DnDToolsLibrary.Entities.EntitiesCommands.InspirationCommands.InvertInspiration
{
    public class InvertInspirationCommandHandler : SuperDndCommandHandlerBase<InvertInspirationCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(InvertInspirationCommand command)
        {
            var entity = command.GetEntity();

            if (entity is Monster)
            {
                return MediatorCommandStatii.Canceled;
            }
            else
            {
                var character = entity as Character;
                IMediatorCommand innerCommand;
                if (character.HasInspiration)
                {
                    innerCommand = new LoseInspirationCommand(character.DisplayName);
                }
                else
                {
                    innerCommand = new AcquireInspirationCommand(character.DisplayName);
                }

                Mediator.Execute(innerCommand);
                command.PushToInnerCommands(innerCommand);

                return MediatorCommandStatii.NoResponse;
            }

        }
    }
}
