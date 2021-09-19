using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.Damage;

namespace DnDToolsLibrary.Attacks.AttacksCommands.DamageCommands.DamageResultListQueries
{
    // TODO refactor with the actual DamageResultList
    public class GetInputDamageResultListResponse : IMediatorCommandResponse
    {
        private GetInputDamageResultListResponse()
        {
        }

        public GetInputDamageResultListResponse(DamageResultList damageResultList)
        {
            DamageResultList = damageResultList;
        }

        public DamageResultList DamageResultList { get; set; }
    }
}
