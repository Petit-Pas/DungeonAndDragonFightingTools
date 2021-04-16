using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using TempExtensionsPlayableEntity;
using WpfDnDCustomControlLibrary.Converters;
using WpfToolsLibrary.ConsoleTools;
using WpfToolsLibrary.Extensions;

namespace DDFight.Commands.AttackCommands
{
    public class ApplyDamageResultListCommand : DnDCommandBase
    {
        public ApplyDamageResultListCommand(PlayableEntity target, DamageResultList damageList, bool isInnerCommand) : base(isInnerCommand)
        {
            Target = target;
            DamageList = (DamageResultList)damageList.Clone();
        }

        public PlayableEntity Target { get; }
        public DamageResultList DamageList { get; }

        protected Paragraph CommandParagraph { get; set; }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            int i = 1;
            int total = 0;

            CommandParagraph = (Paragraph)FightConsole.Instance.UserLogs.Blocks.LastBlock;

            CommandParagraph.Inlines.Add(RunExtensions.BuildRun(Target.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            CommandParagraph.Inlines.Add(RunExtensions.BuildRun(" takes ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));


            foreach (DamageResult dmg in DamageList.Elements)
            {
                int damage_value = 0;
                if (dmg.Damage.LastResult > 0)
                {
                    DamageAffinityEnum affinity = Target.DamageAffinities.GetAffinity(dmg.DamageType).Affinity;

                    // damage resistance / weakness
                    switch (affinity)
                    {
                        case DamageAffinityEnum.Weak:
                            damage_value = dmg.Damage.LastResult * 2;
                            break;
                        case DamageAffinityEnum.Neutral:
                            damage_value = dmg.Damage.LastResult;
                            break;
                        case DamageAffinityEnum.Resistant:
                            damage_value = dmg.Damage.LastResult / 2;
                            break;
                        case DamageAffinityEnum.Immune:
                            damage_value = 0;
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

                if (i == DamageList.Elements.Count && i != 1)
                    CommandParagraph.Inlines.Add(RunExtensions.BuildRun("and ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                CommandParagraph.Inlines.Add(RunExtensions.BuildRun(damage_value.ToString() + " " + dmg.DamageType.ToString(), (Brush)DamageTypeEnumToBrushConverter.StaticConvert(dmg.DamageType), 15, FontWeights.Bold));
                CommandParagraph.Inlines.Add(RunExtensions.BuildRun(i == DamageList.Elements.Count ? " damage" : " damage, ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                total += damage_value;
                i += 1;
            }
            Target.LooseHp(total);
        }
    }
}
