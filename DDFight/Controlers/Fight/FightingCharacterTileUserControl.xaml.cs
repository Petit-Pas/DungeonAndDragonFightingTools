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
    /// Logique d'interaction pour FightingCharacterTileDataContext.xaml
    /// </summary>
    public partial class FightingCharacterTileUserControl : UserControl
    {
        public FightingCharacterTileUserControl()
        {
            InitializeComponent();
        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("in button click");
        }

        private void MainControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("in mouse up event");
        }
    }
}
