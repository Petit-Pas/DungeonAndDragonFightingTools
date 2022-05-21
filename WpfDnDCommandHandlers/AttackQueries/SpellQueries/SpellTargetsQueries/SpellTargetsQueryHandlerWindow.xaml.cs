using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Extensions;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellTargetsQueries;
using DnDToolsLibrary.Fight;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WpfCustomControlLibrary.ModalWindows;
using WpfToolsLibrary.Extensions;

namespace WpfDnDCommandHandlers.AttackQueries.SpellQueries.SpellTargetsQueries
{
    /// <summary>
    /// Logique d'interaction pour GetInputSpellTargetsWindow.xaml
    /// </summary>
    public partial class SpellTargetsQueryHandlerWindow : Window, IResultWindow<SpellTargetQuery, SpellTargets>
    {

        public ObservableCollection<string> Selected
        {
            get { return (ObservableCollection<string>)this.GetValue(SelectedProperty); }
            set { this.SetValue(SelectedProperty, value); }
        }
        private static readonly DependencyProperty SelectedProperty = DependencyProperty.Register(
            nameof(Selected),
            typeof(ObservableCollection<string>),
            typeof(SpellTargetsQueryHandlerWindow),
            new PropertyMetadata(null));

        public ObservableCollection<string> Fighters
        {
            get { return (ObservableCollection<string>)this.GetValue(FightersProperty); }
            set { this.SetValue(FightersProperty, value); }
        }
        private static readonly DependencyProperty FightersProperty = DependencyProperty.Register(
            nameof(Fighters),
            typeof(ObservableCollection<string>),
            typeof(SpellTargetsQueryHandlerWindow),
            new PropertyMetadata(null));

        public bool Validated { get; set; } = false;

        private SpellTargetQuery data_context
        {
            get => DataContext as SpellTargetQuery;
        }

        public SpellTargetsQueryHandlerWindow()
        {
            Initialized += FightersListSelectorWindow_Initialized;
            Loaded += FightersListSelectorWindow_Loaded;
            InitializeComponent();
        }

        private void FightersListSelectorWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Selected = new ObservableCollection<string>();
            Fighters = DIContainer.GetImplementation<IFightersProvider>().GetFightersNames().ToObservableCollection();
        }

        private void FightersListSelectorWindow_Initialized(object sender, EventArgs e)
        {
            ((CollectionViewSource)this.Resources["Selected"]).Filter += FilterSelected;
            ((CollectionViewSource)this.Resources["Fighters"]).Filter += FilterFighters;
        }

        private void FilterSelected(object sender, FilterEventArgs e)
        {
            string name = e.Item as string;
            if (name != null)
            {
                if (SelectedFilterControl.IsEmpty || name.ToLower().Contains(SelectedFilterControl.Text.ToLower()))
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
            else
                e.Accepted = true;
        }

        private void FilterFighters(object sender, FilterEventArgs e)
        {
            string name = e.Item as string;
            if (name != null)
            {
                if (FightersFilterControl.IsEmpty || name.ToLower().Contains(FightersFilterControl.Text.ToLower()))
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
            else
                e.Accepted = true;
        }

        public SpellTargets GetResult()
        {
            return new SpellTargets(Selected.ToList());
        }

        public void LoadContext(SpellTargetQuery context)
        {
            DataContext = context;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Validated == false)
            {
                YesNoWindow win = new YesNoWindow() { Text = "Are you sure you wish to cancel this ?", Validated = false };
                win.ShowCentered();

                if (!win.Validated)
                {
                    e.Cancel = true;
                }
            }
        }

        private void add_selected()
        {
            if (FightersList.SelectedIndex < 0 || FightersList.SelectedItem == null)
                return;

            string new_name = FightersList.SelectedItem as string;

            // already enough selected
            if (data_context.AmountTargets > 0 && Selected.Count >= data_context.AmountTargets)
                return;

            // you can't select it twice if not set in options
            if (false == data_context.TargetCanBeSelectedMoreThanOnce && Selected.Contains(new_name))
                return;

            ValidateButton.IsEnabled = true;
            Selected.Add(new_name);
        }

        private void remove_selected()
        {
            if (SelectedList.SelectedIndex < 0 || SelectedList.SelectedItem == null)
                return;
            
            Selected.RemoveAt(SelectedList.SelectedIndex);
            if (Selected.Count == 0)
                ValidateButton.IsEnabled = false;
        }

        private void FightersList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
                add_selected();
        }

        private void SelectedList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Left)
                remove_selected();
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            this.Validated = true;
            this.Close();
        }

        private void FightersList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            add_selected();
        }
    }
}
