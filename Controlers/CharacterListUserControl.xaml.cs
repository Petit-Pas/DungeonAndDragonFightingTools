using DDFight.Windows;
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
    /// Interaction logic for CharacterListUserControl.xaml
    /// </summary>
    public partial class CharacterListUserControl : UserControl
    {
        public CharacterListUserControl()
        {
            InitializeComponent();
        }

        private void AddCharacter(object sender, RoutedEventArgs e)
        {
            NewCharacterWindow character = new NewCharacterWindow();
            character.ShowDialog();
        }

        private void RemoveCharacter(object sender, RoutedEventArgs e)
        {

        }
    }
}
