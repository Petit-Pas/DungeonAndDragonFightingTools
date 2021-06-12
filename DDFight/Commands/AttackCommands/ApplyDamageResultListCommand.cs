using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Entities;
using TempExtensionsPlayableEntity;

namespace DDFight.Commands.AttackCommands
{
    public class ApplyDamageResultListCommand : DnDCommandBase
    {
        private ICustomConsole console = DIContainer.GetImplementation<ICustomConsole>();
        private IFontWeightProvider fontWeightProvider = DIContainer.GetImplementation<IFontWeightProvider>();
        private IFontColorProvider colorProvider = DIContainer.GetImplementation<IFontColorProvider>();

        public ApplyDamageResultListCommand(PlayableEntity target, DamageResultList damageList, bool isInnerCommand) : base(isInnerCommand)
        {
            Target = target;
            DamageList = (DamageResultList)damageList.Clone();
        }

        public PlayableEntity Target { get; }
        public DamageResultList DamageList { get; }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            int i = 1;
            int total = 0;

            console.AddEntry($"{Target.DisplayName}", fontWeightProvider.Bold);
            console.AddEntry(" takes ");

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
                    console.AddEntry("and ");
                console.AddEntry($"{damage_value} {dmg.DamageType}", fontWeightProvider.Bold, colorProvider.GetColorByKey(dmg.DamageType.ToString()));
                console.AddEntry($"{(i == DamageList.Elements.Count ? " damage" : " damage, ")}");
                total += damage_value;
                i += 1;
            }
            Target.LooseHp(total);
        }
    }
}
