using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using DDFight.Windows.FightWindows;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight.Events;
using WpfToolsLibrary.Extensions;

namespace TempExtensionsPlayableEntity
{
    public static class PlayableEntityGameExtensions
    {
        private static ICustomConsole console = DIContainer.GetImplementation<ICustomConsole>();
        private static IFontWeightProvider fontWeightProvider = DIContainer.GetImplementation<IFontWeightProvider>();
        private static IFontColorProvider fontColorProvider = DIContainer.GetImplementation<IFontColorProvider>();

        #region Turn
        /// <summary>
        ///     Functions to ask the PlayableEntity to setup eveyrthing for a new turn
        ///     Will raise the TurnStarted event
        /// </summary>
        public static void StartNewTurn(this PlayableEntity playableEntity)
        {
            playableEntity.HasAction = true;
            playableEntity.HasReaction = true;
            playableEntity.HasBonusAction = true;

            console.NewParagraph();
            console.AddEntry(playableEntity.DisplayName, fontWeightProvider.Bold);
            console.AddEntry(" starts its turn!\r\n", fontWeightProvider.Normal);

            playableEntity.InvokeTurnStarted(new StartNewTurnEventArgs(playableEntity.DisplayName));
        }

        /// <summary>
        ///     Function called to end the Turn of the PlayableEntity
        ///     Will raise the OnEndTurn event
        /// </summary>
        public static void EndTurn(this PlayableEntity playableEntity)
        {
            playableEntity.InvokeTurnEnded(new TurnEndedEventArgs(playableEntity.DisplayName));
        }

        #endregion Turn

        #region HpManagement

        public static void HealTempHP(this PlayableEntity playableEntity, DiceRoll to_roll)
        {
            to_roll.Roll();
            int amount = to_roll.LastResult;

            console.AddEntry(playableEntity.DisplayName, fontWeightProvider.Bold);

            if (playableEntity.TempHp < amount)
            {
                console.AddEntry(" now has ");
                console.AddEntry($"{amount}", fontWeightProvider.Bold);
                console.AddEntry(" temporary Hps.\n");
                playableEntity.TempHp = amount;
            }
            else
            {
                console.AddEntry(" keeps his ");
                console.AddEntry($"{amount}", fontWeightProvider.Bold);
                console.AddEntry(" temporary Hps.\n");
            }
        }

        public static void Heal(this PlayableEntity playableEntity, DiceRoll to_roll)
        {
            to_roll.Roll();
            int amount = to_roll.LastResult;

            if (playableEntity.Hp + amount >= playableEntity.MaxHp)
                amount = (int)playableEntity.MaxHp - playableEntity.Hp;


            console.AddEntry(playableEntity.DisplayName, fontWeightProvider.Bold);
            console.AddEntry(" regains ");
            console.AddEntry($"{amount}", fontWeightProvider.Bold);
            console.AddEntry(" Hps.\r\n");

            playableEntity.Hp += amount;
        }

        public static void LooseHp(this PlayableEntity playableEntity, int amount)
        {
            console.AddEntry("\nTotal: ");
            console.AddEntry($"{amount}", fontWeightProvider.Bold);
            console.AddEntry(" damage (");
            console.AddEntry($"{playableEntity.HpString}", fontWeightProvider.SemiBold);
            console.AddEntry(" ==> ");

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

            console.AddEntry($"{playableEntity.HpString}", fontWeightProvider.SemiBold);
            console.AddEntry(").\r\n");
            if (amount == 0)
                return;

            // handles 0 HP
            if (playableEntity.Hp <= 0)
            {
                playableEntity.Hp = 0;
                if (playableEntity.IsFocused == true)
                {
                    playableEntity.IsFocused = false;
                    console.AddEntry($"{playableEntity.DisplayName}", fontWeightProvider.SemiBold);
                    console.AddEntry(": Has reached");
                    console.AddEntry(" 0 Hps", fontWeightProvider.SemiBold);
                    console.AddEntry(", lost Focus.");
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

            console.AddEntry($"{playableEntity.DisplayName}", fontWeightProvider.Bold);
            console.AddEntry(" takes ");

            foreach (DamageResult dmg in damages)
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

                if (i == damages.Count && i != 1)
                    console.AddEntry("and ");
                console.AddEntry($"{damage_value} {dmg.DamageType}", fontWeightProvider.Bold, fontColorProvider.GetColorByKey(dmg.DamageType.ToString()));
                console.AddEntry($"{(i == damages.Count ? " damage" : " damage, ")}");
                total += damage_value;
                i += 1;
            }
            playableEntity.LooseHp(total);
        }

        #endregion HpManagement
    }
}
