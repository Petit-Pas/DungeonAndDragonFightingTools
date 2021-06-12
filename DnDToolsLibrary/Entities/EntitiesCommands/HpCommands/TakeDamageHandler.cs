using BaseToolsLibrary.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands
{
    public class TakeDamageHandler : BaseSuperHandler<TakeDamageCommand>
    {
        public override void Execute(IMediatorCommand command)
        {
            TakeDamageCommand _command = base.cast_command(command);

            if (_command.Amount == 0)
                return;

            PlayableEntity target = _command.GetEntity();
            int remaining = _command.Amount;

            if (target.TempHp != 0)
                remaining = handleTempHp(_command, target, remaining);
            handleHp(_command, target, remaining);
        }

        private void handleHp(TakeDamageCommand command, PlayableEntity target, int remaining)
        {
            LooseHpCommand inner_command = new LooseHpCommand(target, remaining);

            _mediator.Value.Execute(inner_command);
            command.AddToInnerCommands(inner_command);
        }

        private int handleTempHp(TakeDamageCommand command, PlayableEntity target, int remaining)
        {
            int amount = remaining < target.TempHp ? remaining : target.TempHp;

            LooseTempHpCommand inner_command = new LooseTempHpCommand(target, amount);

            _mediator.Value.Execute(inner_command);
            command.AddToInnerCommands(inner_command);

            return remaining - amount;

        }
    }
}
