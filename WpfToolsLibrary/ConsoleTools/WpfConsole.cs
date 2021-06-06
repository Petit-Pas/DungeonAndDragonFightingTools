using BaseToolsLibrary.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfToolsLibrary.ConsoleTools
{
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

        #region AddEntry()

        public void AddEntry(string text)
        {
            AddEntry(text, DefaultFontWeight, DefaultFontColor, DefaultFontSize);
        }

        public void AddEntry(string text, IFontWeight fontWeight)
        {
            AddEntry(text, fontWeight, DefaultFontColor, DefaultFontSize);
        }

        public void AddEntry(string text, IFontWeight fontWeight, IFontColor fontColor)
        {
            AddEntry(text, fontWeight, fontColor, DefaultFontSize);
        }

        private Paragraph currentParagraph { get => ConsoleContent.Blocks.LastBlock as Paragraph; set { } }

        public void AddEntry(string text, IFontWeight fontWeight, IFontColor fontColor, int fontSize)
        {
            if (fontColor is WpfFontColor color && fontWeight is WpfFontWeight weight)
            {
                currentParagraph.Inlines.Add(new Run()
                {
                    Text = text,
                    Foreground = color.Brush,
                    FontSize = fontSize,
                    FontWeight = weight.FontWeight,
                });
            }
            else
            {
                Logger.Log($"WARNING: invalid type in WpfConsole.AddEntry(): {fontColor.GetType().ToString()} and {fontWeight.GetType().ToString()}");
            }
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
