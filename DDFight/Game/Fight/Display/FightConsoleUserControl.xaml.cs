using System.Windows.Controls;

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
            DataContext = GlobalContext.Context;
            RichTextBoxControl.TextChanged += RichTextBoxControl_TextChanged;
            RichTextBoxControl.ScrollToEnd();
        }

        private void RichTextBoxControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            ScrollViewerControl.ScrollToEnd();
        }
    }
}
