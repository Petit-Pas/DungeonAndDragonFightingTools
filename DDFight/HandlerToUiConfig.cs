using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDnDCommandHandlers;
using WpfDnDCommandHandlers.AttackCommands.DamageCommands.CalculateDamageResultList;
using WpfDnDCustomControlLibrary.Attacks.Damage;
using WpfDnDCustomControlLibrary.Attacks.HitAttacks;

namespace DDFight
{
    public static class HandlerToUiConfig
    {
        public static void Configure()
        {
            HandlerToUILinker.AddNewPair(typeof(CalculateDamageResultListHandler), new DamageResultListRollableWindow());
        }
    }
}
