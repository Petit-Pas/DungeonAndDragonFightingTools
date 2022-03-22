using BaseToolsLibrary.Mediator;
using Moq;
using System;

namespace CoreUnitTest.Extensions
{
    public static class IMediatorExtensions
    {
        public static Mock<IMediatorHandler> ConfigureCommandHandler<TQuery>(this IMediator mediator, IMediatorCommandResponse result)
        {
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();

            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>()))
                .Returns(result);
            mediator.RegisterHandler(mock.Object, typeof(TQuery));

            return mock;
        }

        public static Mock<IMediatorHandler> ConfigureCommandHandler<TCommand>(this IMediator mediator, Action<IMediatorCommand> callback)
        {
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();

            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>()))
                .Callback(callback);

            mediator.RegisterHandler(mock.Object, typeof(TCommand));

            return mock;
        }

        public static Mock<IMediatorHandler> ConfigureCommandHandler<TCommand>(this IMediator mediator, Action<IMediatorCommand> callback, IMediatorCommandResponse result)
        {
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();

            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>()))
                .Callback(callback)
                .Returns(result);

            mediator.RegisterHandler(mock.Object, typeof(TCommand));

            return mock;
        }
    }
}
