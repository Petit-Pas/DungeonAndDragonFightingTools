using DDFight.Controlers.InputBoxes;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Controlers.Game.Attacks.DamageListControls
{
    /// <summary>
    /// Logique d'interaction pour PlayableDamageListUserControl.xaml
    /// </summary>
    public partial class DamageTemplateListRollableUserControl : UserControl, INotifyPropertyChanged
    {


        public DamageTemplateListRollableUserControl()
        {
            InitializeComponent();

            Loaded += PlayableDamageListUserControl_Loaded;
        }

        private void PlayableDamageListUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DamageControl.ItemsSource = (System.Collections.IEnumerable)DataContext;
        }

        private void PositiveIntTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            PositiveIntTextBox textBox = (PositiveIntTextBox)sender;
            if (textBox.IsValid())
            {
                NotifyPropertyChanged("Damage");
            }
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
        private void NotifyPropertyChanged(string propertyName = "default")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
