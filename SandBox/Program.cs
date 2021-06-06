using SimpleInjector;
using System;

namespace SandBox
{
    class Program
    {
        static readonly Container container;
        static Program()
        {
            container = new Container();

            container.Register<IImplementation, Second>();

            container.Verify();
        }

        static void Main(string[] args)
        {
            IImplementation handler = container.GetInstance<IImplementation>();

            handler.Method();

            Console.ReadLine();
        }
    }
}
