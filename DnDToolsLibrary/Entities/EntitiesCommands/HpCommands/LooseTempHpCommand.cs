using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands
{
    public class LooseTempHpCommand : EntityCommand
    {
        public int? From { get; set; }
        public int? To { get; set; }
        public int Amount { get; set; }

        public LooseTempHpCommand(PlayableEntity entity, int amount) : base(entity.DisplayName)
        {
            Amount = amount;
        }
    }
}
