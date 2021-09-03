using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputAttackSpellResults
{
    public class GetInputAttackSpellResultsCommand : IMediatorCommand
    {
        private GetInputAttackSpellResultsCommand()
        {
        }

        public GetInputAttackSpellResultsCommand(string spellName, int spellLevel, List<NewAttackSpellResult> spellResults)
        {
            SpellName = spellName;
            SpellLevel = spellLevel;
            SpellResults = spellResults;
        }

        public List<NewAttackSpellResult> SpellResults { get; set; }

        public string SpellName { get; set; }
        public int SpellLevel { get; set; }
    }
}
