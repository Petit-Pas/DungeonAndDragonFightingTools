using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.NonAttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.NonAttackSpellResultQueries
{
    public class NonAttackSpellResultsQuery : DndCommandBase
    {
        private NonAttackSpellResultsQuery()
        {
        }

        public NonAttackSpellResultsQuery(NonAttackSpellResult spellTemplate, NonAttackSpellResults spellResults)
        {
            foreach (DamageResult damageResult in spellTemplate.HitDamage)
            {
                // refreshing the damage rolls of the spellEffects depending on the template one.
                damageResult.Damage.PropertyChanged += (e, o) => {
                    foreach (NonAttackSpellResult spell in spellResults)
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
        public NonAttackSpellResult SpellTemplate { get; set; }
        /// <summary>
        ///     These ones actually contain the targets etc..
        ///     When the damages of the spellTemplate is changed, it should change the damage in spellEffects as well
        /// </summary>
        public NonAttackSpellResults SpellResults { get; set; }
    }
}
