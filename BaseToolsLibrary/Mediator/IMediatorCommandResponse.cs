using BaseToolsLibrary.Mediator.CommandStatii;
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

    public static class MediatorCommandStatii
    {
        public static MediatorCommandNoResponse NoResponse { get; } = new MediatorCommandNoResponse();
        public static MediatorCommandCanceled Canceled { get; } = new MediatorCommandCanceled();
        public static MediatorCommandSuccess Success { get; } = new MediatorCommandSuccess();
    }
}
