using BaseToolsLibrary.Mediator;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands
{
    public class TempHealEntityHandler : BaseMediatorHandler<TempHealEntityCommand>
    {
        public override void Execute(IMediatorCommand command)
        {
            TempHealEntityCommand _command = this.cast_command(command);
            PlayableEntity target = _command.GetEntity();

            _command.From = target.TempHp;

            if (_command.Amount > target.TempHp)
                target.TempHp = _command.Amount;

            _command.To = target.TempHp;
        }

        public override void Undo(IMediatorCommand command)
        {
            TempHealEntityCommand _command = this.cast_command(command);

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
