using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseToolsLibrary.Mediator
{
    /// <summary>
    ///     Base interface for the handler of any IMediatorCommand
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMediatorHandler<T> : IMediatorHandler
        where T : class, IMediatorCommand
    {
        // declarations should be in the other interface, the non generic one
    }

    public interface IMediatorHandler
    {
        // declarations should be in this interface, not the generic one
        
        void Execute(IMediatorCommand command);

        void Undo(IMediatorCommand command);
    }
}
