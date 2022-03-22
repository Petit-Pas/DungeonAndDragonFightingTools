using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using System;
using System.Linq;

namespace DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.AddStatus
{
    public class AddStatusCommandHandler : BaseMediatorHandler<AddStatusCommand, IMediatorCommandResponse>
    {
        private static readonly Lazy<ICustomConsole> _console = new Lazy<ICustomConsole>(DIContainer.GetImplementation<ICustomConsole>);
        private static readonly Lazy<IFontWeightProvider> _fontWeightProvider = new Lazy<IFontWeightProvider>(DIContainer.GetImplementation<IFontWeightProvider>);
        private static readonly Lazy<IStatusProvider> _statusProvider = new Lazy<IStatusProvider>(DIContainer.GetImplementation<IStatusProvider>);

        public override IMediatorCommandResponse Execute(IMediatorCommand genericCommand)
        {
            AddStatusCommand _command = this.castCommand(genericCommand);
            PlayableEntity target = _command.GetEntity();

            _command.Status.Target = target;
            _statusProvider.Value.Add(_command.Status);
            target.AffectingStatusList.Add(new StatusReference(_command.Status));

            _console.Value.AddEntry($"{_command.Status.Caster.DisplayName}", _fontWeightProvider.Value.Bold);
            _console.Value.AddEntry($" applies ", _fontWeightProvider.Value.Normal);
            _console.Value.AddEntry($"{_command.Status.DisplayName}", _fontWeightProvider.Value.Bold);
            _console.Value.AddEntry($" to ", _fontWeightProvider.Value.Normal);
            _console.Value.AddEntry($"{_command.Status.Target.DisplayName}\r\n", _fontWeightProvider.Value.Bold);

            return MediatorCommandStatii.NoResponse;
        }

        public override void Undo(IMediatorCommand genericCommand)
        {
            AddStatusCommand _command = this.castCommand(genericCommand);
            PlayableEntity target = _command.GetEntity();

            _statusProvider.Value.Remove(_command.Status);
            StatusReference statusReference = target.AffectingStatusList.First(x => x.ActualStatusReferenceId == _command.Status.Id);
            target.AffectingStatusList.Remove(statusReference);
        }
    }
}
