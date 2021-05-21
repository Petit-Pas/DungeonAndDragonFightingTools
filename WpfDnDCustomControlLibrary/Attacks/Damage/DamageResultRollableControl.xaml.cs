using DnDToolsLibrary.Attacks.Damage;
using System.Windows;
using System.Windows.Controls;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCustomControlLibrary.Attacks.Damage
{
    /// <summary>
    /// Logique d'interaction pour DamageTemplateRollableUserControl.xaml
    /// </summary>
    public partial class DamageResultRollableControl : UserControl, IRollableControl
    {
        private DamageResult data_context
        {
            get => DataContext as DamageResult;
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
          "Crits", typeof(bool), typeof(DamageResultRollableControl), 
          new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool CanBeAltered
        {
            get { return (bool)this.GetValue(CanBeAlteredProperty); }
            set { this.SetValue(CanBeAlteredProperty, value); }
        }
        public static readonly DependencyProperty CanBeAlteredProperty = DependencyProperty.Register(
            nameof(CanBeAltered),
            typeof(bool),
            typeof(DamageResultRollableControl),
            new FrameworkPropertyMetadata(false)
        );

        public DamageResultRollableControl()
        {
            InitializeComponent();
        }

        public void RollControl()
        {
            if (data_context != null)
                if (data_context.Damage.LastRoll == 0)
                    data_context.Damage.Roll(Crits);
        }

        public bool IsFullyRolled()
        {
            if (data_context != null)
                if (data_context.Damage.LastRoll == 0)
                    return false;
            return true;
        }
    }
}
