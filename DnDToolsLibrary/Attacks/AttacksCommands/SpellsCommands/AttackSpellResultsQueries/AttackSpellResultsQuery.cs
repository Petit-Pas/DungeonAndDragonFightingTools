using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.AttackSpellResultsQueries
{
    public class AttackSpellResultsQuery : DndCommandBase
    {
        private AttackSpellResultsQuery()
        {
        }

        public AttackSpellResultsQuery(string spellName, int spellLevel, AttackSpellResults spellResults)
        {
            SpellName = spellName;
            SpellLevel = spellLevel;
            SpellResults = spellResults;
        }

        // Even though this is the response of the query, it is sent with some data populated by default (such as the target) 
        // so that the handler has access to everything it needs
        public AttackSpellResults SpellResults { get; set; }

        public string SpellName { get; set; }
        public int SpellLevel { get; set; }
    }
}
