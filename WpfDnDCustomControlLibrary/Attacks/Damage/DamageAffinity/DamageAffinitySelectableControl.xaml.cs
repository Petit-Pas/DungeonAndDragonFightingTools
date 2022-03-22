using DnDToolsLibrary.Attacks.Damage.Type;
using System.Windows;
using System.Windows.Controls;

namespace WpfDnDCustomControlLibrary.Attacks.Damage.DamageAffinity
{
    /// <summary>
    /// Interaction logic for DamageAffinitySelectableControl.xaml
    /// </summary>
    public partial class DamageAffinitySelectableControl : UserControl
    {
        public DamageAffinitySelectableControl()
        {
            InitializeComponent();
        }

        public DamageAffinityEnum DamageAffinity
        {
            get { return (DamageAffinityEnum)this.GetValue(DamageAffinityProperty); }
            set { this.SetValue(DamageAffinityProperty, value); }
        }

        public static readonly DependencyProperty DamageAffinityProperty = DependencyProperty.Register(
            nameof(DamageAffinity),
            typeof(DamageAffinityEnum),
            typeof(DamageAffinitySelectableControl),
            new FrameworkPropertyMetadata(DamageAffinityEnum.Neutral, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );
    }
}
