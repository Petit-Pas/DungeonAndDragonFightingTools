using BaseToolsLibrary.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands
{
    public class LooseHpHandler : BaseMediatorHandler<LooseHpCommand>
    {
        public override void Execute(IMediatorCommand command)
        {
            LooseHpCommand _command = this.cast_command(command);
            PlayableEntity target = _command.GetEntity();

            _command.From = target.Hp;

            target.Hp -= _command.Amount;
            if (target.Hp < 0)
                target.Hp = 0;

            _command.To = target.Hp;
        }

        public override void Undo(IMediatorCommand command)
        {
            LooseHpCommand _command = this.cast_command(command);

            if (false == _command.To.HasValue || false == _command.From.HasValue)
            {
                Console.WriteLine($"ERROR : Trying to undo a {this.GetType()} command that was not executed first");
                throw new NullReferenceException($"Trying to undo a {this.GetType()} command that was not executed first");
            }

            PlayableEntity target = _command.GetEntity();
            target.Hp = _command.From.Value;
        }
    }
}
