using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using WpfDnDCustomControlLibrary.Entities.Extensions;
using WpfToolsLibrary.ConsoleTools;
using WpfToolsLibrary.Extensions;

namespace WpfDnDCustomControlLibrary.Attacks.Spells.Extensions
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
                target.GetAttacked(new HitAttackResult
                {
                    DamageList = attack.HitDamage,
                    RollResult = attack.RollResult,
                    OnHitStatuses = attackSpellResult.AppliedStatusList,
                    Owner = attackSpellResult.Caster,
                }, attackSpellResult.Caster);
                index += 1;
            }
        }
    }
}
