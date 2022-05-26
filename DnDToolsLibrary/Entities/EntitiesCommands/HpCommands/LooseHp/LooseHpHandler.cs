using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ChallengeConcentration;
using System;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseHp
{
    public class LooseHpHandler : SuperDndCommandHandlerBase<LooseHpCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(LooseHpCommand command)
        {
            PlayableEntity target = command.GetEntity();

            command.From = target.Hp;

            target.Hp -= command.Amount;
            if (target.Hp < 0)
                target.Hp = 0;

            if (target.IsFocused && (target.Hp <= 0 || command.Amount > 0))
            {
                ChallengeConcentrationCommand concentrationCommand = new ChallengeConcentrationCommand(target.DisplayName);
                Mediator.Execute(concentrationCommand);
                command.PushToInnerCommands(concentrationCommand);
            }

            command.To = target.Hp;
            return MediatorCommandStatii.NoResponse;
        }

        public override void Undo(LooseHpCommand command)
        {
            base.Undo(command);

            if (!command.To.HasValue || 
                !command.From.HasValue)
            {
                Console.WriteLine($"ERROR : Trying to undo a {this.GetType()} genericCommand that was not executed first");
                throw new NullReferenceException($"Trying to undo a {this.GetType()} genericCommand that was not executed first");
            }

            var target = command.GetEntity();
            target.Hp = command.From.Value;
        }
    }
}
