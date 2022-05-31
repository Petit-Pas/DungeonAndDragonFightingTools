using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System;
using System.Windows;
using BaseToolsLibrary.DependencyInjection;
using WpfCustomControlLibrary.ComboBoxes;
using System.Windows.Data;
using WpfToolsLibrary.ValidationRules;

namespace WpfDnDCustomControlLibrary.Fight
{
    /// <summary>
    ///     Only selects one in a combobox
    /// </summary>
    public class FightersSelectorComboboxControl : ComboBoxControl, IValidable
    {
        private static readonly Lazy<IFightersProvider> _lazyFightManager = new(DIContainer.GetImplementation<IFightersProvider>);
        private static readonly IFightersProvider _fightersProvider = _lazyFightManager.Value;

        public FightersSelectorComboboxControl() : base()
        {
            Initialized += FightersSelectorControl_Initialized;
            this.DisplayMemberPath = "DisplayName";

            var binding = new Binding("Target");
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding.Source = this;
            binding.ValidationRules.Clear();
            //binding.ValidationRules.Add(this.GetValidationRule());
            binding.NotifyOnValidationError = true;
            binding.NotifyOnSourceUpdated = true;
            binding.NotifyOnTargetUpdated = true;
            this.SetBinding(SelectedItemProperty, binding).UpdateSource();

        }

        private void FightersSelectorControl_Initialized(object sender, EventArgs e)
        {
            this.ItemsSource = _fightersProvider.Fighters;
        }

        public bool IsValid()
        {
            return Target != null;
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
