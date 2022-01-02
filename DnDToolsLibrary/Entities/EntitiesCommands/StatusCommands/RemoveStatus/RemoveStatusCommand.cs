using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus
{
    public class RemoveStatusCommand : EntityCommand
    {
        private IStatusProvider _statusProvider { get => __lazyStatusProvider.Value; }
        private Lazy<IStatusProvider> __lazyStatusProvider = new Lazy<IStatusProvider>(() => DIContainer.GetImplementation<IStatusProvider>());

        public RemoveStatusCommand(Guid statusReference, string affectedEntity) : base(affectedEntity)
        {
            Status = _statusProvider.GetStatusById(statusReference);
        }

        public CustomVerboseStatus Status { get; set; }
    }
}
