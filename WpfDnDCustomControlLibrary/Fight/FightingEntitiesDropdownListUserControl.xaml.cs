using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
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

namespace WpfDnDCustomControlLibrary.Fight
{
    /// <summary>
    /// Interaction logic for FightingEntitiesDropdownListUserControl.xaml
    /// </summary>
    public partial class FightingEntitiesDropdownListUserControl : UserControl
    {
        public FightingEntitiesDropdownListUserControl()
        {
            Initialized += FightingEntitiesDropdownListUserControl_Initialized;
            InitializeComponent();
        }

        private void FightingEntitiesDropdownListUserControl_Initialized(object sender, EventArgs e)
        {
            HitAttackTargetComboControl.ItemsSource = FightersList.Instance.Elements;
            HitAttackTargetComboControl.SelectionChanged += HitAttackTargetComboControl_SelectionChanged;
        }

        private void HitAttackTargetComboControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HitAttackTargetComboControl.SelectedIndex != -1)
                Target = (PlayableEntity)HitAttackTargetComboControl.SelectedItem;
            else
                Target = null;
        }

        public PlayableEntity Target
        {
            get { return (PlayableEntity)this.GetValue(TargetProperty); }
            set { this.SetValue(TargetProperty, value); }
        }
        private static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            nameof(Target),
            typeof(PlayableEntity),
            typeof(FightingEntitiesDropdownListUserControl),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    }
}
