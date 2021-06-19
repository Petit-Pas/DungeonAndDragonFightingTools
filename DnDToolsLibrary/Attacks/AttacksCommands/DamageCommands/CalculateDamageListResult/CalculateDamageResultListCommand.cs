using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.Damage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Attacks.AttacksCommands.DamageCommands.CalculateDamageResultList
{ 
    public class CalculateDamageResultListCommand : IMediatorCommand, IUiCommand
    {
        public DamageResultList DamageList { get; set; }

        public string Reason { get; set; }

        /// <summary>
        ///     Intended to ask the user to either enter numbers or roll for the given DamageResultList.
        ///     / ! \ As the target is not known by the commands, the eventual resistances should be known in the list beforehand
        /// </summary>
        /// <param name="damages">  </param>
        public CalculateDamageResultListCommand(DamageResultList damages, string reason = "")
        {
            DamageList = damages.Clone() as DamageResultList;
            Reason = reason;
        }
    }
}
