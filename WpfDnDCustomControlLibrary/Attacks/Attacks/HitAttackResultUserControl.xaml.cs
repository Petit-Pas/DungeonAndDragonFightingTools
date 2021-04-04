using DnDToolsLibrary.Attacks.HitAttacks;
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

namespace WpfDnDCustomControlLibrary.Attacks.Attacks
{
    /// <summary>
    ///     Interaction logic for HitAttackResultUserControl.xaml
    /// </summary>
    public partial class HitAttackResultUserControl : UserControl
    {
        public HitAttackResultUserControl()
        {
            DataContextChanged += HitAttackResultUserControl_DataContextChanged;
            InitializeComponent();
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
            new PropertyMetadata(new HitAttackResult()));
    }
}
