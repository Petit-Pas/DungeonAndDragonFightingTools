using DnDToolsLibrary.Attacks.Damage;
using System;
using System.Collections.Generic;
using System.Linq;
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
