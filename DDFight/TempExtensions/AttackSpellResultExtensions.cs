using DDFight.GameExtensions;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using TempExtensionsPlayableEntity;
using WpfDnDCustomControlLibrary.Attacks.Spells;
using WpfToolsLibrary.ConsoleTools;
using WpfToolsLibrary.Extensions;

namespace TempExtensionsAttackSpellResultExtensions
{
    public static class AttackSpellResultExtensions
    {
        public static void Cast(this AttackSpellResult attackSpellResult, List<SpellAttackResultRollableUserControl> attacks)
        {
            Paragraph paragraph = (Paragraph)FightConsole.Instance.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(RunExtensions.BuildRun(attackSpellResult.Caster.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(" casts a lvl ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(RunExtensions.BuildRun(attackSpellResult.Level.ToString() + " " + attackSpellResult.Name + "\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));


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
                result.Execute();
                index += 1;
            }
        }
    }
}
