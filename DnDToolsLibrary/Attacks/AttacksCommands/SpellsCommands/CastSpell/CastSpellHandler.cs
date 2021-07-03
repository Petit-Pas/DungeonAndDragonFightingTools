using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputAttackSpellResults;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputSpellLevel;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.GetInputSpellTargets;
using DnDToolsLibrary.Attacks.Spells;
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

            if (_command.Spell.IsAnAttack)
            {
                if (castAttackSpell(_command) == false)
                    return new ValidableResponse<NoResponse>(false, MediatorCommandResponses.NoResponse);
            }
            else
            {
                if (castNonAttackSpell(_command) == false)
                    return new ValidableResponse<NoResponse>(false, MediatorCommandResponses.NoResponse);
            }

            return new ValidableResponse<NoResponse>(true, MediatorCommandResponses.NoResponse);
        }

        private bool castNonAttackSpell(CastSpellCommand command)
        {
            throw new NotImplementedException();
        }

        private bool castAttackSpell(CastSpellCommand command)
        {
            NewAttackSpellResult template = command.Spell.GetAttackSpellResultTemplate(fighterProvider.Value.GetFighterByDisplayName(command.CasterName), command.CastLevel);
            List<NewAttackSpellResult> spellResults = new List<NewAttackSpellResult>();
            foreach (string name in command.TargetNames)
            {
                NewAttackSpellResult spellResult = template.Clone() as NewAttackSpellResult;
                spellResult.Target = fighterProvider.Value.GetFighterByDisplayName(name);
                spellResults.Add(spellResult);
            }

            GetInputAttackSpellResultsCommand _command = new GetInputAttackSpellResultsCommand(command.Spell.DisplayName, command.CastLevel, spellResults);

            ValidableResponse<GetInputAttackSpellResultsResponse> response = base._mediator.Value.Execute(_command) as ValidableResponse<GetInputAttackSpellResultsResponse>;

            if (response.IsValid)
            {
                ;// actually execute the spell
            }
            return response.IsValid;
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
