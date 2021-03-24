using DDFight.Game.Aggression.Attacks;
using DDFight.Tools;
using DDFight.ValidationRules;
using System.Windows.Controls;
using WpfToolsLibrary.ValidationRules;

namespace DDFight.Controlers.Game.Attacks
{
    /// <summary>
    /// Interaction logic for EditableHitAttackTemplate.xaml
    /// </summary>
    public partial class HitAttackTemplateEditableUserControl : UserControl, IValidable
    {
        public HitAttackTemplateEditableUserControl()
        {
            InitializeComponent();
            Loaded += EditableHitAttackTemplateUserControl_Loaded;
        }

        private void EditableHitAttackTemplateUserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            NameTextBox.SetFocus();
        }

        public bool IsValid()
        {
            return this.AreAllChildrenValid();
        }
    }
}
