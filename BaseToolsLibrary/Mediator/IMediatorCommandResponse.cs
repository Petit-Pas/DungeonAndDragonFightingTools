using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseToolsLibrary.Mediator
{
    public interface IMediatorCommandResponse
    {
    }

    public static class MediatorCommandResponses
    {
        public static NoResponse NoResponse { get; } = new NoResponse();
    }
}
