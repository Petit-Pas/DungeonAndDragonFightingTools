using BaseToolsLibrary.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseTempHp
{
    public class LooseTempHpHandler : BaseMediatorHandler<LooseTempHpCommand, NoResponse>
    {
        public override NoResponse Execute(IMediatorCommand command)
        {
            LooseTempHpCommand _command = this.cast_command(command);
            PlayableEntity target = _command.GetEntity();

            _command.From = target.TempHp;

            target.TempHp -= _command.Amount;
            if (target.TempHp < 0)
            {
                throw new InvalidOperationException($"WARNING : Trying to remove more TempHps than what {target.DisplayName} has. TempsHps will be set to 0");
            }

            _command.To = target.TempHp;
            return MediatorCommandResponses.NoResponse;
        }

        public override void Undo(IMediatorCommand command)
        {
            LooseTempHpCommand _command = this.cast_command(command);

            if (false == _command.To.HasValue || false == _command.From.HasValue)
            {
                Console.WriteLine($"ERROR : Trying to undo a {this.GetType()} command that was not executed first");
                throw new NullReferenceException($"Trying to undo a {this.GetType()} command that was not executed first");
            }

            PlayableEntity target = _command.GetEntity();
            target.TempHp = _command.From.Value;
        }
    }
}
