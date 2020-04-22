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

namespace DDFight.Game.Aggression.Display
{
    /// <summary>
    /// Logique d'interaction pour DamageTemplateRollableUserControl.xaml
    /// </summary>
    public partial class DamageTemplateRollableUserControl : UserControl
    {
        private DamageTemplate data_context
        {
            get => (DamageTemplate)DataContext;
        }

        public DamageTemplateRollableUserControl()
        {
            InitializeComponent();
        }
    }
}
