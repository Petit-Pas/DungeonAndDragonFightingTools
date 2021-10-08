using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseToolsLibrary
{
    public interface IEquivalentComparable<T>
        where T : class
    {
        bool IsEquivalentTo(T toCompare);
    }
}
