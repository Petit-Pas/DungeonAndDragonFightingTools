using System.Windows;
using System.Windows.Controls;

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
