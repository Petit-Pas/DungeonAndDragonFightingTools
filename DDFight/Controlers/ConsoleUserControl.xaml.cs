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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DDFight.Controlers
{
    /// <summary>
    /// Logique d'interaction pour Console.xaml
    /// </summary>
    public partial class ConsoleUserControl : UserControl
    {

        public ConsoleUserControl()
        {

            InitializeComponent();
            DataContext = Global.Context;

            /*FlowDocument doc = new FlowDocument();

            for (int i = 0; i != 20; i += 1)
            {
                Paragraph par = new Paragraph();
                par.Inlines.Add(new Run("test1\r\n"));
                par.Inlines.Add(new Run("test2\r\n"));
                par.Inlines.Add(new Run("test3\r\n"));
                par.Inlines.Add(new Run("test4\r\n"));
                par.Inlines.Add(new Run("test5\r\n"));
                par.Inlines.Add(new Run("test6\r\n"));

                doc.Blocks.Add(par);
            }

            RichTextBoxControl.Document = doc;*/

            RichTextBoxControl.ScrollToEnd();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Paragraph par = new Paragraph();
            par.Inlines.Add(new Run("toto\r\n"));
            par.Inlines.Add(new Run("toto\r\n"));
            par.Inlines.Add(new Run("toto\r\n"));
            par.Inlines.Add(new Run("toto\r\n"));
            par.Inlines.Add(new Run("toto\r\n"));
            par.Inlines.Add(new Run("toto\r\n"));
            Global.Context.UserLogs.Blocks.Add(par);

            Console.WriteLine("logs is now:" + Global.Context.UserLogs.Blocks.Count());
        }
    }
}
