using DDFight.Game;
using DDFight.Game.Characteristics;
using DDFight.Game.DamageAffinity;
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

namespace DDFight.Controlers.Game.DamageAffinity
{
    /// <summary>
    /// Interaction logic for EditableDamageAffinityListUserControl.xaml
    /// </summary>
    public partial class EditableDamageAffinityListUserControl : UserControl
    {
        private PlayableEntity _dataContext
        {
            get => (PlayableEntity)this.DataContext;
        }

        public EditableDamageAffinityListUserControl()
        {
            InitializeComponent();
            Loaded += EditableDamageAffinityListUserControl_Loaded;
        }

        private void EditableDamageAffinityListUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<DamageTypeAffinityDataContext> items = new List<DamageTypeAffinityDataContext>();
            foreach (DamageTypeAffinityDataContext dc in _dataContext.DamageAffinities.DamageTypeAffinityList)
            {
                items.Add(dc);
            }
            DamageTypeAffinityListView.ItemsSource = items;
        }
    }
}
