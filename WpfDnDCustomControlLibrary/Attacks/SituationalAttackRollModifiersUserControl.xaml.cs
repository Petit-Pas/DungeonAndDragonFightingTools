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
    /// Interaction logic for SituationalAttackRollModifiersUserControl.xaml
    /// </summary>
    public partial class SituationalAttackRollModifiersUserControl : UserControl
    {
        public SituationalAttackRollModifiersUserControl()
        {
            DataContextChanged += SituationalAttackRollModifiersUserControl_DataContextChanged;
            InitializeComponent();
        }

        private void SituationalAttackRollModifiersUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is SituationalAttackRollModifiers modifier)
                Modifiers = modifier;
        }

        public SituationalAttackRollModifiers Modifiers
        {
            get { return (SituationalAttackRollModifiers)this.GetValue(ModifiersProperty); }
            set { this.SetValue(ModifiersProperty, value); }
        }
        private static readonly DependencyProperty ModifiersProperty = DependencyProperty.Register(
            nameof(Modifiers),
            typeof(SituationalAttackRollModifiers),
            typeof(SituationalAttackRollModifiersUserControl),
            new PropertyMetadata(new SituationalAttackRollModifiers()));
    }
}
