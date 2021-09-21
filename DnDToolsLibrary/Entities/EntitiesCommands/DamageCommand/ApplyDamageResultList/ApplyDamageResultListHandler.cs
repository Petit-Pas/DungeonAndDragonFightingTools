using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.TakeDamage;
using System;
using System.Linq;
using static DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList.ApplyDamageResultListCommand;

namespace DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList
{
    public class ApplyDamageResultListHandler : BaseSuperHandler<ApplyDamageResultListCommand, ApplyDamageResultListResponse>
    {
        private static Lazy<ICustomConsole> console = new Lazy<ICustomConsole>(() => DIContainer.GetImplementation<ICustomConsole>());
        private static Lazy<IFontWeightProvider> fontWeightProvider = new Lazy<IFontWeightProvider>(() => DIContainer.GetImplementation<IFontWeightProvider>());
        private static Lazy<IFontColorProvider> colorProvider = new Lazy<IFontColorProvider>(() => DIContainer.GetImplementation<IFontColorProvider>());

        private void applyDamageAffinity(ref int rawAmount, DamageAffinityEnum affinity)
        {
            switch (affinity)
            {
                case DamageAffinityEnum.Weak:
                    rawAmount *= 2;
                    break;
                case DamageAffinityEnum.Neutral:
                    break;
                case DamageAffinityEnum.Resistant:
                    rawAmount /= 2;
                    break;
                case DamageAffinityEnum.Immune:
                    rawAmount = 0;
                    break;
            }
        }

        private void applySavingModifier(ref int rawAmount, DamageModifierEnum modifier)
        {
            switch (modifier)
            {
                case DamageModifierEnum.Halved:
                    rawAmount /= 2;
                    break;
                case DamageModifierEnum.Canceled:
                    rawAmount = 0;
                    break;
                default:
                    break;
            }
        }

        public override ApplyDamageResultListResponse Execute(IMediatorCommand command)
        {
            ApplyDamageResultListCommand _command = base.cast_command(command);

            if (false == _command.DamageList.Any())
                return null;

            PlayableEntity target = _command.GetEntity();
            int total = 0;

            console.Value.AddEntry($"{target.DisplayName}", fontWeightProvider.Value.Bold);
            console.Value.AddEntry(" takes ");

            int i = 1;
            foreach (Damage damage in _command.DamageList)
            {
                int final_damage = damage.RawAmount;

                if (final_damage < 0)
                {
                    Console.WriteLine($"WARNING : Trying to inflict {final_damage} damage on {target.DisplayName}," +
                        $" will be treated as 0.");
                    final_damage = 0;
                }

                applyDamageAffinity(ref final_damage, damage.TypeAffinity);

                if (_command.LastSavingWasSuccessfull)
                    applySavingModifier(ref final_damage, damage.SavingModifer);

                total += final_damage;

                if (i == _command.DamageList.Count && i != 1)
                    console.Value.AddEntry("and ");
                console.Value.AddEntry($"{final_damage} {damage.Type}", fontWeightProvider.Value.Bold, colorProvider.Value.GetColorByKey(damage.Type.ToString()));
                console.Value.AddEntry($"{(i == _command.DamageList.Count ? " damage" : " damage, ")}");
                
                i += 1;
            }
            console.Value.AddEntry("\r\n");

            TakeDamageCommand inner_command = new TakeDamageCommand(target, total);
            base._mediator.Value.Execute(inner_command);
            _command.PushToInnerCommands(inner_command);

            return new ApplyDamageResultListResponse(total);
        }
    }
}
