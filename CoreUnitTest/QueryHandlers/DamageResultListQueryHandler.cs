using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.DamageCommands.DamageResultListQueries;
using DnDToolsLibrary.Attacks.Damage;

namespace CoreUnitTest.QueryHandlers
{
    public class DamageResultListQueryHandler : BaseMediatorHandler<DamageResultListQuery, ValidableResponse<GetInputDamageResultListResponse>>
    {
        public override ValidableResponse<GetInputDamageResultListResponse> Execute(DamageResultListQuery query)
        {
            foreach (DamageResult damage in query.DamageList)
            {
                damage.Damage.Roll();
            }

            return new ValidableResponse<GetInputDamageResultListResponse>(true, new GetInputDamageResultListResponse(query.DamageList));
        }

        public override void Undo(DamageResultListQuery genericCommand)
        {
        }
    }
}
