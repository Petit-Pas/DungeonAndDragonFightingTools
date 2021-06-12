using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseToolsLibrary.Mediator
{
    /// <summary>
    ///     Mediator interface
    /// </summary>
    public interface IMediator
    {
        void Undo(IMediatorCommand command);

        void Execute(IMediatorCommand command);
    }
}
