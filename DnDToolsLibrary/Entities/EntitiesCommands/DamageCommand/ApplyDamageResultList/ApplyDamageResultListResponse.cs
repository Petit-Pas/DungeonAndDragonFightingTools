using BaseToolsLibrary.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList
{
    public class ApplyDamageResultListResponse : IMediatorCommandResponse
    {
        public int Amount { get; set; } = 0;

        public ApplyDamageResultListResponse()
        {
        }

        public ApplyDamageResultListResponse(int amount)
        {
            Amount = amount;
        }
    }
}
