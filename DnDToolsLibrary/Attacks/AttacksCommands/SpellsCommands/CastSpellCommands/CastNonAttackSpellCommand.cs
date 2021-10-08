using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.NonAttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.Spells;
using System.Collections.Generic;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands
{
    public class CastNonAttackSpellCommand : SuperCommandBase
    {
        public CastNonAttackSpellCommand(string casterName, Spell spell, int castLevel, List<string> targetNames)
        {
            CasterName = casterName;
            Spell = spell;
            CastLevel = castLevel;
            TargetNames = targetNames;
        }

        public string CasterName { get; }
        public Spell Spell { get; }
        
        public int CastLevel { get; }
        public List<string> TargetNames { get; }

        public NonAttackSpellResults SpellResults { get; set; }
    }
}
