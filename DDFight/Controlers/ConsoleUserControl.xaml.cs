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
            RichTextBoxControl.TextChanged += RichTextBoxControl_TextChanged;
            RichTextBoxControl.ScrollToEnd();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Run test = new Run("TEST");
            test.FontSize = 22;

            //test.Foreground = Application.Current.Resources.; //Application.Current.Resources["Light"];

            test.Foreground = (Brush)this.FindResource("Light");
            test.FontWeight = FontWeights.SemiBold;
            //test.FontWeight.

            //test.Foreground = new 

            //test.Foreground = StaticResourceExtension;
            Paragraph par = new Paragraph();
            par.Inlines.Add(test);


            par.Inlines.Add(new Run("toto\r\n"));
            par.Inlines.Add(new Run("toto\r\n"));
            par.Inlines.Add(new Run("toto\r\n"));
            par.Inlines.Add(new Run("toto\r\n"));
            par.Inlines.Add(new Run("toto\r\n"));
            par.Inlines.Add(new Run("toto " + Global.Context.UserLogs.Blocks.Count() + "\r\n"));
            Global.Context.UserLogs.Blocks.Add(par);


            Console.WriteLine("logs is now:" + Global.Context.UserLogs.Blocks.Count());

            //Global.Context.UserLogs.



        }

        private void RichTextBoxControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            RichTextBoxControl.ScrollToEnd();
        }

        private void RichTextBoxControl_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            Console.WriteLine("COCHON: alo?");
            RichTextBoxControl.ScrollToEnd();
        }
    }
}
