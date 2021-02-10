using DDFight.ValidationRules;
using System.Windows.Controls;

namespace DDFight.Controlers.Monsters
{
    /// <summary>
    /// Logique d'interaction pour MonsterMainInfoUserControl.xaml
    /// </summary>
    public partial class MonsterMainInfoUserControl : UserControl, IValidable
    {
        
        public MonsterMainInfoUserControl()
        {
            InitializeComponent();
            Loaded += MonsterMainInfoUserControl_Loaded;
        }

        private void MonsterMainInfoUserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            NameBoxUserControl.Focus();
        }

        public bool IsValid()
        {
            return this.AreAllChildrenValid();
        }
    }
}
