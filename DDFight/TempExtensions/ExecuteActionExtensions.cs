using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using TempExtensionsOnHitStatus;
using TempExtensionsPlayableEntity;
using WpfToolsLibrary.ConsoleTools;
using WpfToolsLibrary.Extensions;

namespace DDFight.GameExtensions
{
    public static class ExecuteActionExtensions
    {
        public static void Execute(this HitAttackResult hitAttackResult)
        {
            PlayableEntity attacker = hitAttackResult.Owner;
            PlayableEntity target = hitAttackResult.Target;

            #region FightLog

            Paragraph paragraph = (Paragraph)FightConsole.Instance.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(RunExtensions.BuildRun(attacker.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(" attacks ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(RunExtensions.BuildRun(target.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(". ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(RunExtensions.BuildRun(hitAttackResult.RollResult.Description, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun("\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

            #endregion FightLog

            if (hitAttackResult.RollResult.Hits)
            {
                target.TakeHitDamage(hitAttackResult.DamageList);
                foreach (OnHitStatus onHitStatus in hitAttackResult.OnHitStatuses.Elements)
                {
                    onHitStatus.CheckIfApply(attacker, target);
                }
            }
        }
    }
}
