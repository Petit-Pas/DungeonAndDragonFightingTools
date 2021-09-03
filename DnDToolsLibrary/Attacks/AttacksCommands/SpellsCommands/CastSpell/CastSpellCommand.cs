using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpell
{
    public class CastSpellCommand : BaseSuperCommand
    {
        [XmlAttribute]
        public string CasterName { get; set; } = null;

        [XmlAttribute]
        public int CastLevel { get; set; } = -1;

        public List<string> TargetNames { get; set; } = new List<string>();

        public Spell Spell { get; set; } = null;

        public CastSpellCommand(PlayableEntity caster, Spell spell)
        {
            CasterName = caster.DisplayName;
            Spell = spell.Clone() as Spell;
        }
    }
}
