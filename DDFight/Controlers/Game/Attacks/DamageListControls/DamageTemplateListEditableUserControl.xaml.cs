using DDFight.Game.Aggression;
using DDFight.Game.DamageAffinity;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Controlers.Game.Attacks.DamageListControls
{
    /// <summary>
    /// Logique d'interaction pour DamageListUserControl.xaml
    /// </summary>
    public partial class DamageTemplateListEditableUserControl : UserControl
    {
        private List<DamageTemplate> data_context
        {
            get => (List<DamageTemplate>)this.DataContext;
        }

        public DamageTemplateListEditableUserControl()
        {
            InitializeComponent();

            Loaded += DamageListUserControl_Loaded;
        }

        private void DamageListUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            refresh_damage_list();
        }

        private void refresh_damage_list()
        {
            List<DamageTemplate> list = new List<DamageTemplate>();
            foreach (DamageTemplate tmp in data_context)
            {
                list.Add(tmp);
            }
            DamageListView.ItemsSource = list;
        }

        private void AddDamage_Button_Click(object sender, RoutedEventArgs e)
        {
            data_context.Add(new DamageTemplate("1d4", DamageTypeEnum.Force));
            refresh_damage_list();
        }

        private void RemoveDamage_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            foreach (DamageTemplate tmp in DamageListView.ItemsSource)
            {
                if (tmp == ((Button)sender).DataContext)
                {
                    data_context.RemoveAt(index);
                    break;
                }
                index += 1;
            }
            refresh_damage_list();
        }

        public bool IsValid()
        {
            return this.AreAllChildrenValid();
        }
    }
}
