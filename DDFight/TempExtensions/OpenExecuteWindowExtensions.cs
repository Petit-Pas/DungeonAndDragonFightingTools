using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using DDFight.Game.Aggression.Spells.Display;
using DDFight.Game.Fight.Display;
using DDFight.Windows.ModalWindows.FormWindow;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System.Collections.Generic;
using System.Linq;
using TempExtensionsOnHitStatus;
using TempExtensionsPlayableEntity;
using WpfDnDCustomControlLibrary.Attacks.HitAttacks;
using WpfToolsLibrary.Extensions;

namespace DDFight.WpfExtensions
{
    public static class OpenExecuteWindowExtensions
    {
        private static ICustomConsole console = DIContainer.GetImplementation<ICustomConsole>();
        private static IFontWeightProvider fontWeightProvider = DIContainer.GetImplementation<IFontWeightProvider>();

        public static void ExecuteHitAttack(this HitAttackTemplate template)
        {
            HitAttackExecuteWindow window = new HitAttackExecuteWindow() { DataContext = template, };
            window.ShowCentered();
        }

        public static void Cast(this NonAttackSpellResult nonAttackSpellResult, List<SavingThrow> savings = null)
        {

            console.AddEntry($"{nonAttackSpellResult.Caster.DisplayName}", fontWeightProvider.Bold);
            console.AddEntry(" casts a lvl ");
            console.AddEntry($"{nonAttackSpellResult.Level} {nonAttackSpellResult.Name}\r\n", fontWeightProvider.Bold);

            // TODO Here it doesnt make sense to move him and not the Saving throws
            if (nonAttackSpellResult.Targets.Contains(nonAttackSpellResult.Caster))
            {
                int position = nonAttackSpellResult.Targets.IndexOf(nonAttackSpellResult.Targets.First(x => x.DisplayName == nonAttackSpellResult.Caster.DisplayName));
                // This is to move the Caster at the end of the list, organizing things better for the Log Console if there is a Status
                nonAttackSpellResult.Targets.Remove(nonAttackSpellResult.Caster);
                nonAttackSpellResult.Targets.Add(nonAttackSpellResult.Caster);

                if (savings.Count > position)
                {
                    SavingThrow saving = savings.ElementAt(position);
                    savings.Remove(saving);
                    savings.Add(saving);
                }
            }

            bool already_applied = false;

            if (nonAttackSpellResult.HasSavingThrow)
            {
                foreach (OnHitStatus status in nonAttackSpellResult.AppliedStatusList)
                {
                    status.ApplySavingCharacteristic = nonAttackSpellResult.SavingCharacteristic;
                    status.ApplySavingDifficulty = nonAttackSpellResult.SavingDifficulty;
                }
            }
            foreach (DamageResult dmg in nonAttackSpellResult.HitDamage)
            {
                dmg.LastSavingWasSuccesfull = false;
            }
            
            for (int i = 0; i != nonAttackSpellResult.Targets.Count; i += 1)
            {
                // damage lessening if has saving throw
                if (nonAttackSpellResult.HasSavingThrow)
                {
                    console.AddEntry($"{nonAttackSpellResult.Targets.ElementAt(i).DisplayName}", fontWeightProvider.Bold);
                    console.AddEntry(" (");
                    console.AddEntry($"{savings.ElementAt(i).Result} / {savings.ElementAt(i).Difficulty}", fontWeightProvider.Bold);
                    console.AddEntry(") ==> ");
                    if (savings.ElementAt(i).IsSuccesful)
                    {
                        foreach (DamageResult dmg in nonAttackSpellResult.HitDamage)
                        {
                            dmg.LastSavingWasSuccesfull = true;
                        }
                        console.AddEntry("Resists\r\n", fontWeightProvider.Bold);
                    }
                    else
                    {
                        foreach (DamageResult dmg in nonAttackSpellResult.HitDamage)
                        {
                            dmg.LastSavingWasSuccesfull = false;
                        }
                        console.AddEntry("Does not resist\r\n", fontWeightProvider.Bold);
                    }
                }
                // damage application
                if (nonAttackSpellResult.HitDamage.Count != 0)
                    nonAttackSpellResult.Targets.ElementAt(i).TakeHitDamage(nonAttackSpellResult.HitDamage);

                // OnHitStatus Application
                foreach (OnHitStatus status in nonAttackSpellResult.AppliedStatusList)
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
                    else if (nonAttackSpellResult.HasSavingThrow == true && savings.ElementAt(i).IsSuccesful && status.SpellApplicationModifier == ApplicationModifierEnum.Maintained)
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
