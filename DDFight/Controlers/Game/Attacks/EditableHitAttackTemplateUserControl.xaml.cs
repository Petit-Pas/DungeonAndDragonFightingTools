using DDFight.Game.Aggression.Attacks;
using DDFight.ValidationRules;
using System.Windows.Controls;

namespace DDFight.Controlers.Game.Attacks
{
    /// <summary>
    /// Interaction logic for EditableHitAttackTemplate.xaml
    /// </summary>
    public partial class EditableHitAttackTemplateUserControl : UserControl, IValidable
    {
        private HitAttackTemplate _dataContext
        {
            get => (HitAttackTemplate)this.DataContext;
        }

        public EditableHitAttackTemplateUserControl()
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
