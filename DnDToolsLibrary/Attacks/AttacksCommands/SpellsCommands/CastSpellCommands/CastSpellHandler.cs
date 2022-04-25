using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellLevelQueries;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellTargetsQueries;
using DnDToolsLibrary.Fight;
using System;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands
{
    public class CastSpellHandler : SuperCommandHandlerBase<CastSpellCommand, IMediatorCommandResponse>
    {
        private static Lazy<IFighterProvider> _fighterProvider = new Lazy<IFighterProvider> (() => DIContainer.GetImplementation<IFighterProvider>());

        public override IMediatorCommandResponse Execute(CastSpellCommand command)
        {
            if (spellLevelSelected(command) == false)
                return MediatorCommandStatii.Canceled;
            if (targetsSelected(command) == false)
                return MediatorCommandStatii.Canceled;

            ValidableResponse<MediatorCommandNoResponse> response;
            if (command.Spell.IsAnAttack)
            {
                CastAttackSpellCommand castAttackSpellCommand = new CastAttackSpellCommand(command.CasterName, command.Spell, command.CastLevel, command.TargetNames);
                response = base._mediator.Value.Execute(castAttackSpellCommand) as ValidableResponse<MediatorCommandNoResponse>;
            }
            else
            {
                CastNonAttackSpellCommand castNonAttackSpellCommand = new CastNonAttackSpellCommand(command.CasterName, command.Spell, command.CastLevel, command.TargetNames);
                response = base._mediator.Value.Execute(castNonAttackSpellCommand) as ValidableResponse<MediatorCommandNoResponse>;
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
            
            ValidableResponse<SpellTargets> response = base._mediator.Value.Execute(target_command) as ValidableResponse<SpellTargets>;
            
            if (response.IsValid)
                command.TargetNames = response.Response.TargetNames;
            return response.IsValid;
        }

        private bool spellLevelSelected(CastSpellCommand command)
        {
            if (command.Spell.BaseLevel == 0)
                return cantripLevelSelected(command);
            return normalSpellLevelSelected(command);
        }

        private bool normalSpellLevelSelected(CastSpellCommand command)
        {
            ValidableResponse<SpellLevel> response = base._mediator.Value.Execute(new NormalSpellLevelQuery(command.Spell.BaseLevel)) as ValidableResponse<SpellLevel>;
            
            if (response.IsValid)
                command.CastLevel = response.Response.Value;
            return response.IsValid;
        }

        private bool cantripLevelSelected(CastSpellCommand command)
        {
            ValidableResponse<SpellLevel> response = base._mediator.Value.Execute(new CantripLevelQuery()) as ValidableResponse<SpellLevel>;
            if (response.IsValid)
                command.CastLevel = response.Response.Value;
            return response.IsValid;
        }
    }
}
