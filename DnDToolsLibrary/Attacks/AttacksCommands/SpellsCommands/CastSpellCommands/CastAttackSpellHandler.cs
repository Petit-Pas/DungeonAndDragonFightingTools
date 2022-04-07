using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.AttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;
using System;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands
{
    public class CastAttackSpellHandler : SuperCommandHandlerBase<CastAttackSpellCommand, IMediatorCommandResponse>
    {
        private static Lazy<IFigtherProvider> _fighterProvider = new Lazy<IFigtherProvider>(() => DIContainer.GetImplementation<IFigtherProvider>());

        public override IMediatorCommandResponse Execute(CastAttackSpellCommand command)
        {

            if (spellResultObtained(command))
            {
                foreach (NewAttackSpellResult attackSpellResult in command.SpellResults)
                {
                    ApplyDamageResultListCommand damageCommand = new ApplyDamageResultListCommand(attackSpellResult.Target, attackSpellResult.HitDamage);
                    base._mediator.Value.Execute(damageCommand);
                    command.PushToInnerCommands(damageCommand);

                    foreach (OnHitStatus status in attackSpellResult.AppliedStatusList)
                    {
                        TryApplyStatusCommand statusCommand = new TryApplyStatusCommand(command.CasterName, attackSpellResult.TargetName, status);
                        base._mediator.Value.Execute(statusCommand);
                        command.PushToInnerCommands(statusCommand);
                    }
                }
                return MediatorCommandStatii.Success;
            }
            return MediatorCommandStatii.Canceled;
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
