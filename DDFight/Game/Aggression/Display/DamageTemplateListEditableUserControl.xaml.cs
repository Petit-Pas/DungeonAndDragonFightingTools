using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WpfToolsLibrary.Extensions;

namespace DDFight.Controlers.Game.Attacks.DamageListControls
{
    /// <summary>
    /// Logique d'interaction pour DamageListUserControl.xaml
    /// </summary>
    public partial class DamageTemplateListEditableUserControl : UserControl, INotifyPropertyChanged
    {
        private DamageTemplateList data_context
        {
            get => (DamageTemplateList)this.DataContext;
        }

        public bool HasSavingThrow
        {
            get { return (bool)GetValue(HasSavingThrowProperty); }
            set { SetValue(HasSavingThrowProperty, value); }
        }

        public static readonly DependencyProperty HasSavingThrowProperty =
            DependencyProperty.Register(nameof(HasSavingThrow), typeof(bool),
                typeof(DamageTemplateListEditableUserControl));

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
            DamageListView.ItemsSource = data_context;
        }

        private void AddDamage_Button_Click(object sender, RoutedEventArgs e)
        {
            data_context.AddElementSilent(new DamageTemplate("1d4", DamageTypeEnum.Force));
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
