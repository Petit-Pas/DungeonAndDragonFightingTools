using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System;
using DnDToolsLibrary.Attacks.AttacksCommands.DamageCommands.DamageResultListQueries;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.AddStatus;
using BaseToolsLibrary.Mediator.CommandStatii;

namespace DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands
{
    public class TryApplyStatusCommandHandler : SuperCommandHandlerBase<TryApplyStatusCommand, ValidableResponse<MediatorCommandNoResponse>>
    {
        private Lazy<IFigtherProvider> _fighterProvider = new Lazy<IFigtherProvider>(() => DIContainer.GetImplementation<IFigtherProvider>());

        public override ValidableResponse<MediatorCommandNoResponse> Execute(IMediatorCommand command)
        {
            TryApplyStatusCommand _command = this.castCommand(command);
            PlayableEntity target = _fighterProvider.Value.GetFighterByDisplayName(_command.TargetName);

            bool applyIsSuccess = true;

            if (_command.Saving == null)
            {
                // check if we need to do one anyway
                if (_command.Status.HasApplyCondition)
                {
                    SavingThrowQuery savingQuery = new SavingThrowQuery(
                        new SavingThrow(_command.Status.ApplySavingCharacteristic, _command.Status.ApplySavingDifficulty, _command.TargetName),
                        "Saving from Status application");
                    ValidableResponse<SavingThrow> response = base._mediator.Value.Execute(savingQuery) as ValidableResponse<SavingThrow>;

                    if (!response.IsValid)
                        return new ValidableResponse<MediatorCommandNoResponse>(false, MediatorCommandStatii.NoResponse);
                    _command.Saving = response.Response;
                }
            }

            if (_command.Saving != null)
            {
                // if the saving is failed, or if the status is maintained when the status is succesfull
                applyIsSuccess = !_command.Saving.IsSuccesful || _command.Status.SpellApplicationModifier == ApplicationModifierEnum.Maintained;
            }

            // sometimes it is applied anyway
            if (applyIsSuccess)
            {
                applyToTarget(target, _command);
            }

            if (_command.Status.OnApplyDamageList.Count != 0)
            {
                applyOnHitDamage(target, _command, _command.Saving?.IsSuccesful ?? false);
            }

            return new ValidableResponse<MediatorCommandNoResponse>(true, MediatorCommandStatii.NoResponse);
        }

        private void applyOnHitDamage(PlayableEntity target, TryApplyStatusCommand command, bool savingIsSuccess)
        {
            DamageResultList damageResultList = command.Status.OnApplyDamageList.GetResultList(!(command.Saving == null));

            foreach (DamageResult damageResult in damageResultList)
            {
                damageResult.AffinityModifier = target.DamageAffinities.GetAffinity(damageResult.DamageType).Affinity;
            }

            DamageResultListQuery damageQuery = new DamageResultListQuery(damageResultList, $"The status {command.Status.DisplayName} inflicted by {command.CasterName} will inflict damage to {command.TargetName}.");
            ValidableResponse<GetInputDamageResultListResponse> damageQueryResponse = _mediator.Value.Execute(damageQuery) as ValidableResponse<GetInputDamageResultListResponse>;

            if (damageQueryResponse.IsValid)
            {
                ApplyDamageResultListCommand applyOnHitCommand = new ApplyDamageResultListCommand(target, damageQueryResponse.Response.DamageResultList, savingIsSuccess);
                _mediator.Value.Execute(applyOnHitCommand);
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

            AddStatusCommand command = new AddStatusCommand(target, initialCommand.Status);
            base._mediator.Value.Execute(command);
            initialCommand.PushToInnerCommands(command);
        }
    }
}
