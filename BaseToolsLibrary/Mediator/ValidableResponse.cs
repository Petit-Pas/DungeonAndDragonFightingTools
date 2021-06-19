using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseToolsLibrary.Mediator
{
    /// <summary>
    ///     This can be used for commands that will use UI, as the command can be canceled by the user
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidableResponse<T> : IMediatorCommandResponse
    {

        public ValidableResponse(bool isValid, T response)
        {
            IsValid = isValid;
            Response = response;
        }

        public bool IsValid { get; set; }

        public T Response { get; set; }
    }
}
