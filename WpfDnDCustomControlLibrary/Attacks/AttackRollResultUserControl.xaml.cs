using DnDToolsLibrary.Attacks;
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

namespace WpfDnDCustomControlLibrary.Attacks
{
    /// <summary>
    /// Interaction logic for AttackRollResultUserControl.xaml
    /// </summary>
    public partial class AttackRollResultUserControl : UserControl
    {
        public AttackRollResultUserControl()
        {
            DataContextChanged += AttackRollResultUserControl_DataContextChanged;
            InitializeComponent();
        }

        private void AttackRollResultUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is AttackRollResult result)
                RollResult = result;
        }

        public AttackRollResult RollResult
        {
            get { return (AttackRollResult)this.GetValue(RollResultProperty); }
            set { this.SetValue(RollResultProperty, value); }
        }
        private static readonly DependencyProperty RollResultProperty = DependencyProperty.Register(
            nameof(RollResult),
            typeof(AttackRollResult),
            typeof(AttackRollResultUserControl),
            new PropertyMetadata(new AttackRollResult()));

        public bool Crits
        {
            get { return (bool)this.GetValue(CritsProperty); }
            set { this.SetValue(CritsProperty, value); }
        }
        private static readonly DependencyProperty CritsProperty = DependencyProperty.Register(
            nameof(Crits),
            typeof(bool),
            typeof(AttackRollResultUserControl),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    }
}
