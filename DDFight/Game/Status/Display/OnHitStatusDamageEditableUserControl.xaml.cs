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

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Interaction logic for OnHitStatusDamageEditableUserControl.xaml
    /// </summary>
    public partial class OnHitStatusDamageEditableUserControl : UserControl
    {
        public OnHitStatusDamageEditableUserControl()
        {
            Loaded += OnHitStatusDamageEditableUserControl_Loaded;
            InitializeComponent();
        }

        private void OnHitStatusDamageEditableUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OnApplyDamageListControl.HeaderTextControl.Text = "On Apply Damage";
            DotDamageControl.HeaderTextControl.Text = "Dot Damage";
        }
    }
}
