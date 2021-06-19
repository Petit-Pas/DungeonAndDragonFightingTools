using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.DamageCommands.CalculateDamageResultList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCommandHandlers.AttackCommands.DamageCommands.CalculateDamageResultList
{
    public class CalculateDamageResultListHandler : BaseMediatorHandler<CalculateDamageResultListCommand, ValidableResponse<CalculateDamageResultListResponse>>
    {
        public override ValidableResponse<CalculateDamageResultListResponse> Execute(IMediatorCommand command)
        {
            CalculateDamageResultListCommand _command = base.cast_command(command);
            Window window = HandlerToUILinker.GetWindow(this.GetType()) as Window;

            window.DataContext = _command;
            window.ShowCentered();

            bool validated = ((IValidableWindow)window).Validated;

            return new ValidableResponse<CalculateDamageResultListResponse>(
                validated, new CalculateDamageResultListResponse() { DamageResultList = _command.DamageList }
            );
        }

        public override void Undo(IMediatorCommand command)
        {
            // as this command only fills data and does not modify anything, it won't do anything to undo it
        }
    }
}
