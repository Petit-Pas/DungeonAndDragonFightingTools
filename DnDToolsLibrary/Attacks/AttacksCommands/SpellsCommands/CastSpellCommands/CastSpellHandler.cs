using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.AttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.NonAttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellLevelQueries;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellTargetsQueries;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Fight;
using System;
using System.Collections.Generic;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands
{
    public class CastSpellHandler : SuperCommandHandlerBase<CastSpellCommand, ValidableResponse<NoResponse>>
    {
        private static Lazy<IFigtherProvider> _fighterProvider = new Lazy<IFigtherProvider> (() => DIContainer.GetImplementation<IFigtherProvider>());

        public override ValidableResponse<NoResponse> Execute(IMediatorCommand command)
        {
            CastSpellCommand _command = base.castCommand(command);
            PlayableEntity caster = _fighterProvider.Value.GetFighterByDisplayName(_command.CasterName);

            if (spellLevelSelected(_command) == false)
                return new ValidableResponse<NoResponse>(false, MediatorCommandResponses.NoResponse);
            if (targetsSelected(_command) == false)
                return new ValidableResponse<NoResponse>(false, MediatorCommandResponses.NoResponse);

            ValidableResponse<NoResponse> response;
            if (_command.Spell.IsAnAttack)
            {
                CastAttackSpellCommand castAttackSpellCommand = new CastAttackSpellCommand(_command.CasterName, _command.Spell, _command.CastLevel, _command.TargetNames);
                response = base._mediator.Value.Execute(castAttackSpellCommand) as ValidableResponse<NoResponse>;
            }
            else
            {
                CastNonAttackSpellCommand castNonAttackSpellCommand = new CastNonAttackSpellCommand(_command.CasterName, _command.Spell, _command.CastLevel, _command.TargetNames);
                response = base._mediator.Value.Execute(castNonAttackSpellCommand) as ValidableResponse<NoResponse>;
            }
            return new ValidableResponse<NoResponse>(response.IsValid, MediatorCommandResponses.NoResponse);
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
