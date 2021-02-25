using DDFight.Game.DamageAffinity;
using DDFight.Tools;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Game.Aggression.Display
{
    /// <summary>
    /// Interaction logic for DotTemplateListEditableControl.xaml
    /// </summary>
    public partial class DotTemplateListEditableControl : UserControl, INotifyPropertyChanged
    {
        private DotTemplateList data_context
        {
            get => (DotTemplateList)this.DataContext;
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
            DamageListView.ItemsSource = data_context.Elements;
        }

        private void AddDamage_Button_Click(object sender, RoutedEventArgs e)
        {
            data_context.AddElementSilent(new DotTemplate("1d4", DamageTypeEnum.Force));
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
