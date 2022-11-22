using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.TakeDamage;
using System;
using System.Diagnostics;
using BaseToolsLibrary.Extensions;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList
{
    public class ApplyDamageResultListHandler : SuperDndCommandHandlerBase<ApplyDamageResultListCommand, ApplyDamageResultListResponse>
    {
        private void ApplyDamageAffinity(ref int rawAmount, DamageAffinityEnum affinity)
        {
            switch (affinity)
            {
                case DamageAffinityEnum.Weak:
                    rawAmount *= 2;
                    break;
                case DamageAffinityEnum.Resistant:
                    rawAmount /= 2;
                    break;
                case DamageAffinityEnum.Immune:
                    rawAmount = 0;
                    break;
                case DamageAffinityEnum.Neutral:
                case DamageAffinityEnum.Unspecified:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(affinity), affinity, null);
            }
        }

        private void ApplySavingModifier(ref int rawAmount, DamageModifierEnum modifier)
        {
            switch (modifier)
            {
                case DamageModifierEnum.Halved:
                    rawAmount /= 2;
                    break;
                case DamageModifierEnum.Canceled:
                    rawAmount = 0;
                    break;
                case DamageModifierEnum.Normal:
                default:
                    break;
            }
        }

        private void ApplyReduceDamageBy(ref int total, PlayableEntity entity, ApplyDamageResultListCommand command)
        {
            if (entity.ReduceAllDamageBy != 0)
            {
                total = Math.Max(0, total - entity.ReduceAllDamageBy);
                command.AddLog($" , total reduced by {entity.ReduceAllDamageBy}");
            }
        }

        public override ApplyDamageResultListResponse Execute(ApplyDamageResultListCommand command)
        {
            if (command.DamageList.None())
            {
                return new ApplyDamageResultListResponse(0);
            }

            var total = 0;
            var target = command.GetEntity();

            command.AddLog($"{target.DisplayName}", FontWeightProvider.Bold);
            command.AddLog(" takes ");

            var i = 1;
            foreach (var damage in command.DamageList)
            {
                var finalDamage = damage.RawAmount;

                if (finalDamage < 0)
                {
                    Trace.WriteLine($"WARNING : Trying to inflict {finalDamage} damage on {target.DisplayName}," +
                        " will be treated as 0.");
                    finalDamage = 0;
                }

                ApplyDamageAffinity(ref finalDamage, damage.TypeAffinity);

                if (command.LastSavingWasSuccessfull)
                {
                    ApplySavingModifier(ref finalDamage, damage.SavingModifer);
                }

                total += finalDamage;

                if (i == command.DamageList.Count && i != 1)
                    command.AddLog("and ");
                command.AddLog($"{finalDamage} {damage.Type}", FontWeightProvider.Bold, FontColorProvider.GetColorByKey(damage.Type.ToString()));
                command.AddLog($"{(i == command.DamageList.Count ? " damage" : " damage, ")}");

                i += 1;
            }

            // TODO never tested
            ApplyReduceDamageBy(ref total, target, command);

            command.AddLog(".\r\n");

            var takeDamageCommand = new TakeDamageCommand(target, total);
            Mediator.Execute(takeDamageCommand);
            command.PushToInnerCommands(takeDamageCommand);
            return new ApplyDamageResultListResponse(total);
        }
    }
}
