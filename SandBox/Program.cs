using System;

namespace SandBox
{
    class Program
    {
        public class Test
        {
            public int test = 0;

            public Test()
            {
                Console.WriteLine("base ctor");
                test = 1;
            }

            public Test(int oui) : this()
            {
                Console.WriteLine("allo?");
            }
        }

        static void Main(string[] args)
        {
            Test test = new Test(0);

            Console.ReadKey();
        }
    }
}
