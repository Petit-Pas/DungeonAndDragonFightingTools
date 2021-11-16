using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.NonAttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;
using System;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands
{
    public class CastNonAttackSpellHandler : SuperCommandHandlerBase<CastNonAttackSpellCommand, ValidableResponse<NoResponse>>
    {
        private static Lazy<IFigtherProvider> _fighterProvider = new Lazy<IFigtherProvider>(() => DIContainer.GetImplementation<IFigtherProvider>());

        public override ValidableResponse<NoResponse> Execute(IMediatorCommand command)
        {
            CastNonAttackSpellCommand _command = command as CastNonAttackSpellCommand;

            if (spellResultObtained(_command))
            {
                foreach (NewNonAttackSpellResult spellResult in _command.SpellResults)
                {
                    ApplyDamageResultListCommand damageCommand = new ApplyDamageResultListCommand(spellResult.Target, spellResult.HitDamage);
                    base._mediator.Value.Execute(damageCommand);
                    _command.PushToInnerCommands(damageCommand);

                    foreach (OnHitStatus status in spellResult.AppliedStatusList)
                    {
                        TryApplyStatusCommand statusCommand = new TryApplyStatusCommand(spellResult.CasterName, spellResult.TargetName, status, spellResult.Saving);
                        base._mediator.Value.Execute(statusCommand);
                        _command.PushToInnerCommands(statusCommand);
                    }
                }
                return new ValidableResponse<NoResponse>(true, MediatorCommandResponses.NoResponse);
            }
            return new ValidableResponse<NoResponse>(false, MediatorCommandResponses.NoResponse);
        }

        private bool spellResultObtained(CastNonAttackSpellCommand command)
        {
            NewNonAttackSpellResult template = command.Spell.GetNonAttackSpellResultTemplate(_fighterProvider.Value.GetFighterByDisplayName(command.CasterName), command.CastLevel);
            NonAttackSpellResults spellResults = new NonAttackSpellResults();
            
            foreach (string name in command.TargetNames)
            {
                NewNonAttackSpellResult spellResult = template.Clone() as NewNonAttackSpellResult;
                spellResult.Target = _fighterProvider.Value.GetFighterByDisplayName(name);
                spellResults.Add(spellResult);
            }

            NonAttackSpellResultsQuery query = new NonAttackSpellResultsQuery(template, spellResults);
            ValidableResponse<NonAttackSpellResults> response = base._mediator.Value.Execute(query) as ValidableResponse<NonAttackSpellResults>;

            command.SpellResults = response.IsValid ? response.Response : null;

            return response.IsValid;
        }
    }
}
