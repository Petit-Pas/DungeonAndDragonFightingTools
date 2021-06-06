using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseToolsLibrary.IO
{
    public interface IFontWeightProvider
    {
        IFontWeight Bold { get; }
        IFontWeight SemiBold { get; }
        IFontWeight Normal { get; }
    }
}
