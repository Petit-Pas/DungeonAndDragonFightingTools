using BaseToolsLibrary.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseToolsLibrary.Mediator
{
    /// <summary>
    ///     Base handler for the BaseSuperCommands implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseSuperHandler<T> : BaseMediatorHandler<T>
        where T : BaseSuperCommand
    {
        protected Lazy<IMediator> _mediator = new Lazy<IMediator>(() => DIContainer.GetImplementation<IMediator>());

        public BaseSuperHandler()
        {
        }

        public override void Undo(IMediatorCommand command)
        {
            BaseSuperCommand _command = this.cast_command(command);
            
            foreach (IMediatorCommand inner_command in _command.InnerCommands)
            {
                _mediator.Value.Undo(inner_command);
            }
            _command.InnerCommands.Clear();
        }
    }
}
