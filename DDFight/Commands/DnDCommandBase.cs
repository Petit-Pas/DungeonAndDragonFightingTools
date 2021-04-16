using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Commands
{
    public abstract class DnDCommandBase : IDnDCommand
    {
        /// <summary>
        ///     Used by the DnDCommandManager to know if it needs to retain that Command (value false)
        ///                                         or if it only needs to execute, and memory will be stored by the master command (value true)
        /// </summary>
        public bool IsInnerCommand { get; set; }

#pragma warning disable 0067
        public event EventHandler CanExecuteChanged;
#pragma warning restore 0067

        public DnDCommandBase(bool isInnerCommand)
        {
            IsInnerCommand = isInnerCommand;
        }

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }
}
