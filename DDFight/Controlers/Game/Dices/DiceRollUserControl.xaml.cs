using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Dices;
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

namespace DDFight.Controlers.Game.Dices
{
    /// <summary>
    /// Interaction logic for DiceRollUserControl.xaml
    /// </summary>
    public partial class DiceRollUserControl : UserControl
    {
        public DiceRollUserControl()
        {
            InitializeComponent();
            AttackTemplate ctx = new AttackTemplate();
            ctx.Damage = new DiceRoll("1d4");
            DataContext = ctx;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Value currently is: " + ((AttackTemplate)DataContext).Damage.ToString());
        }
    }
}
