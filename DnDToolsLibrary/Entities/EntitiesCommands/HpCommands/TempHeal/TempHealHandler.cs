using BaseToolsLibrary.Mediator;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.TempHeal
{
    public class TempHealHandler : BaseMediatorHandler<TempHealCommand, NoResponse>
    {
        public override NoResponse Execute(IMediatorCommand command)
        {
            TempHealCommand _command = this.cast_command(command);
            PlayableEntity target = _command.GetEntity();

            _command.From = target.TempHp;

            if (_command.Amount > target.TempHp)
                target.TempHp = _command.Amount;

            _command.To = target.TempHp;
            return MediatorCommandResponses.NoResponse;
        }

        public override void Undo(IMediatorCommand command)
        {
            TempHealCommand _command = this.cast_command(command);

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
