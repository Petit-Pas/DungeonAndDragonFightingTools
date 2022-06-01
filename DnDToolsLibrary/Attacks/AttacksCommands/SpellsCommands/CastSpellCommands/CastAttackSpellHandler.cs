using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.AttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;
using System;
using BaseToolsLibrary.IO;
using DnDToolsLibrary.BaseCommands;
using DnDToolsLibrary.Attacks.AttacksCommands.HitAttackCommands.ApplyHitAttackResult;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands
{
    public class CastAttackSpellHandler : SuperDndCommandHandlerBase<CastAttackSpellCommand, IMediatorCommandResponse>
    {
        private static Lazy<IFightersProvider> _fighterProvider = new Lazy<IFightersProvider>(() => DIContainer.GetImplementation<IFightersProvider>());

        public override IMediatorCommandResponse Execute(CastAttackSpellCommand command)
        {

            if (spellResultObtained(command))
            {
                foreach (var attackSpellResult in command.SpellResults)
                {
                    // TODO this will always hit => it is a problem!
                    command.AddLog("- ");
                    command.AddLog(attackSpellResult.TargetName, FontWeightProvider.Bold);
                    command.AddLog(": ");

                    using (new Indenter(3))
                    {
                        var hitCommand = new ApplyHitAttackResultCommand(attackSpellResult);
                        Mediator.Execute(hitCommand);
                        command.PushToInnerCommands(hitCommand);

                        foreach (var status in attackSpellResult.OnHitStatuses)
                        {
                            var statusCommand = new TryApplyStatusCommand(command.CasterName,
                                attackSpellResult.TargetName, status);
                            Mediator.Execute(statusCommand);
                            command.PushToInnerCommands(statusCommand);
                        }
                    }
                }
                return MediatorCommandStatii.Success;
            }
            return MediatorCommandStatii.Canceled;
        }

        private bool spellResultObtained(CastAttackSpellCommand command)
        {
            var template = command.Spell.GetAttackSpellResultTemplate(_fighterProvider.Value.GetFighterByDisplayName(command.CasterName), command.CastLevel);
            var spellResults = new AttackSpellResults();

            foreach (string name in command.TargetNames)
            {
                var spellResult = template.Clone() as AttackSpellResult;
                spellResult.Target = _fighterProvider.Value.GetFighterByDisplayName(name);
                spellResults.Add(spellResult);
            }

            var query = new AttackSpellResultsQuery(command.Spell.DisplayName, command.CastLevel, spellResults);
            var response = Mediator.Execute(query) as ValidableResponse<AttackSpellResults>;

            command.SpellResults = response.IsValid ? response.Response : null;

            return response.IsValid;
        }
    }
}
