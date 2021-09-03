using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputAttackSpellResults
{
    public class GetInputAttackSpellResultsResponse : IMediatorCommandResponse
    {
        private GetInputAttackSpellResultsResponse()
        {

        }

        public GetInputAttackSpellResultsResponse(List<NewAttackSpellResult> spellResults)
        {
            SpellResults = spellResults;
        }

        public List<NewAttackSpellResult> SpellResults { get; set; }
    }
}
