using SimpleInjector;

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

        public static void RegisterSingleton<T, U>(U instance)
            where T : class
            where U : class, T
        {
            container.RegisterInstance<T>(instance);
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
