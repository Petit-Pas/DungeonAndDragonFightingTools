using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.AcquireConcentration;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ConcentrationCheckQueries;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ChallengeConcentration
{
    public class ChallengeConcentrationCommandHandler : SuperCommandHandlerBase<ChallengeConcentrationCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(IMediatorCommand genericCommand)
        {
            ChallengeConcentrationCommand _command = genericCommand as ChallengeConcentrationCommand;
            PlayableEntity entity = _command.GetEntity();

            if (!entity.IsFocused)
            {
                return MediatorCommandStatii.Canceled;
            }

            ConcentrationCheckQuery query = new ConcentrationCheckQuery(_command.GetEntityName());
            ValidableResponse<SavingThrow> queryResponse = this._mediator.Value.Execute(query) as ValidableResponse<SavingThrow>;

            if (queryResponse.IsValid)
            {
                SavingThrow saving = queryResponse.Response as SavingThrow;
                if (saving.IsFailed)
                {
                    LoseConcentrationCommand loseConcentrationCommand = new LoseConcentrationCommand(_command.GetEntityName());
                    _command.InnerCommands.Push(loseConcentrationCommand);
                    this._mediator.Value.Execute(loseConcentrationCommand);
                }
                return MediatorCommandStatii.Success;
            }
            return MediatorCommandStatii.Canceled;
        }
    }
}
