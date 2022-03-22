﻿using BaseToolsLibrary.Mediator;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseTempHp
{
    public class LooseTempHpHandler : BaseMediatorHandler<LooseTempHpCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(IMediatorCommand genericCommand)
        {
            LooseTempHpCommand _command = this.castCommand(genericCommand);
            PlayableEntity target = _command.GetEntity();

            _command.From = target.TempHp;

            target.TempHp -= _command.Amount;
            if (target.TempHp < 0)
            {
                throw new InvalidOperationException($"WARNING : Trying to remove more TempHps than what {target.DisplayName} has. TempsHps will be set to 0");
            }

            _command.To = target.TempHp;
            return MediatorCommandStatii.NoResponse;
        }

        public override void Undo(IMediatorCommand genericCommand)
        {
            LooseTempHpCommand _command = this.castCommand(genericCommand);

            if (!_command.To.HasValue || 
                !_command.From.HasValue)
            {
                Console.WriteLine($"ERROR : Trying to undo a {this.GetType()} genericCommand that was not executed first");
                throw new NullReferenceException($"Trying to undo a {this.GetType()} genericCommand that was not executed first");
            }

            PlayableEntity target = _command.GetEntity();
            target.TempHp = _command.From.Value;
        }
    }
}
