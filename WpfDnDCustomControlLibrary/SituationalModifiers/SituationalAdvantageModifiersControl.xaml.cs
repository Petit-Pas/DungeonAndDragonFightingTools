using DnDToolsLibrary.Attacks;
using System.Windows;
using System.Windows.Controls;

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
