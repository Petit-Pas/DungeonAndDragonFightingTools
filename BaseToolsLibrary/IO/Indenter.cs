using System;
using BaseToolsLibrary.DependencyInjection;

namespace BaseToolsLibrary.IO
{
    // using this class in a using statement ensures that all messages logged to the console in between will be indeented
    public class Indenter : IDisposable
    {
        private static Lazy<ICustomConsole> _lazyConsole = new(DIContainer.GetImplementation<ICustomConsole>);
        private static ICustomConsole _console => _lazyConsole.Value;

        private int _amount;

        public Indenter(int amount)
        {
            _amount = amount;
            _console.AddIndentation(_amount);
        }
        public void Dispose()
        {
            _console.RemoveIndentation(_amount);
        }
    }
}
