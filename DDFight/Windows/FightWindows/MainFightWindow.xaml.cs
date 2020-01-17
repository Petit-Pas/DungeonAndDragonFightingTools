using DDFight.Controlers.InputBoxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Interaction logic for MainFightWindow.xaml
    /// </summary>
    public partial class MainFightWindow : Window
    {
        public MainFightWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Paragraph par = new Paragraph();
            Run tmp = new Run("toto");
            par.Inlines.Add(tmp);
            Global.Context.UserLogs.Blocks.Add(par);

            Console.WriteLine("COCHON: UserLogs now has n paragraophs: " + Global.Context.UserLogs.Blocks.Count());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ConsoleControl.RichTextBoxControl.ClearValue(BindableRichTextBox.DocumentProperty);
        }
    }
}
