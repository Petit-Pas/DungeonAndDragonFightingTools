using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfDnDCustomControlLibrary.Attacks.Spells
{
    /// <summary>
    /// Logique d'interaction pour CantripLevelSelectorUserControl.xaml
    /// </summary>
    public partial class CantripLevelSelectorUserControl : UserControl
    {
        public CantripLevelSelectorUserControl()
        {
            InitializeComponent();
        }
        public event EventHandler SelectedLevelChanged;

        protected void RaiseSelectedLevelChangedEvent()
        {
            SelectedLevelChanged?.Invoke(this, null);
        }

        public delegate void SelectedLevelChangedEventHandler(object sender, EventArgs args);

        public int SelectedLevel
        {
            get { return (int)this.GetValue(SelectedLevelProperty); }
            set { this.SetValue(SelectedLevelProperty, value); }
        }
        private static readonly DependencyProperty SelectedLevelProperty = DependencyProperty.Register(
            nameof(SelectedLevel),
            typeof(int),
            typeof(CantripLevelSelectorUserControl),
            new FrameworkPropertyMetadata(-1,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (o, e) => ((CantripLevelSelectorUserControl)o).RaiseSelectedLevelChangedEvent()
                )
            );
    }
}
