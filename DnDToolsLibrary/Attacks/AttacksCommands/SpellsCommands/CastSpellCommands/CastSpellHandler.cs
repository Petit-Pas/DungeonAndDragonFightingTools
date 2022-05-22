using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellLevelQueries;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellTargetsQueries;
using DnDToolsLibrary.BaseCommandHandlers;
using DnDToolsLibrary.Fight;
using System;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands
{
    public class CastSpellHandler : SuperDndCommandHandler<CastSpellCommand, IMediatorCommandResponse>
    {
        private static Lazy<IFightersProvider> _fighterProvider = new Lazy<IFightersProvider> (() => DIContainer.GetImplementation<IFightersProvider>());

        public override IMediatorCommandResponse Execute(CastSpellCommand command)
        {
            if (spellLevelSelected(command) == false)
                return MediatorCommandStatii.Canceled;
            if (targetsSelected(command) == false)
                return MediatorCommandStatii.Canceled;

            ValidableResponse<MediatorCommandNoResponse> response;
            if (command.Spell.IsAnAttack)
            {
                var castAttackSpellCommand = new CastAttackSpellCommand(command.CasterName, command.Spell, command.CastLevel, command.TargetNames);
                response = Mediator.Execute(castAttackSpellCommand) as ValidableResponse<MediatorCommandNoResponse>;
            }
            else
            {
                var castNonAttackSpellCommand = new CastNonAttackSpellCommand(command.CasterName, command.Spell, command.CastLevel, command.TargetNames);
                response = Mediator.Execute(castNonAttackSpellCommand) as ValidableResponse<MediatorCommandNoResponse>;
            }
            return MediatorCommandStatii.Success;
        }

        private bool targetsSelected(CastSpellCommand command)
        {
            SpellTargetQuery target_command = new SpellTargetQuery(command.Spell.AmountTargets, command.Spell.CanSelectSameTargetTwice);

            if (command.CastLevel != command.Spell.BaseLevel && command.Spell.AdditionalTargetPerLevel != 0)
            {
                target_command.AmountTargets += command.Spell.AdditionalTargetPerLevel * (command.CastLevel - command.Spell.BaseLevel);
            }
            
            ValidableResponse<SpellTargets> response = Mediator.Execute(target_command) as ValidableResponse<SpellTargets>;
            
            if (response.IsValid)
                command.TargetNames = response.Response.TargetNames;
            return response.IsValid;
        }

        private bool spellLevelSelected(CastSpellCommand command)
        {
            if (command.Spell.IsCantrip)
                return cantripLevelSelected(command);
            return normalSpellLevelSelected(command);
        }

        private bool normalSpellLevelSelected(CastSpellCommand command)
        {
            ValidableResponse<SpellLevel> response = Mediator.Execute(new NormalSpellLevelQuery(command.Spell.BaseLevel)) as ValidableResponse<SpellLevel>;
            
            if (response.IsValid)
                command.CastLevel = response.Response.Value;
            return response.IsValid;
        }

        private bool cantripLevelSelected(CastSpellCommand command)
        {
            ValidableResponse<SpellLevel> response = Mediator.Execute(new CantripLevelQuery()) as ValidableResponse<SpellLevel>;
            if (response.IsValid)
                command.CastLevel = response.Response.Value;
            return response.IsValid;
        }
    }
}
