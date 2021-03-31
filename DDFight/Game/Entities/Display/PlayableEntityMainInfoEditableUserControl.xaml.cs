using System.Windows.Controls;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.ValidationRules;

namespace DDFight.Game.Entities.Display
{
    /// <summary>
    /// Interaction logic for PlayableEntityMainInfoEditableUserControl.xaml
    /// </summary>
    public partial class PlayableEntityMainInfoEditableUserControl : UserControl, IValidable
    {

        public PlayableEntityMainInfoEditableUserControl()
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
