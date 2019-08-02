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

namespace WpfSandbox
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainDataContext _DataContext = new MainDataContext ();

        public MainWindow()
        {
            InitializeComponent();

            MainDataContext test = new MainDataContext();
            MainDataContext tes2 = new MainDataContext();

            Console.WriteLine(test == tes2);

            test.Embedded.EmbeddedInt += 1;

            Console.WriteLine(test == tes2);

            this.DataContext = _DataContext;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            _DataContext.MainInt += 1;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            _DataContext.Embedded.EmbeddedInt += 1;
        }

    }
}
