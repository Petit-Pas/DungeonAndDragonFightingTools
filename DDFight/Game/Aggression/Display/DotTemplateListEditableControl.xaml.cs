using DDFight.Game.DamageAffinity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace DDFight.Game.Aggression.Display
{
    /// <summary>
    /// Interaction logic for DotTemplateListEditableControl.xaml
    /// </summary>
    public partial class DotTemplateListEditableControl : UserControl, INotifyPropertyChanged
    {
        private List<DotTemplate> data_context
        {
            get => (List<DotTemplate>)this.DataContext;
        }

        public DotTemplateListEditableControl()
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
            List<DotTemplate> list = new List<DotTemplate>();
            foreach (DotTemplate tmp in data_context)
            {
                list.Add(tmp);
            }
            DamageListView.ItemsSource = list;
        }

        private void AddDamage_Button_Click(object sender, RoutedEventArgs e)
        {
            data_context.Add(new DotTemplate("1d4", DamageTypeEnum.Force));
            refresh_damage_list();
        }

        private void RemoveDamage_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            foreach (DotTemplate tmp in DamageListView.ItemsSource)
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

        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
