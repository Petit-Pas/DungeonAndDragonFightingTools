using DDFight.Game;
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
    /// Interaction logic for CharacteristicsUserControl.xaml
    /// </summary>
    public partial class CharacteristicsUserControl : UserControl
    {
        public CharacteristicsUserControl()
        {
            InitializeComponent();
        }

        private void CheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                System.Windows.Input.KeyEventArgs args = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, Key.Tab);
                args.RoutedEvent = Keyboard.KeyDownEvent;
                InputManager.Current.ProcessInput(args);
            }
        }
    }
}
