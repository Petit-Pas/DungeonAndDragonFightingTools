using System.Collections.Generic;
using BaseToolsLibrary.Extensions;

namespace BaseToolsLibrary.IO
{
    public interface ICustomConsole
    {
        void Reset();
        void NewParagraph();
        int AddEntry(string text);
        int AddEntry(string text, IFontWeight fontWeight);
        int AddEntry(string text, IFontWeight fontWeight, IFontColor fontColor);
        /// <summary>
        ///     Adds an entry to a console
        /// </summary>
        /// <param name="text"> the text to write </param>
        /// <param name="fontWeight"> describes the weight to use </param>
        /// <param name="fontColor"> describes the color to use </param>
        /// <param name="fontSize"> the size of the text font </param>
        /// <returns> the hash of the entry created, used so that a later message can be inserted </returns>
        int AddEntry(string text, IFontWeight fontWeight, IFontColor fontColor, int fontSize);

        int AddEntryAfter(int previousHash, string text);
        int AddEntryAfter(int previousHash, string text, IFontWeight fontWeight);
        int AddEntryAfter(int previousHash, string text, IFontWeight fontWeight, IFontColor fontColor);
        int AddEntryAfter(int previousHash, string text, IFontWeight fontWeight, IFontColor fontColor, int fontSize);


        IFontWeight DefaultFontWeight { get; set; }
        IFontColor DefaultFontColor { get; set; }
        int DefaultFontSize { get; set; }
        void RemoveLastParagraph();
        void RemoveEntries(IEnumerable<int> commandLogMessages);

        void AddIndentation(int amount);
        void RemoveIndentation(int amount);
    }
}
