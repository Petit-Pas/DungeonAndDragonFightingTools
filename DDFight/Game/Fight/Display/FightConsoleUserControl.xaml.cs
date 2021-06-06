using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using System.Windows.Controls;
using WpfToolsLibrary.ConsoleTools;

namespace DDFight.Controlers.Fight
{
    /// <summary>
    /// Logique d'interaction pour Console.xaml
    /// </summary>
    public partial class FightConsoleUserControl : UserControl
    {
        private WpfConsole console = DIContainer.GetImplementation<ICustomConsole>() as WpfConsole;

        public FightConsoleUserControl()
        {
            InitializeComponent();
            DataContext = console.ConsoleContent;
            RichTextBoxControl.TextChanged += RichTextBoxControl_TextChanged;
            RichTextBoxControl.ScrollToEnd();
        }

        private void RichTextBoxControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            ScrollViewerControl.ScrollToEnd();
        }
    }
}
