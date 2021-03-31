using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Entities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Controlers.Game.DamageAffinity
{
    /// <summary>
    /// Interaction logic for EditableDamageAffinityListUserControl.xaml
    /// </summary>
    public partial class DamageTypeAffinityListEditableUserControl : UserControl
    {
        private PlayableEntity _dataContext
        {
            get => (PlayableEntity)this.DataContext;
        }

        public DamageTypeAffinityListEditableUserControl()
        {
            InitializeComponent();
            Loaded += EditableDamageAffinityListUserControl_Loaded;
        }

        private void EditableDamageAffinityListUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<DamageTypeAffinity> items = new List<DamageTypeAffinity>();
            foreach (DamageTypeAffinity dc in _dataContext.DamageAffinities.AffinityList)
            {
                items.Add(dc);
            }
            DamageTypeAffinityListView.ItemsSource = items;
        }
    }
}
