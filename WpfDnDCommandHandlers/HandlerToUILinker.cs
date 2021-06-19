using BaseToolsLibrary.Mediator;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using WpfDnDCommandHandlers.AttackCommands.DamageCommands.CalculateDamageResultList;
using WpfDnDCustomControlLibrary.Attacks.Damage;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCommandHandlers
{
    public static class HandlerToUILinker
    {
        private static Dictionary<Type, IValidableWindow> _links = new Dictionary<Type, IValidableWindow>();

        public static void AddNewPair(Type handler, IValidableWindow window)
        {
            _links[handler] = window;
        }

        public static IValidableWindow GetWindow(Type handler)
        {
            return _links[handler];
        }
    }
}
