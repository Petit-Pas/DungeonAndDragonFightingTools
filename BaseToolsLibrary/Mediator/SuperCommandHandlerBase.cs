using BaseToolsLibrary.DependencyInjection;
using System;

namespace BaseToolsLibrary.Mediator
{
    /// <summary>
    ///     Base handler for the BaseSuperCommands implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SuperCommandHandlerBase<TCommand, TResponse> : BaseMediatorHandler<TCommand, TResponse>
        where TCommand : SuperCommandBase
        where TResponse : class, IMediatorCommandResponse
    {
        protected static Lazy<IMediator> _mediator = new Lazy<IMediator>(() => DIContainer.GetImplementation<IMediator>());

        public SuperCommandHandlerBase()
        {
        }

        public override void Undo(TCommand command)
        {
            foreach (IMediatorCommand innerCommand in command.InnerCommands)
            {
                _mediator.Value.Undo(innerCommand);
            }
            command.InnerCommands.Clear();
        }
    }
}
