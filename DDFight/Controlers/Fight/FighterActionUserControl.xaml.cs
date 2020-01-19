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
    /// Logique d'interaction pour FighterActionUserControl.xaml
    /// </summary>
    public partial class FighterActionUserControl : UserControl
    {
        public FighterActionUserControl()
        {
            InitializeComponent();
        }

        private void NextTurn_Button_Click(object sender, RoutedEventArgs e)
        {
            Global.Context.FightContext.NextTurn();
        }
    }
}
