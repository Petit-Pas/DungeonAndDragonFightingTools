using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputNonAttackSpellResults
{
    public class GetInputNonAttackSpellResultsResponse : IMediatorCommandResponse
    {
        private GetInputNonAttackSpellResultsResponse()
        {
        }

        public GetInputNonAttackSpellResultsResponse(List<NewNonAttackSpellResult> spellResults)
        {
            SpellResults = spellResults;
        }

        public List<NewNonAttackSpellResult> SpellResults { get; set; }
    }
}
