using BaseToolsLibrary.Mediator;
using System.Windows;
using WpfCustomControlLibrary.ModalWindows;
using WpfToolsLibrary.Extensions;

namespace WpfDnDCommandHandlers
{
    public abstract class BaseResultCommandHandler<TInput, TOutput> : BaseMediatorHandler<TInput, ValidableResponse<TOutput>>
        where TInput : class, IMediatorCommand
        where TOutput : class, IMediatorCommandResponse
    {
        public override ValidableResponse<TOutput> Execute(IMediatorCommand genericCommand)
        {
            Window window = HandlerToUILinker.GetWindow(this.GetType()) as Window;
            IResultWindow<TInput, TOutput> result_window = window as IResultWindow<TInput, TOutput>;
            TInput _command = base.castCommand(genericCommand);

            result_window.LoadContext(_command);

            window.ShowCentered();

            return new ValidableResponse<TOutput>(
                    result_window.Validated,
                    result_window.GetResult());
        }

        public override void Undo(IMediatorCommand genericCommand)
        {
            // undoing a user input won't do much
        }
    }
}
