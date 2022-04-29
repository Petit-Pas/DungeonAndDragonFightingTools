using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ConcentrationCheckQueries;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ChallengeConcentration
{
    public class ChallengeConcentrationCommandHandler : SuperCommandHandlerBase<ChallengeConcentrationCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(ChallengeConcentrationCommand command)
        {
            PlayableEntity entity = command.GetEntity();

            if (!entity.IsFocused)
            {
                return MediatorCommandStatii.Canceled;
            }

            ConcentrationCheckQuery query = new ConcentrationCheckQuery(command.GetEntityName());
            ValidableResponse<SavingThrow> queryResponse = _mediator.Value.Execute(query) as ValidableResponse<SavingThrow>;

            if (queryResponse.IsValid)
            {
                SavingThrow saving = queryResponse.Response as SavingThrow;
                if (saving.IsFailed)
                {
                    LoseConcentrationCommand loseConcentrationCommand = new LoseConcentrationCommand(command.GetEntityName());
                    command.InnerCommands.Push(loseConcentrationCommand);
                    _mediator.Value.Execute(loseConcentrationCommand);
                }
                return MediatorCommandStatii.Success;
            }
            return MediatorCommandStatii.Canceled;
        }
    }
}
