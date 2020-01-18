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

namespace DDFight.Controlers.Fight
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

            /*Paragraph par = new Paragraph();
            par.Inlines.Add(test);*/

        }

        private void RichTextBoxControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            RichTextBoxControl.ScrollToEnd();
        }
    }
}
