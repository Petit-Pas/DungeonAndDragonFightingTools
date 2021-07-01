using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.Damage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Attacks.AttacksCommands.DamageCommands.GetInputDamageResultList
{
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
