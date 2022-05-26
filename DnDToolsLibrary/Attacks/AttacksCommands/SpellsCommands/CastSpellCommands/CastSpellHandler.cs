using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellLevelQueries;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellTargetsQueries;
using DnDToolsLibrary.Fight;
using System;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands
{
    public class CastSpellHandler : SuperDndCommandHandlerBase<CastSpellCommand, IMediatorCommandResponse>
    {
        private static Lazy<IFightersProvider> _fighterProvider = new Lazy<IFightersProvider> (() => DIContainer.GetImplementation<IFightersProvider>());

        public override IMediatorCommandResponse Execute(CastSpellCommand command)
        {
            if (SpellLevelSelected(command) == false)
                return MediatorCommandStatii.Canceled;
            if (TargetsSelected(command) == false)
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

        private static bool TargetsSelected(CastSpellCommand command)
        {
            var targetQuery = new SpellTargetQuery(command.Spell.AmountTargets, command.Spell.CanSelectSameTargetTwice);

            if (command.CastLevel != command.Spell.BaseLevel && command.Spell.AdditionalTargetPerLevel != 0)
            {
                targetQuery.AmountTargets += command.Spell.AdditionalTargetPerLevel * (command.CastLevel - command.Spell.BaseLevel);
            }
            
            var response = Mediator.Execute(targetQuery) as ValidableResponse<SpellTargets>;

            if (response.IsValid)
                command.TargetNames = response.Response.TargetNames;

            command.AddLog($" on {response.Response.TargetNames?.Count} fighters.");

            return response.IsValid;
        }

        private static bool SpellLevelSelected(CastSpellCommand command)
        {
            command.AddLog(command.CasterName, FontWeightProvider.Bold);
            var entry = command.AddLog(" casts ");
            command.AddLog(command.Spell.DisplayName, FontWeightProvider.Bold);

            var result = command.Spell.IsCantrip ? 
                CantripLevelSelected(command) :
                NormalSpellLevelSelected(command);

            command.AddLogAfter(entry, $"a lvl {command.CastLevel} ");

            return result;
        }

        private static bool NormalSpellLevelSelected(CastSpellCommand command)
        {
            var validableSpellLevel = Mediator.Execute(new NormalSpellLevelQuery(command.Spell.BaseLevel)) as ValidableResponse<SpellLevel>;
            
            if (validableSpellLevel.IsValid)
                command.CastLevel = validableSpellLevel.Response.Value;
            return validableSpellLevel.IsValid;
        }

        private static bool CantripLevelSelected(CastSpellCommand command)
        {
            var validableSpellLevel = Mediator.Execute(new CantripLevelQuery()) as ValidableResponse<SpellLevel>;
            if (validableSpellLevel.IsValid)
                command.CastLevel = validableSpellLevel.Response.Value;
            return validableSpellLevel.IsValid;
        }
    }
}
