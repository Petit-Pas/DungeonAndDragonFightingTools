﻿using System;
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
        // TODO should this be XmlIgnored ?
        // we probably only need the base data of the super command in order to recreate them all with a call to Execute()
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
