using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Spells;
using System.Collections.Generic;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.NonAttackSpellResultsQueries
{
    public class NonAttackSpellResultsQuery : IMediatorCommand
    {
        private NonAttackSpellResultsQuery()
        {
        }

        public NonAttackSpellResultsQuery(NewNonAttackSpellResult spellTemplate, NonAttackSpellResults spellResults)
        {
            foreach (DamageResult damageResult in spellTemplate.HitDamage)
            {
                // refreshing the damage rolls of the spellEffects depending on the template one.
                damageResult.Damage.PropertyChanged += (e, o) => {
                    foreach (NewNonAttackSpellResult spell in spellResults)
                    {
                        for (int i = 0; i != SpellTemplate.HitDamage.Count; i += 1)
                        {
                            spell.HitDamage[i].Damage.LastRoll = spellTemplate.HitDamage[i].Damage.LastRoll;
                        }
                    }
                };
            }

            SpellTemplate = spellTemplate;
            SpellResults = spellResults;
        }

        /// <summary>
        ///     This one is only the template of the spell itseld
        /// </summary>
        public NewNonAttackSpellResult SpellTemplate { get; set; }
        /// <summary>
        ///     These ones actually contain the targets etc..
        ///     When the damages of the spellTemplate is changed, it should change the damage in spellEffects as well
        /// </summary>
        public NonAttackSpellResults SpellResults { get; set; }
    }
}
