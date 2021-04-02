using BaseToolsLibrary.Memory;
using DDFight.Game.Aggression.Spells.Display;
using DDFight.Game.Fight.Display;
using DDFight.Windows.FightWindows;
using DDFight.Windows.ModalWindows.FormWindow;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using TempExtensionsOnHitStatus;
using TempExtensionsPlayableEntity;
using WpfToolsLibrary.ConsoleTools;
using WpfToolsLibrary.Extensions;

namespace DDFight.WpfExtensions
{
    public static class OpenExecuteWindowExtensions
    {
        public static void ExecuteHitAttack(this HitAttackTemplate template)
        {
            ExecuteHitAttackWindow window = new ExecuteHitAttackWindow() { DataContext = template, };
            window.ShowCentered();
        }

        public static void Cast(this NonAttackSpellResult nonAttackSpellResult, List<SavingThrow> savings = null)
        {

            Paragraph paragraph = (Paragraph)FightConsole.Instance.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(RunExtensions.BuildRun(nonAttackSpellResult.Caster.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(" casts a lvl ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(RunExtensions.BuildRun(nonAttackSpellResult.Level.ToString() + " " + nonAttackSpellResult.Name + "\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));

            if (nonAttackSpellResult.Targets.Contains(nonAttackSpellResult.Caster))
            {
                // This is to move the Caster at the end of the list, organizing things better for the Log Console if there is a Status
                nonAttackSpellResult.Targets.Remove(nonAttackSpellResult.Caster);
                nonAttackSpellResult.Targets.Add(nonAttackSpellResult.Caster);
            }

            bool already_applied = false;

            if (nonAttackSpellResult.HasSavingThrow)
            {
                foreach (OnHitStatus status in nonAttackSpellResult.AppliedStatusList.Elements)
                {
                    status.ApplySavingCharacteristic = nonAttackSpellResult.SavingCharacteristic;
                    status.ApplySavingDifficulty = nonAttackSpellResult.SavingDifficulty;
                }
            }
            foreach (DamageResult dmg in nonAttackSpellResult.HitDamage.Elements)
            {
                dmg.LastSavingWasSuccesfull = false;
            }
            
            for (int i = 0; i != nonAttackSpellResult.Targets.Count; i += 1)
            {
                // damage lessening if has saving throw
                if (nonAttackSpellResult.HasSavingThrow)
                {
                    paragraph.Inlines.Add(RunExtensions.BuildRun(nonAttackSpellResult.Targets.ElementAt(i).DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    paragraph.Inlines.Add(RunExtensions.BuildRun(" (", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                    paragraph.Inlines.Add(RunExtensions.BuildRun(savings.ElementAt(i).Result + " / " + savings.ElementAt(i).Difficulty, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    paragraph.Inlines.Add(RunExtensions.BuildRun(") ==> ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                    if (savings.ElementAt(i).IsSuccesful)
                    {
                        foreach (DamageResult dmg in nonAttackSpellResult.HitDamage.Elements)
                        {
                            dmg.LastSavingWasSuccesfull = true;
                        }
                        paragraph.Inlines.Add(RunExtensions.BuildRun("Resists\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    }
                    else
                    {
                        foreach (DamageResult dmg in nonAttackSpellResult.HitDamage.Elements)
                        {
                            dmg.LastSavingWasSuccesfull = false;
                        }
                        paragraph.Inlines.Add(RunExtensions.BuildRun("Does not resist\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    }
                }
                // damage application
                if (nonAttackSpellResult.HitDamage.Count != 0)
                    nonAttackSpellResult.Targets.ElementAt(i).TakeHitDamage(nonAttackSpellResult.HitDamage);

                // OnHitStatus Application
                foreach (OnHitStatus status in nonAttackSpellResult.AppliedStatusList.Elements)
                {
                    if (nonAttackSpellResult.HasSavingThrow == false)
                    {
                        status.Apply(nonAttackSpellResult.Caster, nonAttackSpellResult.Targets.ElementAt(i), multiple_application: already_applied);
                        already_applied = true;
                    }
                    else if (savings.ElementAt(i).IsSuccesful == false)
                    {
                        status.Apply(nonAttackSpellResult.Caster, nonAttackSpellResult.Targets.ElementAt(i), multiple_application: already_applied);
                        already_applied = true;
                    }
                    else if (nonAttackSpellResult.HasSavingThrow = true && savings.ElementAt(i).IsSuccesful && status.SpellApplicationModifier == ApplicationModifierEnum.Maintained)
                    {
                        status.Apply(nonAttackSpellResult.Caster, nonAttackSpellResult.Targets.ElementAt(i), application_success: false, multiple_application: already_applied);
                        already_applied = true;
                    }
                }
            }
        }

        public static void CastSpell(this Spell spell, PlayableEntity caster)
        {
            AskPositiveIntWindow levelWindow = new AskPositiveIntWindow();
            levelWindow.DescriptionTextBoxControl.Text = "at which level do you wish to cast this spell?";
            levelWindow.Number = spell.BaseLevel;
            levelWindow.ShowCentered();

            if (levelWindow.Validated == false)
                return;

            int level = levelWindow.Number;
            int additional_levels = level - spell.BaseLevel;
            int amountTargets = spell.AmountTargets;

            if (amountTargets != 0)
                for (int i = additional_levels; i > 0; i--)
                {
                    amountTargets += spell.AdditionalTargetPerLevel;
                }

            FightingEntityListSelectableWindow targetWindow = new FightingEntityListSelectableWindow {
                MaximumSelected = amountTargets,
                CanSelectSameTargetTwice = spell.CanSelectSameTargetTwice,
            };
            targetWindow.ShowCentered();

            if (targetWindow.Validated == true)
            {
                if (spell.IsAnAttack)
                {
                    SpellAttackCastWindow window = new SpellAttackCastWindow()
                    {
                        DataContext = spell.GetAttackSpellResult(caster, targetWindow.Selected, additional_levels)
                    };
                    window.ShowCentered();
                }
                else
                {
                    SpellNonAttackCastWindow window = new SpellNonAttackCastWindow() { 
                        DataContext = spell.GetNonAttackSpellResult(caster, targetWindow.Selected, additional_levels) 
                    };
                    window.ShowCentered();
                }
            }
        }
    }
}
