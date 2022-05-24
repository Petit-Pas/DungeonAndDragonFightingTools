using BaseToolsLibrary.Mediator;
using System;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseTempHp
{
    public class LooseTempHpHandler : DndCommandHandlerBase<LooseTempHpCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(LooseTempHpCommand command)
        {
            PlayableEntity target = command.GetEntity();

            command.From = target.TempHp;

            target.TempHp -= command.Amount;
            if (target.TempHp < 0)
            {
                throw new InvalidOperationException($"WARNING : Trying to remove more TempHps than what {target.DisplayName} has. TempsHps will be set to 0");
            }

            command.To = target.TempHp;
            return MediatorCommandStatii.NoResponse;
        }

        public override void Undo(LooseTempHpCommand command)
        {
            if (!command.To.HasValue || 
                !command.From.HasValue)
            {
                Console.WriteLine($"ERROR : Trying to undo a {this.GetType()} genericCommand that was not executed first");
                throw new NullReferenceException($"Trying to undo a {this.GetType()} genericCommand that was not executed first");
            }

            PlayableEntity target = command.GetEntity();
            target.TempHp = command.From.Value;
        }
    }
}
