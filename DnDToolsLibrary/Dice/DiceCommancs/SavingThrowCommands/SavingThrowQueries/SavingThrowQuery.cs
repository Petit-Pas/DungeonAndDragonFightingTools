using BaseToolsLibrary.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries
{
    public class SavingThrowQuery : IMediatorCommand
    {
        public SavingThrowQuery(SavingThrow saving, string reason)
        {
            SavingThrow = saving;
            Reason = reason;
        }

        public SavingThrow SavingThrow { get; set; }
        public string Reason { get; set; }
    }
}
