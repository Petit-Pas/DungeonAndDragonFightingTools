﻿using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.AttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.BaseCommandHandlers;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;
using System;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands
{
    public class CastAttackSpellHandler : SuperDndCommandHandler<CastAttackSpellCommand, IMediatorCommandResponse>
    {
        private static Lazy<IFightersProvider> _fighterProvider = new Lazy<IFightersProvider>(() => DIContainer.GetImplementation<IFightersProvider>());

        public override IMediatorCommandResponse Execute(CastAttackSpellCommand command)
        {

            if (spellResultObtained(command))
            {
                foreach (var attackSpellResult in command.SpellResults)
                {
                    var damageCommand = new ApplyDamageResultListCommand(attackSpellResult.Target, attackSpellResult.HitDamage);
                    Mediator.Execute(damageCommand);
                    command.PushToInnerCommands(damageCommand);

                    foreach (var status in attackSpellResult.AppliedStatusList)
                    {
                        var statusCommand = new TryApplyStatusCommand(command.CasterName, attackSpellResult.TargetName, status);
                        Mediator.Execute(statusCommand);
                        command.PushToInnerCommands(statusCommand);
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
                var spellResult = template.Clone() as NewAttackSpellResult;
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
