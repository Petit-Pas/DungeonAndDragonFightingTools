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

namespace WpfDnDCustomControlLibrary.SituationalModifiers
{
    /// <summary>
    /// Interaction logic for SituationalAdvantageModifiersUserControl.xaml
    /// </summary>
    public partial class SituationalAdvantageModifiersControl : UserControl
    {
        public SituationalAdvantageModifiersControl()
        {
            DataContextChanged += SituationalAdvantageModifiersUserControl_DataContextChanged;
            InitializeComponent();
        }

        private void SituationalAdvantageModifiersUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is SituationalAdvantageModifiers modifier)
                Modifiers = modifier;
        }

        public SituationalAdvantageModifiers Modifiers
        {
            get { return (SituationalAdvantageModifiers)this.GetValue(ModifiersProperty); }
            set { this.SetValue(ModifiersProperty, value); }
        }
        private static readonly DependencyProperty ModifiersProperty = DependencyProperty.Register(
            nameof(Modifiers),
            typeof(SituationalAdvantageModifiers),
            typeof(SituationalAdvantageModifiersControl),
            new PropertyMetadata(new SituationalAdvantageModifiers()));
    }
}
