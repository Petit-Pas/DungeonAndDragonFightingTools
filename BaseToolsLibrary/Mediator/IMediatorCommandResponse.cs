﻿using BaseToolsLibrary.Mediator.CommandStatii;
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
        public static IMediatorCommandResponse NoResponse { get; } = new MediatorCommandNoResponse();
        public static IMediatorCommandResponse Canceled { get; } = new MediatorCommandCanceled();
        public static IMediatorCommandResponse Success { get; } = new MediatorCommandSuccess();
        public static IMediatorCommandResponse Failed { get; } = new MediatorCommandFailed();
    }
}
