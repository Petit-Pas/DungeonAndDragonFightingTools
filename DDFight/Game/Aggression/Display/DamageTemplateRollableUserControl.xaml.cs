using System.Windows.Controls;

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
