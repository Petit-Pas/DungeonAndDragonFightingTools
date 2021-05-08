using System.Windows.Controls;
using WpfToolsLibrary.ConsoleTools;

namespace DDFight.Controlers.Fight
{
    /// <summary>
    /// Logique d'interaction pour Console.xaml
    /// </summary>
    public partial class FightConsoleUserControl : UserControl
    {

        public FightConsoleUserControl()
        {
            InitializeComponent();
            DataContext = FightConsole.Instance;
            RichTextBoxControl.TextChanged += RichTextBoxControl_TextChanged;
            RichTextBoxControl.ScrollToEnd();
        }

        private void RichTextBoxControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            ScrollViewerControl.ScrollToEnd();
        }
    }
}
