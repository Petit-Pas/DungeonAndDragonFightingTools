using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseToolsLibrary.IO
{
    public interface ICustomConsole
    {
        void Reset();
        void NewParagraph();
        void AddEntry(string text);
        void AddEntry(string text, IFontWeight fontWeight);
        void AddEntry(string text, IFontWeight fontWeight, IFontColor fontColor);
        void AddEntry(string text, IFontWeight fontWeight, IFontColor fontColor, int fontSize);

        IFontWeight DefaultFontWeight { get; set; }
        IFontColor DefaultFontColor { get; set; }
        int DefaultFontSize { get; set; }
    }
}
