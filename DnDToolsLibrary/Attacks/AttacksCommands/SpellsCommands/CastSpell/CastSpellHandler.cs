using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputSpellLevel;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputSpellTargets;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpell
{
    public class CastSpellHandler : BaseSuperHandler<CastSpellCommand, ValidableResponse<NoResponse>>
    {
        private static Lazy<IFigtherProvider> fighterProvider = new Lazy<IFigtherProvider> (() => DIContainer.GetImplementation<IFigtherProvider>());

        public override ValidableResponse<NoResponse> Execute(IMediatorCommand command)
        {
            CastSpellCommand _command = base.cast_command(command);
            PlayableEntity caster = fighterProvider.Value.GetFighterByDisplayName(_command.CasterName);

            if (spellLevelSelected(_command) == false)
                return new ValidableResponse<NoResponse>(false, MediatorCommandResponses.NoResponse);
            if (targetsSelected(_command) == false)
                return new ValidableResponse<NoResponse>(false, MediatorCommandResponses.NoResponse);

            return new ValidableResponse<NoResponse>(true, MediatorCommandResponses.NoResponse);
        }

        private bool targetsSelected(CastSpellCommand command)
        {
            GetInputSpellTargetsCommand target_command = new GetInputSpellTargetsCommand(command.Spell.AmountTargets, command.Spell.CanSelectSameTargetTwice);

            if (command.CastLevel != command.Spell.BaseLevel && command.Spell.AdditionalTargetPerLevel != 0)
            {
                target_command.AmountTargets += command.Spell.AdditionalTargetPerLevel * (command.CastLevel - command.Spell.BaseLevel);
            }
            
            ValidableResponse<GetInputSpellTargetsResponse> response = base._mediator.Value.Execute(target_command) as ValidableResponse<GetInputSpellTargetsResponse>;
            
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
            ValidableResponse<GetInputSpellLevelResponse> response = base._mediator.Value.Execute(new GetInputNormalSpellLevelCommand(command.Spell.BaseLevel)) as ValidableResponse<GetInputSpellLevelResponse>;
            
            if (response.IsValid)
                command.CastLevel = response.Response.Level;
            return response.IsValid;
        }

        private bool cantripLevelSelected(CastSpellCommand command)
        {
            ValidableResponse<GetInputCantripLevelResponse> response = base._mediator.Value.Execute(new GetInputCantripLevelCommand()) as ValidableResponse<GetInputCantripLevelResponse>;
            if (response.IsValid)
                command.CastLevel = response.Response.Level;
            return response.IsValid;
        }
    }
}
