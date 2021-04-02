using DDFight;
using DDFight.Game.Entities.Display;
using DDFight.Windows.FightWindows;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Fight.Events;
using DnDToolsLibrary.Status;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using TempExtensionsOnHitStatus;
using WpfDnDCustomControlLibrary.Converters;
using WpfToolsLibrary.ConsoleTools;
using WpfToolsLibrary.Extensions;

namespace TempExtensionsPlayableEntity
{
    public static class PlayableEntityGameExtensions
    {

        #region Turn
        /// <summary>
        ///     Functions to ask the PlayableEntity to setup eveyrthing for a new turn
        ///     Will raise the NewTurnStarted event
        /// </summary>
        public static void StartNewTurn(this PlayableEntity playableEntity)
        {
            playableEntity.HasAction = true;
            playableEntity.HasReaction = true;
            playableEntity.HasBonusAction = true;

            FlowDocument document = FightConsole.Instance.UserLogs;
            document.Blocks.Add(new Paragraph());

            Paragraph tmp = (Paragraph)document.Blocks.LastBlock;
            tmp.Inlines.Add(RunExtensions.BuildRun(playableEntity.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            tmp.Inlines.Add(RunExtensions.BuildRun(" starts its turn!\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

            playableEntity.InvokeTurnStarted(new StartNewTurnEventArgs()
            {
                Character = playableEntity,
                CharacterIndex = FightersList.Instance.Elements.IndexOf(playableEntity),
            });
        }

        /// <summary>
        ///     Function called to end the Turn of the PlayableEntity
        ///     Will raise the OnEndTurn event
        /// </summary>
        public static void EndTurn(this PlayableEntity playableEntity)
        {
            playableEntity.InvokeTurnEnded(new TurnEndedEventArgs()
            {
                Character = playableEntity,
                CharacterIndex = FightersList.Instance.Elements.IndexOf(playableEntity),
            });
        }

        #endregion Turn

        #region Attack
        /// <summary>
        ///     Will evaluate if the HitAttack hits and deal damage if so.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="attacker"></param>
        /// <returns></returns>
        public static bool GetAttacked(this PlayableEntity playableEntity, HitAttackResult result, PlayableEntity attacker)
        {
            Paragraph paragraph = (Paragraph)FightConsole.Instance.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(RunExtensions.BuildRun(attacker.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(" attacks ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(RunExtensions.BuildRun(playableEntity.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(". ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(RunExtensions.BuildRun(result.RollResult.Description, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun("\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

            if (result.RollResult.Hits)
            {
                playableEntity.TakeHitDamage(result.DamageList);
                foreach (OnHitStatus onHitStatus in result.OnHitStatuses.Elements)
                {
                    onHitStatus.CheckIfApply(result.Owner, result.Target);
                }
                return true;
            }
            else
                return false;
        }

        #endregion Attack

        #region HpManagement

        public static void HealTempHP(this PlayableEntity playableEntity, DiceRoll to_roll)
        {
            to_roll.Roll();
            int amount = to_roll.LastResult;
            Paragraph paragraph = (Paragraph)FightConsole.Instance.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(RunExtensions.BuildRun(playableEntity.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));

            if (playableEntity.TempHp < amount)
            {
                paragraph.Inlines.Add(RunExtensions.BuildRun(" now has ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(RunExtensions.BuildRun(amount.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(RunExtensions.BuildRun(" temporary Hps.\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                playableEntity.TempHp = amount;
            }
            else
            {
                paragraph.Inlines.Add(RunExtensions.BuildRun(" keeps his ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(RunExtensions.BuildRun(playableEntity.TempHp.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(RunExtensions.BuildRun(" temporary Hps.\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            }
        }

        public static void Heal(this PlayableEntity playableEntity, DiceRoll to_roll)
        {
            to_roll.Roll();
            int amount = to_roll.LastResult;

            if (playableEntity.Hp + amount >= playableEntity.MaxHp)
                amount = (int)playableEntity.MaxHp - playableEntity.Hp;

            Paragraph paragraph = (Paragraph)FightConsole.Instance.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(RunExtensions.BuildRun(playableEntity.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(" regains ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(RunExtensions.BuildRun(amount.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(" Hps.\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

            playableEntity.Hp += amount;
        }

        public static void LooseHp(this PlayableEntity playableEntity, int amount)
        {
            Paragraph paragraph = (Paragraph)FightConsole.Instance.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(RunExtensions.BuildRun("\nTotal: ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(RunExtensions.BuildRun(amount.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(" damage (", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(RunExtensions.BuildRun(playableEntity.HpString, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(" ==> ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

            // handles temp HP
            if (playableEntity.TempHp != 0)
            {
                if (playableEntity.TempHp - amount < 0)
                {
                    amount -= playableEntity.TempHp;
                    playableEntity.TempHp = 0;
                }
                else
                {
                    playableEntity.TempHp -= amount;
                    amount = 0;
                }
            }

            // removes remaining HPs
            playableEntity.Hp -= amount;

            paragraph.Inlines.Add(RunExtensions.BuildRun(playableEntity.HpString, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(").\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            if (amount == 0)
                return;

            // handles 0 HP
            if (playableEntity.Hp <= 0)
            {
                playableEntity.Hp = 0;
                if (playableEntity.IsFocused == true)
                {
                    playableEntity.IsFocused = false;
                    paragraph.Inlines.Add(RunExtensions.BuildRun(playableEntity.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
                    paragraph.Inlines.Add(RunExtensions.BuildRun(": Has reached", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                    paragraph.Inlines.Add(RunExtensions.BuildRun(" 0 Hps", (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
                    paragraph.Inlines.Add(RunExtensions.BuildRun(", lost Focus.", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                }
            }

            // handles Concentration Check if required
            if (playableEntity.IsFocused)
            {
                ConcentrationCheckWindow window = new ConcentrationCheckWindow
                {
                    DataContext = playableEntity
                };
                window.ShowCentered();
                if (window.Success == false)
                {
                    playableEntity.IsFocused = false;
                }
            }
        }

        /// <summary>
        ///     method called when a hit attack lands to compute the damage received
        /// </summary>
        /// <param name="damages"></param>
        // TODO Might need to rename this
        public static void TakeHitDamage(this PlayableEntity playableEntity, DamageResultList damages)
        {
            int i = 1;
            int total = 0;

            Paragraph paragraph = (Paragraph)FightConsole.Instance.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(RunExtensions.BuildRun(playableEntity.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(" takes ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));


            foreach (DamageResult dmg in damages.Elements)
            {
                int damage_value = 0;
                if (dmg.Damage.LastResult > 0)
                {
                    DamageAffinityEnum affinity = playableEntity.DamageAffinities.GetAffinity(dmg.DamageType).Affinity;

                    // damage resistance / weakness
                    switch (affinity)
                    {
                        case DamageAffinityEnum.Neutral:
                            damage_value = dmg.Damage.LastResult;
                            break;
                        case DamageAffinityEnum.Resistant:
                            damage_value = dmg.Damage.LastResult / 2;
                            break;
                        case DamageAffinityEnum.Immune:
                            damage_value = 0;
                            break;
                        case DamageAffinityEnum.Weak:
                            damage_value = dmg.Damage.LastResult * 2;
                            break;
                    }
                    if (dmg.LastSavingWasSuccesfull)
                    {
                        // Situational damage modifiers (such as a saving throw that could divide damge by 2)
                        switch (dmg.SituationalDamageModifier)
                        {
                            case DamageModifierEnum.Halved:
                                damage_value /= 2;
                                break;
                            case DamageModifierEnum.Canceled:
                                damage_value = 0;
                                break;
                            default:
                                break;
                        }
                        dmg.LastSavingWasSuccesfull = false;
                    }
                }

                if (i == damages.Elements.Count && i != 1)
                    paragraph.Inlines.Add(RunExtensions.BuildRun("and ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(RunExtensions.BuildRun(damage_value.ToString() + " " + dmg.DamageType.ToString(), (Brush)DamageTypeEnumToBrushConverter.StaticConvert(dmg.DamageType), 15, FontWeights.Bold));
                paragraph.Inlines.Add(RunExtensions.BuildRun(i == damages.Elements.Count ? " damage" : " damage, ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                total += damage_value;
                i += 1;
            }
            playableEntity.LooseHp(total);
        }

        #endregion HpManagement
    }
}
