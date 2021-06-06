using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseToolsLibrary.DependencyInjection
{
    public static class DIContainer
    {
        public static Container container = new Container();

        public static void RegisterTransient<T, U>() 
            where T : class
            where U : class, T
        {
            container.Register<T, U>();
        }

        public static void RegisterSingleton<T, U>()
            where T : class
            where U : class, T
        {
            container.RegisterSingleton<T, U>();
        }

        public static T GetImplementation<T>()
            where T : class
        {
            return container.GetInstance<T>();
        }

        public static void Verify()
        {
            container.Verify();
        }

    }
}
