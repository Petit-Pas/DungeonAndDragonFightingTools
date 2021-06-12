using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BaseToolsLibrary.Mediator
{
    /// <summary>
    ///     Base class for any command that will itself contain more smaller commands
    /// </summary>
    public abstract class BaseSuperCommand : IMediatorCommand
    {
        [XmlElement]
        public Stack<IMediatorCommand> InnerCommands { get; set; }

        public BaseSuperCommand()
        {
            InnerCommands = new Stack<IMediatorCommand>();
        }

        public void AddToInnerCommands(IMediatorCommand command)
        {
            InnerCommands.Push(command);
        }

        public IMediatorCommand GetLastInnerCommand()
        {
            return InnerCommands.Pop();
        }
        
    }
}
