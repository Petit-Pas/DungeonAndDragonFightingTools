using DDFight.ValidationRules;
using System.Windows.Controls;

namespace DDFight.Controlers.Characters
{
    /// <summary>
    /// Logique d'interaction pour CharacterMainInfoUserControl.xaml
    /// </summary>
    public partial class CharacterMainInfoUserControl : UserControl, IValidable
    {
        
        public CharacterMainInfoUserControl()
        {
            InitializeComponent();
            Loaded += CharacterMainInfoUserControl_Loaded;
        }

        private void CharacterMainInfoUserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            NameBoxUserControl.Focus();
        }

        public bool IsValid()
        {
            return this.AreAllChildrenValid();
        }
    }
}
