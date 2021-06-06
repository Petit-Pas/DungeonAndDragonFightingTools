using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBox
{
    public class Consumer
    {
        public Consumer(IImplementation implementation)
        {
            implementation.Method();
        }
    }
}
