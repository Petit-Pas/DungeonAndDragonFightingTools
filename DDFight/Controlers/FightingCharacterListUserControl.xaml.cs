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
    /// Interaction logic for FightingCharacterListUserControl.xaml
    /// </summary>
    public partial class FightingCharacterListUserControl : UserControl
    {
        public FightingCharacterListUserControl()
        {
            InitializeComponent();
            Loaded += FightingCharacterListUserControl_Loaded;
        }

        private void FightingCharacterListUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FightersControl.ItemsSource = Global.Context.FightingCharacters;
        }
    }
}
