using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using DDFight.Commands;
using DDFight.Commands.AttackCommands;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities;
using System.Collections.Generic;
using WpfDnDCustomControlLibrary.Attacks.Spells;

namespace TempExtensionsAttackSpellResultExtensions
{
    public static class AttackSpellResultExtensions
    {
        private static ICustomConsole console = DIContainer.GetImplementation<ICustomConsole>();
        private static IFontWeightProvider fontWeightProvider = DIContainer.GetImplementation<IFontWeightProvider>();

        public static void Cast(this AttackSpellResult attackSpellResult, List<SpellAttackResultRollableUserControl> attacks)
        {

            console.AddEntry($"{attackSpellResult.Caster.DisplayName}", fontWeightProvider.Bold);
            console.AddEntry(" casts a lvl ", fontWeightProvider.Bold);
            console.AddEntry($"{attackSpellResult.Level} {attackSpellResult.Name}\r\n", fontWeightProvider.Bold);

            int index = 0;
            foreach (PlayableEntity target in attackSpellResult.Targets)
            {
                SpellAttackResultRollableUserControl attack = attacks[index];
                HitAttackResult result = new HitAttackResult {
                    DamageList = attack.HitDamage,
                    RollResult = attack.RollResult,
                    OnHitStatuses = attackSpellResult.AppliedStatusList,
                    Owner = attackSpellResult.Caster,
                    Target = target,
                };
                DnDCommandManager.StaticTryExecute(new ApplyHitAttackResultCommand(result, true));
                index += 1;
            }
        }
    }
}
