using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfCustomControlLibrary.ComboBoxes;

namespace WpfDnDCustomControlLibrary.Fight
{
    /// <summary>
    ///     Only selects one in a combobox
    /// </summary>
    public class FightersSelectorComboboxControl : ComboBoxControl
    {
        public FightersSelectorComboboxControl() : base()
        {
            Initialized += FightersSelectorControl_Initialized;
            this.DisplayMemberPath = "DisplayName";
        }

        private void FightersSelectorControl_Initialized(object sender, EventArgs e)
        {
            this.ItemsSource = FightersList.Instance.Elements;
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
