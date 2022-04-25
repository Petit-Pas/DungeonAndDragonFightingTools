using System.Collections.Generic;
using System.Xml.Serialization;

namespace BaseToolsLibrary.Mediator
{
    /// <summary>
    ///     Base class for any command that will itself contain more smaller commands
    /// </summary>
    public abstract class SuperCommandBase : IMediatorCommand
    {
        [XmlElement]
        public Stack<IMediatorCommand> InnerCommands { get; set; }

        public SuperCommandBase()
        {
            InnerCommands = new Stack<IMediatorCommand>();
        }

        public void PushToInnerCommands(IMediatorCommand command)
        {
            InnerCommands.Push(command);
        }

        public IMediatorCommand PopLastInnerCommand()
        {
            return InnerCommands.Pop();
        }

        public IMediatorCommand PeekLastInnerCommand()
        {
            return InnerCommands.Peek();
        }
        
    }
}
