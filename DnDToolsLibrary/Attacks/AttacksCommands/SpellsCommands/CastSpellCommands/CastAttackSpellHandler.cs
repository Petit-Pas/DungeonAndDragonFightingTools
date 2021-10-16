using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.AttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;
using System;
using System.Collections.Generic;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands
{
    public class CastAttackSpellHandler : SuperCommandHandlerBase<CastAttackSpellCommand, ValidableResponse<NoResponse>>
    {
        private static Lazy<IFigtherProvider> _fighterProvider = new Lazy<IFigtherProvider>(() => DIContainer.GetImplementation<IFigtherProvider>());

        public override ValidableResponse<NoResponse> Execute(IMediatorCommand command)
        {
            CastAttackSpellCommand _command = command as CastAttackSpellCommand;

            if (spellResultObtained(_command))
            {
                foreach (NewAttackSpellResult attackSpellResult in _command.SpellResults)
                {
                    ApplyDamageResultListCommand damageCommand = new ApplyDamageResultListCommand(attackSpellResult.Target, attackSpellResult.HitDamage);
                    base._mediator.Value.Execute(damageCommand);
                    _command.PushToInnerCommands(damageCommand);

                    foreach (OnHitStatus status in attackSpellResult.AppliedStatusList)
                    {
                        TryApplyStatusCommand statusCommand = new TryApplyStatusCommand(_command.CasterName, attackSpellResult.TargetName, status);
                        base._mediator.Value.Execute(statusCommand);
                        _command.PushToInnerCommands(statusCommand);
                    }
                }
                return new ValidableResponse<NoResponse>(true, MediatorCommandResponses.NoResponse);
            }
            return new ValidableResponse<NoResponse>(false, MediatorCommandResponses.NoResponse);
        }

        private bool spellResultObtained(CastAttackSpellCommand command)
        {
            NewAttackSpellResult template = command.Spell.GetAttackSpellResultTemplate(_fighterProvider.Value.GetFighterByDisplayName(command.CasterName), command.CastLevel);
            AttackSpellResults spellResults = new AttackSpellResults();

            foreach (string name in command.TargetNames)
            {
                NewAttackSpellResult spellResult = template.Clone() as NewAttackSpellResult;
                spellResult.Target = _fighterProvider.Value.GetFighterByDisplayName(name);
                spellResults.Add(spellResult);
            }

            AttackSpellResultsQuery query = new AttackSpellResultsQuery(command.Spell.DisplayName, command.CastLevel, spellResults);
            ValidableResponse<AttackSpellResults> response = base._mediator.Value.Execute(query) as ValidableResponse<AttackSpellResults>;

            command.SpellResults = response.IsValid ? response.Response : null;

            return response.IsValid;
        }
    }
}
