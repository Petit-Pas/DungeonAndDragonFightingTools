using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfCustomControlLibrary.CircularSelector;

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

        private void RefreshEnabledButtons()
        {
            ResetButtons();
            if (DisabledLevels.Length != 0)
            {
                CheckButton(DisabledLevels, 1, Level1);
                CheckButton(DisabledLevels, 2, Level2);
                CheckButton(DisabledLevels, 3, Level3);
                CheckButton(DisabledLevels, 4, Level4);
                CheckButton(DisabledLevels, 5, Level5);
                CheckButton(DisabledLevels, 6, Level6);
                CheckButton(DisabledLevels, 7, Level7);
                CheckButton(DisabledLevels, 8, Level8);
                CheckButton(DisabledLevels, 9, Level9);
            }
        }

        private void CheckButton(int[] disabledLevels, int index, CircularSelectorRadioButtonControl level)
        {
            if (disabledLevels.Contains(index))
            {
                level.IsEnabled = false;
            }
        }

        private void ResetButtons()
        {
            Level1.IsEnabled = true;
            Level2.IsEnabled = true;
            Level3.IsEnabled = true;
            Level4.IsEnabled = true;
            Level5.IsEnabled = true;
            Level6.IsEnabled = true;
            Level7.IsEnabled = true;
            Level8.IsEnabled = true;
            Level9.IsEnabled = true;
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

        public int[] DisabledLevels
        {
            get { return (int[])this.GetValue(DisabledLevelProperty); }
            set { this.SetValue(DisabledLevelProperty, value); }
        }
        private static readonly DependencyProperty DisabledLevelProperty = DependencyProperty.Register(
            nameof(DisabledLevels),
            typeof(int[]),
            typeof(NormalSpellLevelSelectorUserControl),
            new FrameworkPropertyMetadata(Array.Empty<int>(),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (o, e) => ((NormalSpellLevelSelectorUserControl)o).RefreshEnabledButtons()
            )
        );
    }
}
