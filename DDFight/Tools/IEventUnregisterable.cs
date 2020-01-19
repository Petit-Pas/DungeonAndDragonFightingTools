using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Tools
{
    /// <summary>
    ///     Used to unregister UIElement from their external EventHandler in order to avoid having zombies instance of them
    /// </summary>
    public interface IEventUnregisterable
    {
        void UnregisterToAll();
    }
}
