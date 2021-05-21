using DnDToolsLibrary.Attacks.Damage;
using System.Windows;
using System.Windows.Controls;

namespace WpfDnDCustomControlLibrary.Attacks.Damage
{
    /// <summary>
    /// Logique d'interaction pour DamageResultListRollableUserControl.xaml
    /// </summary>
    public partial class DamageResultListRollableControl : UserControl
    {
        private DamageResultList data_context
        {
            get => DataContext as DamageResultList;
        }

        public DamageResultListRollableControl()
        {
            InitializeComponent();
        }

        public static void crit_Updated(DependencyObject o, DependencyPropertyChangedEventArgs a)
        {
        }

        /// <summary>
        ///     tells wheter the damage are applied with a crit
        /// </summary>
        public bool Crits
        {
            get { return (bool)this.GetValue(CritProperty); }
            set { this.SetValue(CritProperty, value); }
        }
        public static readonly DependencyProperty CritProperty = DependencyProperty.Register(
          "Crits", typeof(bool), typeof(DamageResultListRollableControl), 
          new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, crit_Updated));
    }
}
