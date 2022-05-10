using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System;
using System.Windows;
using BaseToolsLibrary.DependencyInjection;
using WpfCustomControlLibrary.ComboBoxes;

namespace WpfDnDCustomControlLibrary.Fight
{
    /// <summary>
    ///     Only selects one in a combobox
    /// </summary>
    public class FightersSelectorComboboxControl : ComboBoxControl
    {
        private static readonly Lazy<IFightersProvider> _lazyFightManager = new(DIContainer.GetImplementation<IFightersProvider>);
        private static readonly IFightersProvider _fightersProvider = _lazyFightManager.Value;

        public FightersSelectorComboboxControl() : base()
        {
            Initialized += FightersSelectorControl_Initialized;
            this.DisplayMemberPath = "DisplayName";
        }

        private void FightersSelectorControl_Initialized(object sender, EventArgs e)
        {
            this.ItemsSource = _fightersProvider.Fighters;
            this.SelectionChanged += FightersSelectorControl_SelectionChanged;
        }

        private void FightersSelectorControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SelectedIndex != -1)
                Target = (PlayableEntity)SelectedItem;
            else
                Target = null;
        }

        public PlayableEntity Target
        {
            get { return (PlayableEntity)this.GetValue(TargetProperty); }
            set { this.SetValue(TargetProperty, value); }
        }
        private static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            nameof(Target),
            typeof(PlayableEntity),
            typeof(FightersSelectorComboboxControl),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    }
}
