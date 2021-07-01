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

namespace WpfDnDCustomControlLibrary.Attacks.Spells
{
    /// <summary>
    /// Logique d'interaction pour NormalSpellLevelSelectorUserControl.xaml
    /// </summary>
    public partial class NormalSpellLevelSelectorUserControl : UserControl
    {
        public NormalSpellLevelSelectorUserControl()
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
            typeof(NormalSpellLevelSelectorUserControl),
            new FrameworkPropertyMetadata(-1,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (o, e) => ((NormalSpellLevelSelectorUserControl)o).RaiseSelectedLevelChangedEvent()
                )
            );
    }
}
