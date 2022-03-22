using DnDToolsLibrary.Attacks.Damage;
using System.Windows;
using System.Windows.Controls;

namespace WpfDnDCustomControlLibrary.Attacks.Damage
{
    /// <summary>
    /// Interaction logic for DamageModifierSelectableControl.xaml
    /// </summary>
    public partial class DamageModifierSelectableControl : UserControl
    {
        public DamageModifierSelectableControl()
        {
            InitializeComponent();
        }

        public DamageModifierEnum DamageModifier
        {
            get { return (DamageModifierEnum)this.GetValue(DamageModifierProperty); }
            set { this.SetValue(DamageModifierProperty, value); }
        }
        public static readonly DependencyProperty DamageModifierProperty = DependencyProperty.Register(
            nameof(DamageModifier),
            typeof(DamageModifierEnum),
            typeof(DamageModifierSelectableControl),
            new FrameworkPropertyMetadata(DamageModifierEnum.Normal, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );
    }
}
