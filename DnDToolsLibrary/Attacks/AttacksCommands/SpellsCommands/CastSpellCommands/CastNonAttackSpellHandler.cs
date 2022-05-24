using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.NonAttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;
using System;
using System.Linq;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.NonAttackSpellResultQueries;
using DnDToolsLibrary.BaseCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.AcquireConcentration;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands
{
    public class CastNonAttackSpellHandler : SuperDndCommandHandlerBase<CastNonAttackSpellCommand, IMediatorCommandResponse>
    {
        private static Lazy<IFightersProvider> _fighterProvider = new Lazy<IFightersProvider>(() => DIContainer.GetImplementation<IFightersProvider>());

        public override IMediatorCommandResponse Execute(CastNonAttackSpellCommand command)
        {
            if (spellResultObtained(command))
            {
                if (command.Spell.AppliedStatus.Any(x => x.EndsOnCasterLossOfConcentration))
                {
                    // TODO this is not tested either
                    var acquireConcentrationCommand = new AcquireConcentrationCommand(command.CasterName);
                    Mediator.Execute(acquireConcentrationCommand);
                    command.PushToInnerCommands(acquireConcentrationCommand);
                }

                foreach (var spellResult in command.SpellResults)
                {
                    // TODO the fact that the saving is passed is not tested
                    var damageCommand = new ApplyDamageResultListCommand(spellResult.Target, spellResult.HitDamage, spellResult.Saving?.IsSuccesful ?? false);
                    Mediator.Execute(damageCommand);
                    command.PushToInnerCommands(damageCommand);

                    foreach (var status in spellResult.AppliedStatusList)
                    {
                        var statusCommand = new TryApplyStatusCommand(spellResult.CasterName, spellResult.TargetName, status, spellResult.Saving);
                        Mediator.Execute(statusCommand);
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
            var response = Mediator.Execute(query) as ValidableResponse<NonAttackSpellResults>;

            command.SpellResults = response.IsValid ? response.Response : null;

            return response.IsValid;
        }
    }
}
