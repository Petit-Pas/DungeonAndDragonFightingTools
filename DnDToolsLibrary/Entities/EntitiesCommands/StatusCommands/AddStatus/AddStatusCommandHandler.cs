using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
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

        public override IMediatorCommandResponse Execute(AddStatusCommand command)
        {
            PlayableEntity target = command.GetEntity();

            command.Status.Target = target;
            _statusProvider.Value.Add(command.Status);
            target.AffectingStatusList.Add(new StatusReference(command.Status));

            _console.Value.AddEntry($"{command.Status.Caster.DisplayName}", _fontWeightProvider.Value.Bold);
            _console.Value.AddEntry($" applies ", _fontWeightProvider.Value.Normal);
            _console.Value.AddEntry($"{command.Status.DisplayName}", _fontWeightProvider.Value.Bold);
            _console.Value.AddEntry($" to ", _fontWeightProvider.Value.Normal);
            _console.Value.AddEntry($"{command.Status.Target.DisplayName}\r\n", _fontWeightProvider.Value.Bold);

            return MediatorCommandStatii.NoResponse;
        }

        public override void Undo(AddStatusCommand command)
        {
            PlayableEntity target = command.GetEntity();

            _statusProvider.Value.Remove(command.Status);
            StatusReference statusReference = target.AffectingStatusList.First(x => x.ActualStatusReferenceId == command.Status.Id);
            target.AffectingStatusList.Remove(statusReference);
        }
    }
}
