using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System;
using DnDToolsLibrary.Attacks.AttacksCommands.DamageCommands.DamageResultListQueries;
using DnDToolsLibrary.BaseCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.AddStatus;

namespace DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands
{
    public class TryApplyStatusCommandHandler : SuperDndCommandHandlerBase<TryApplyStatusCommand, IMediatorCommandResponse>
    {
        private Lazy<IFightersProvider> _fighterProvider = new Lazy<IFightersProvider>(() => DIContainer.GetImplementation<IFightersProvider>());

        public override IMediatorCommandResponse Execute(TryApplyStatusCommand command)
        {
            PlayableEntity target = _fighterProvider.Value.GetFighterByDisplayName(command.Status.TargetName);

            bool applyIsSuccess = true;

            if (command.Saving == null)
            {
                // check if we need to do one anyway
                if (command.Status.HasApplyCondition)
                {
                    SavingThrowQuery savingQuery = new SavingThrowQuery(
                        new SavingThrow(command.Status.ApplySavingCharacteristic, command.Status.ApplySavingDifficulty, command.Status.TargetName),
                        "Saving from Status application");
                    ValidableResponse<SavingThrow> response = Mediator.Execute(savingQuery) as ValidableResponse<SavingThrow>;

                    if (!response.IsValid)
                        return MediatorCommandStatii.Canceled;
                    command.Saving = response.Response;
                }
            }

            if (command.Saving != null)
            {
                // if the saving is failed, or if the status is maintained when the status is succesfull
                applyIsSuccess = !command.Saving.IsSuccesful || command.Status.SpellApplicationModifier == ApplicationModifierEnum.Maintained;
            }

            // sometimes it is applied anyway
            if (applyIsSuccess)
            {
                applyToTarget(target, command);
            }

            if (command.Status.OnApplyDamageList.Count != 0)
            {
                applyOnHitDamage(target, command, command.Saving?.IsSuccesful ?? false);
            }

            return applyIsSuccess ? MediatorCommandStatii.Success : MediatorCommandStatii.Failed;
        }

        private void applyOnHitDamage(PlayableEntity target, TryApplyStatusCommand command, bool savingIsSuccess)
        {
            DamageResultList damageResultList = command.Status.OnApplyDamageList.GetResultList(!(command.Saving == null));

            foreach (DamageResult damageResult in damageResultList)
            {
                damageResult.AffinityModifier = target.DamageAffinities.GetAffinity(damageResult.DamageType).Affinity;
            }

            DamageResultListQuery damageQuery = new DamageResultListQuery(damageResultList, $"The status {command.Status.DisplayName} inflicted by {command.Status.CasterName} will inflict damage to {command.Status.TargetName}.");
            ValidableResponse<GetInputDamageResultListResponse> damageQueryResponse = Mediator.Execute(damageQuery) as ValidableResponse<GetInputDamageResultListResponse>;

            if (damageQueryResponse.IsValid)
            {
                ApplyDamageResultListCommand applyOnHitCommand = new ApplyDamageResultListCommand(target, damageQueryResponse.Response.DamageResultList, savingIsSuccess);
                Mediator.Execute(applyOnHitCommand);
                command.PushToInnerCommands(applyOnHitCommand);
            }
        }

        public void applyToTarget(PlayableEntity target, TryApplyStatusCommand initialCommand)
        {
            if (initialCommand.Saving != null)
            {
                initialCommand.Status.ApplySavingDifficulty = initialCommand.Saving.Difficulty;
                initialCommand.Status.ApplySavingCharacteristic = initialCommand.Saving.Characteristic;
            }

            var command = new AddStatusCommand(target, initialCommand.Status);
            Mediator.Execute(command);
            initialCommand.PushToInnerCommands(command);
        }
    }
}
