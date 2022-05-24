using BaseToolsLibrary.Mediator;
using System.Windows;
using DnDToolsLibrary.BaseCommands;
using WpfCustomControlLibrary.ModalWindows;
using WpfToolsLibrary.Extensions;

namespace WpfDnDCommandHandlers
{
    public abstract class BaseResultCommandHandler<TInput, TOutput> : BaseMediatorHandler<TInput, ValidableResponse<TOutput>>
        where TInput : DndCommandBase
        where TOutput : class, IMediatorCommandResponse
    {
        public override ValidableResponse<TOutput> Execute(TInput command)
        {
            Window window = HandlerToUILinker.GetWindow(this.GetType()) as Window;
            IResultWindow<TInput, TOutput> resultWindow = window as IResultWindow<TInput, TOutput>;
            
            resultWindow.LoadContext(command);

            window.ShowCentered();

            return new ValidableResponse<TOutput>(
                    resultWindow.Validated,
                    resultWindow.GetResult());
        }

        public override void Undo(TInput genericCommand)
        {
            // undoing a user input won't do much
        }
    }
}
