using WpfToolsLibrary.Navigation;

namespace WpfCustomControlLibrary.ModalWindows
{
    /// <summary>
    ///     can be used to 
    /// </summary>
    public interface IResultWindow<TInput, TOutput> : IValidableWindow
    {
        void LoadContext(TInput context);

        TOutput GetResult();
    }
}
