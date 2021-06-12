using BaseToolsLibrary.Mediator;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands
{
    public class HealEntityHandler : BaseMediatorHandler<HealEntityCommand>
    {
        public override void Execute(IMediatorCommand command)
        {
            HealEntityCommand _command = this.cast_command(command);
            PlayableEntity target = _command.GetEntity();

            if (_command.Amount < 0)
            {
                Console.WriteLine($"WARNING : Trying to heal {target.DisplayName} for {_command.Amount}, will be set to 0 instead");
                _command.Amount = 0;
            }

            _command.From = target.Hp;

            if (target.Hp + _command.Amount > target.MaxHp)
                target.Hp = (int)target.MaxHp;
            else
                target.Hp += _command.Amount;

            _command.To = target.Hp;
        }

        public override void Undo(IMediatorCommand command)
        {
            HealEntityCommand _command = this.cast_command(command);
            
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
