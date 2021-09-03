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
    public class GetInputNonAttackSpellResultsCommand : IMediatorCommand
    {
        private GetInputNonAttackSpellResultsCommand()
        {
        }

        public GetInputNonAttackSpellResultsCommand(NewNonAttackSpellResult spellTemplate, List<NewNonAttackSpellResult> spellResult)
        {
            foreach (DamageResult damageResult in spellTemplate.HitDamage.Elements)
            {
                // refreshing the damage rolls of the spellEffects depending on the template one.
                damageResult.Damage.PropertyChanged += (e, o) => {
                    foreach (NewNonAttackSpellResult spell in spellResult)
                    {
                        for (int i = 0; i != SpellTemplate.HitDamage.Elements.Count; i += 1)
                        {
                            spell.HitDamage.Elements[i].Damage.LastRoll = spellTemplate.HitDamage.Elements[i].Damage.LastRoll;
                        }
                    }
                };
            }

            SpellTemplate = spellTemplate;
            SpellResults = spellResult;
        }

        /// <summary>
        ///     This one is only the template of the spell itseld
        /// </summary>
        public NewNonAttackSpellResult SpellTemplate { get; set; }
        /// <summary>
        ///     These ones actually contain the targets etc..
        ///     When the damages of the spellTemplate is changed, it should change the damage in spellEffects as well
        /// </summary>
        public List<NewNonAttackSpellResult> SpellResults { get; set; }
    }
}
