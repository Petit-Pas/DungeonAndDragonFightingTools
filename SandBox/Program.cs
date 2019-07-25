using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBox
{
    class Program
    {
        interface Ia
        {
            void Method();
        }

        class a : Ia
        {
            void Ia.Method()
            {
                throw new NotImplementedException();
            }
        }

        static void test (Ia A)
        {
            A.Method();
        }

        static void Main(string[] args)
        {
            a A = new a(); 
            switch (A)
            {
                case Ia yes:
                    test(yes);
                    break;
                default:
                    break;
            }
            Console.ReadLine();
        }
    }
}
