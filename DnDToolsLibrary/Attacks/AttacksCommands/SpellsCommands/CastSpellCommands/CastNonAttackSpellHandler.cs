using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.NonAttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;
using System;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands
{
    public class CastNonAttackSpellHandler : SuperCommandHandlerBase<CastNonAttackSpellCommand, IMediatorCommandResponse>
    {
        private static Lazy<IFightersProvider> _fighterProvider = new Lazy<IFightersProvider>(() => DIContainer.GetImplementation<IFightersProvider>());

        public override IMediatorCommandResponse Execute(CastNonAttackSpellCommand command)
        {
            if (spellResultObtained(command))
            {
                _fighterProvider.Value.GetFighterByDisplayName(command.CasterName).IsFocused = true;
                foreach (var spellResult in command.SpellResults)
                {
                    // TODO the fact that the saving is passed is not tested
                    var damageCommand = new ApplyDamageResultListCommand(spellResult.Target, spellResult.HitDamage, spellResult.Saving.IsSuccesful);
                    _mediator.Value.Execute(damageCommand);
                    command.PushToInnerCommands(damageCommand);

                    foreach (var status in spellResult.AppliedStatusList)
                    {
                        var statusCommand = new TryApplyStatusCommand(spellResult.CasterName, spellResult.TargetName, status, spellResult.Saving);
                        _mediator.Value.Execute(statusCommand);
                        command.PushToInnerCommands(statusCommand);
                    }
                }
                return MediatorCommandStatii.Success;
            }
            return MediatorCommandStatii.Canceled;
        }

        private bool spellResultObtained(CastNonAttackSpellCommand command)
        {
            var template = command.Spell.GetNonAttackSpellResultTemplate(_fighterProvider.Value.GetFighterByDisplayName(command.CasterName), command.CastLevel);
            var spellResults = new NonAttackSpellResults();
            
            foreach (string name in command.TargetNames)
            {
                var spellResult = template.Clone() as NewNonAttackSpellResult;
                spellResult.Target = _fighterProvider.Value.GetFighterByDisplayName(name);
                spellResults.Add(spellResult);
            }

            var query = new NonAttackSpellResultsQuery(template, spellResults);
            var response = _mediator.Value.Execute(query) as ValidableResponse<NonAttackSpellResults>;

            command.SpellResults = response.IsValid ? response.Response : null;

            return response.IsValid;
        }
    }
}
