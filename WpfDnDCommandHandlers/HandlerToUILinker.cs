using BaseToolsLibrary.Mediator;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using WpfDnDCustomControlLibrary.Attacks.Damage;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCommandHandlers
{
    public static class HandlerToUILinker
    {
        private static Dictionary<Type, Type> _links = new Dictionary<Type, Type>();

        public static void AddNewPair(Type handler, Type window)
        {
            _links[handler] = window;
        }

        public static IValidableWindow GetWindow(Type handler)
        {
            return Activator.CreateInstance(_links[handler]) as IValidableWindow;
        }
    }
}
