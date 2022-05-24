using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.AttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.Spells;
using System.Collections.Generic;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands
{
    public class CastAttackSpellCommand : SuperDndCommandBase
    {
        public CastAttackSpellCommand(string casterName, Spell spell, int castLevel, List<string> targetNames)
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

        public AttackSpellResults SpellResults { get; set; }
    }
}
