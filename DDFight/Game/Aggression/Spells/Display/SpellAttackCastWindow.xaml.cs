using DDFight.Tools.UXShortcuts;
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
using System.Windows.Shapes;

namespace DDFight.Game.Aggression.Spells.Display
{
    /// <summary>
    /// Interaction logic for SpellAttackCastWindow.xaml
    /// </summary>
    public partial class SpellAttackCastWindow : Window, IRollableControl
    {
        public SpellAttackCastWindow()
        {
            InitializeComponent();
        }

        public bool IsFullyRolled()
        {
            return RollableWindowTool.AreAllRollableChildrenRolled(this);
        }

        public void RollControl()
        {
            RollableWindowTool.RollRollableChildren(this);
        }

        private void CastButtonControl_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (RollableWindowTool.IsRollControlPressed(e))
            {
                e.Handled = true;
                if (!IsFullyRolled())
                    RollControl();
            }
        }
    }
}
