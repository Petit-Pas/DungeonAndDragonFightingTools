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
          new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool CanBeAltered
        {
            get { return (bool)this.GetValue(CanBeAlteredProperty); }
            set { this.SetValue(CanBeAlteredProperty, value); }
        }
        public static readonly DependencyProperty CanBeAlteredProperty = DependencyProperty.Register(
            nameof(CanBeAltered),
            typeof(bool),
            typeof(DamageResultListRollableControl),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );

        public bool EditModeEnabled
        {
            get { return (bool)this.GetValue(EditModeEnabledProperty); }
            set { this.SetValue(EditModeEnabledProperty, value); }
        }
        private static readonly DependencyProperty EditModeEnabledProperty = DependencyProperty.Register(
            nameof(EditModeEnabled),
            typeof(bool),
            typeof(DamageResultListRollableControl),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool Rollable
        {
            get { return (bool)this.GetValue(RollableProperty); }
            set { this.SetValue(RollableProperty, value); }
        }
        private static readonly DependencyProperty RollableProperty = DependencyProperty.Register(
            nameof(Rollable),
            typeof(bool),
            typeof(DamageResultListRollableControl),
            new PropertyMetadata(true));

        private void EditModeChange(object sender, RoutedEventArgs e)
        {
            CanBeAltered = !CanBeAltered;
        }
    }
}
