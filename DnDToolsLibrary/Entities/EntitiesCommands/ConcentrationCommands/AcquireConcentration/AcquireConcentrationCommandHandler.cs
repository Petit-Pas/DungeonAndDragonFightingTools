using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.AcquireConcentration
{
    public class AcquireConcentrationCommandHandler : SuperCommandHandlerBase<AcquireConcentrationCommand, MediatorCommandNoResponse>
    {
        public override MediatorCommandNoResponse Execute(IMediatorCommand command)
        {
            AcquireConcentrationCommand _command = base.castCommand(command);
            PlayableEntity entity = _command.GetEntity();

            _command.WasFocused = entity.IsFocused;

            if (entity.IsFocused)
            {
                // will take care of removing already applied statuses
                LoseConcentrationCommand loseConcentrationCommand = new LoseConcentrationCommand(entity.DisplayName);
                base._mediator.Value.Execute(loseConcentrationCommand);
                _command.PushToInnerCommands(loseConcentrationCommand);
            }

            entity.IsFocused = true;

            return MediatorCommandStatii.NoResponse;
        }

        public override void Undo(IMediatorCommand command)
        {
            AcquireConcentrationCommand _command = base.castCommand(command);
            PlayableEntity entity = _command.GetEntity();

            // resets the possible status linked to the concentration
            base.Undo(command);
            
            // resets the focus status to the previous state
            entity.IsFocused = _command.WasFocused;
        }
    }
}
