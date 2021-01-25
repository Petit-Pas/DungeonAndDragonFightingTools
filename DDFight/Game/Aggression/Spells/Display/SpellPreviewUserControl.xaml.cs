using System.Windows;
using System.Windows.Controls;

namespace DDFight.Game.Aggression.Spells.Display
{
    /// <summary>
    /// Interaction logic for SpellPreviewUserControl.xaml
    /// </summary>
    public partial class SpellPreviewUserControl : UserControl
    {
        public SpellPreviewUserControl()
        {
            InitializeComponent();
            DataContextChanged += SpellPreviewUserControl_DataContextChanged;
        }

        private void SpellPreviewUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext == null)
                this.Visibility = Visibility.Collapsed;
            else
                this.Visibility = Visibility.Visible;
        }
    }
}
