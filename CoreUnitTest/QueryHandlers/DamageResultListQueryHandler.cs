using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.DamageCommands.DamageResultListQueries;
using DnDToolsLibrary.Attacks.Damage;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreUnitTest.QueryHandlers
{
    public class DamageResultListQueryHandler : BaseMediatorHandler<DamageResultListQuery, ValidableResponse<GetInputDamageResultListResponse>>
    {
        public override ValidableResponse<GetInputDamageResultListResponse> Execute(IMediatorCommand genericCommand)
        {
            DamageResultListQuery query = this.castCommand(genericCommand);

            foreach (DamageResult damage in query.DamageList)
            {
                damage.Damage.Roll();
            }

            return new ValidableResponse<GetInputDamageResultListResponse>(true, new GetInputDamageResultListResponse(query.DamageList));
        }

        public override void Undo(IMediatorCommand genericCommand)
        {
        }
    }
}
