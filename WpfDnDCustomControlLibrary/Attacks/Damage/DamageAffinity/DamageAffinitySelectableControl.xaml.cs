using DnDToolsLibrary.Attacks.Damage.Type;
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
            new PropertyMetadata(DamageAffinityEnum.Neutral)
        );
    }
}
