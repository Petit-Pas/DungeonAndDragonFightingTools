using BaseToolsLibrary.DependencyInjection;
using DnDToolsLibrary.Status;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus
{
    public class RemoveStatusCommand : EntityCommand
    {
        private static readonly Lazy<IStatusProvider> _statusProvider = new(DIContainer.GetImplementation<IStatusProvider>());
        protected static IStatusProvider StatusProvider => _statusProvider.Value;

        public RemoveStatusCommand(Guid statusReference, string affectedEntity) : base(affectedEntity)
        {
            Status = StatusProvider.GetStatusById(statusReference);
        }

        public CustomVerboseStatus Status { get; set; }
    }
}
