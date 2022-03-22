using System;
using System.Collections.Generic;
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
