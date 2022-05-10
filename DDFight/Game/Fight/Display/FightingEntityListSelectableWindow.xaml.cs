using System;
using DDFight.Windows;
using DnDToolsLibrary.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using BaseToolsLibrary.DependencyInjection;
using DnDToolsLibrary.Fight;
using WpfToolsLibrary.Extensions;

namespace DDFight.Game.Fight.Display
{
    /// <summary>
    /// Logique d'interaction pour FightingEntityListSelectableUserWindow.xaml
    /// </summary>
    public partial class FightingEntityListSelectableWindow : Window, INotifyPropertyChanged
    {
        private static readonly Lazy<IFightersProvider> _lazyFightManager = new(DIContainer.GetImplementation<IFightersProvider>());
        protected static IFightersProvider FightersProvider => _lazyFightManager.Value;

        #region Properties

        /// <summary>
        ///     will be set to true if an entity can be selected more than 1 time
        /// </summary>
        public bool CanSelectSameTargetTwice 
        {
            get => _canSelectSameTargetTwice;
            set
            {
                if (_canSelectSameTargetTwice != value)
                {
                    _canSelectSameTargetTwice = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _canSelectSameTargetTwice = false;

        /// <summary>
        ///     The amount of selected max, 0 for no limit
        /// </summary>
        public int MaximumSelected
        {
            get => _maximumSelected;
            set
            {
                if (_maximumSelected != value)
                {
                    _maximumSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _maximumSelected = 0;

        /// <summary>
        ///     Tells wether or not the choice has been validated
        /// </summary>
        public bool Validated = false;

        #endregion Properties

        public FightingEntityListSelectableWindow()
        {
            InitializeComponent();
            Loaded += FightingEntityListSelectableWindow_Loaded;
        }

        #region Filter
        private void FilterTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            SelectFromListControl.FilterINameableListBox(FilterControl.Text);
        }

        #endregion Filter
        private void FightingEntityListSelectableWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SelectFromListControl.ItemsSource = FightersProvider.GetObservableCollection();
            Selected = new ObservableCollection<PlayableEntity>();
            SelectedListControl.ItemsSource = Selected;
            Selected.CollectionChanged += Selected_CollectionChanged;
        }

        private void Selected_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ValidateButton.IsEnabled = true;
            if (Selected.Count == 0)
                ValidateButton.IsEnabled = false;
        }

        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        /// <summary>
        ///     returns false if it has already been selected and this.CanSelectSameTargetTwice is set to true
        ///     returns true otherwise
        /// </summary>
        /// <returns></returns>
        private bool canStillBeSelected()
        {
            if (CanSelectSameTargetTwice)
                return true;
            PlayableEntity tried = (PlayableEntity)SelectFromListControl.SelectedItem;
            foreach (PlayableEntity tmp in SelectedListControl.Items)
            {
                if (tried == tmp)
                    return false;
            }
            return true;
        }

        #region Selection

        /// <summary>
        ///     returns false if the amount of selected characters equals the MaximumSelected (unless MaximumSelected is set to 0 => no limit)
        /// </summary>
        /// <returns></returns>
        private bool canStillSelectMore()
        {
            if (MaximumSelected == 0)
                return true;
            if (SelectedListControl.Items.Count < MaximumSelected)
                return true;
            return false;
        }

        public ObservableCollection<PlayableEntity> Selected;

        private void SelectFromListControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right || e.Key == Key.Enter)
            {
                if (canStillBeSelected() && canStillSelectMore())
                {
                    Selected.Add((PlayableEntity)SelectFromListControl.SelectedItem);
                }

            }
        }

        private void SelectedListControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Delete)
            {
                Selected.RemoveAt(SelectedListControl.SelectedIndex);
            }
        }

        #endregion Selection


        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            Validated = true;
            this.Close();
        }

        private void FightingEntityListSelectableWindowControl_Closing(object sender, CancelEventArgs e)
        {
            if (Validated == false && Selected.Count != 0)
            {
                AskYesNoDataContext ctx = new AskYesNoDataContext
                {
                    Message = "Do you wish to validate your selection?"
                };
                AskYesNoWindow window = new AskYesNoWindow { DataContext = ctx };
                window.ShowCentered();

                Validated = ctx.Yes;
            }
        }
    }
}
