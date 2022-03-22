using System;
using System.Windows;
using System.Windows.Input;

namespace WpfToolsLibrary.Commands.WindowCommands
{
    public class CloseCommand : ICommand
    {

#pragma warning disable 0067
        public event EventHandler CanExecuteChanged;
#pragma warning restore 0067

        public CloseCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ((Window)parameter).Close();
        }
    }
}
