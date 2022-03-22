using DnDToolsLibrary.Attacks.HitAttacks;
using System.Windows;
using System.Windows.Controls;

namespace WpfDnDCustomControlLibrary.Attacks.HitAttacks
{
    /// <summary>
    ///     Interaction logic for HitAttackResultUserControl.xaml
    /// </summary>
    public partial class HitAttackResultUserControl : UserControl
    {
        public HitAttackResultUserControl()
        {
            DataContextChanged += HitAttackResultUserControl_DataContextChanged;
            Loaded += HitAttackResultUserControl_Loaded;
            InitializeComponent();
        }

        private void HitAttackResultUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.TargetSelectionControl.Focus();
        }

        private void HitAttackResultUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is HitAttackResult attackResult)
                AttackResult = attackResult;
        }

        public HitAttackResult AttackResult
        {
            get { return (HitAttackResult)this.GetValue(AttackResultProperty); }
            set { this.SetValue(AttackResultProperty, value); }
        }
        private static readonly DependencyProperty AttackResultProperty = DependencyProperty.Register(
            nameof(AttackResult),
            typeof(HitAttackResult),
            typeof(HitAttackResultUserControl),
            new PropertyMetadata(null));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.TargetSelectionControl.Focus();
        }
    }
}
