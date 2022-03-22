using BaseToolsLibrary.IO;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfToolsLibrary.ConsoleTools
{
    internal static class InlineCollectionExtensions
    {
        internal static void InsertAfter(this InlineCollection inlineCollection, Run run, int lastHash)
        {
            if (lastHash == -1)
            {
                inlineCollection.Add(run);
            }
            else
            {
                Inline previous = inlineCollection.First(i => i.GetHashCode() == lastHash);
                inlineCollection.InsertAfter(previous, run);
            }
        }
    }

    public class WpfConsole : ICustomConsole, INotifyPropertyChanged
    {
        public FlowDocument ConsoleContent
        {
            get
            {
                return _consoleContent;
            }
            set
            {
                _consoleContent = value;
                NotifyPropertyChanged();
            }
        }

        public IFontWeight DefaultFontWeight { get; set; } = new WpfFontWeight() { FontWeight = FontWeights.Normal };
        public IFontColor DefaultFontColor { get; set; } = new WpfFontColor() { Brush = new SolidColorBrush(Colors.White) };
        public int DefaultFontSize { get; set; } = 15;

        private FlowDocument _consoleContent = new FlowDocument();
        private Paragraph currentParagraph { get => ConsoleContent.Blocks.LastBlock as Paragraph; set { } }

        #region AddEntry()

        public int AddEntry(string text)
        {
            return AddEntry(text, DefaultFontWeight, DefaultFontColor, DefaultFontSize);
        }

        public int AddEntry(string text, IFontWeight fontWeight)
        {
            return AddEntry(text, fontWeight, DefaultFontColor, DefaultFontSize);
        }

        public int AddEntry(string text, IFontWeight fontWeight, IFontColor fontColor)
        {
            return AddEntry(text, fontWeight, fontColor, DefaultFontSize);
        }

        public int AddEntry(string text, IFontWeight fontWeight, IFontColor fontColor, int fontSize)
        {
            return ActuallyAddEntry(text, -1, fontWeight, fontColor, fontSize);
        }

        public int AddEntryAfter(int previousHash, string text)
        {
            return AddEntryAfter(previousHash, text, DefaultFontWeight);
        }

        public int AddEntryAfter(int previousHash, string text, IFontWeight fontWeight)
        {
            return AddEntryAfter(previousHash, text, fontWeight, DefaultFontColor);
        }

        public int AddEntryAfter(int previousHash, string text, IFontWeight fontWeight, IFontColor fontColor)
        {
            return AddEntryAfter(previousHash, text, fontWeight, DefaultFontColor, DefaultFontSize);
        }

        public int AddEntryAfter(int previousHash, string text, IFontWeight fontWeight, IFontColor fontColor, int fontSize)
        {
            return ActuallyAddEntry(text, previousHash, fontWeight, fontColor, fontSize);
        }

        private int ActuallyAddEntry(string text, int previousHash, IFontWeight fontWeight, IFontColor fontColor, int fontSize)
        {
            if (fontColor is WpfFontColor color && fontWeight is WpfFontWeight weight)
            {
                if (currentParagraph != null)
                {
                    var run = new Run()
                    {
                        Text = text,
                        Foreground = color.Brush,
                        FontSize = fontSize,
                        FontWeight = weight.FontWeight,
                    };

                    currentParagraph.Inlines.InsertAfter(run, previousHash);

                    return run.GetHashCode();
                }
            }
            Logger.Log($"WARNING: invalid type in WpfConsole.AddEntry(): {fontColor.GetType().ToString()} and {fontWeight.GetType().ToString()}");
            return -1;
        }

        #endregion AddEntry()

        public void NewParagraph()
        {
            ConsoleContent.Blocks.Add(new Paragraph());
        }

        public void Reset()
        {
            this.ConsoleContent = new FlowDocument();
        }

        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
