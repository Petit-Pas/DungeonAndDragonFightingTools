using DDFight.Tools;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Controlers
{
    /// <summary>
    ///     A Base class to display a list of element by their name with easy access to basic list management (add, delete, update, duplicate)
    ///     /!\ A member "DisplayName" must be existing for the ListBox to display the element efficiently
    /// </summary>
    public partial class BaseListEditableUserControl : UserControl, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public BaseListEditableUserControl()
        {
            InitializeComponent();
            ContextMenu_Populate();
        }

        public bool ContextMenuCanOpen
        {
            get { return (bool)this.GetValue(ContextMenuCanOpenProperty); }
            set { this.SetValue(ContextMenuCanOpenProperty, value); }
        }
        private static readonly DependencyProperty ContextMenuCanOpenProperty = DependencyProperty.Register(
            nameof(ContextMenuCanOpen), typeof(bool), typeof(BaseListEditableUserControl),
            new FrameworkPropertyMetadata(true));

        public Visibility ButtonsVisibility
        {
            get { return (Visibility)this.GetValue(ButtonsVisbilityProperty); }
            set { this.SetValue(ButtonsVisbilityProperty, value); }
        }
        private static readonly DependencyProperty ButtonsVisbilityProperty = DependencyProperty.Register(
            nameof(ButtonsVisibility), typeof(Visibility), typeof(BaseListEditableUserControl),
            new FrameworkPropertyMetadata(Visibility.Visible));

        public object EntityList
        {
            get { return this.GetValue(EntityListPoprety); }
            set { this.SetValue(EntityListPoprety, value); }
        }
        private static readonly DependencyProperty EntityListPoprety = DependencyProperty.Register(
            nameof(EntityList), typeof(object), typeof(BaseListEditableUserControl),
            new FrameworkPropertyMetadata(null));

        #region ContextMenu

        public ContextMenu ListContextMenu
        {
            get {
                try
                {
                    return _listContextMenu.Items.Count != 0 ? _listContextMenu : null;
                }
                catch (Exception) { return null; }
                }
            set
            {
                _listContextMenu = value;
                NotifyPropertyChanged();
            }
        }
        private ContextMenu _listContextMenu = new ContextMenu();

        protected virtual void ContextMenu_Populate()
        {
            ContextMenu menu = new ContextMenu();

            MenuItem edit = new MenuItem { Header = "Edit" };
            edit.Click += ContextMenu_EditClick;
            menu.Items.Add(edit);

            MenuItem remove = new MenuItem { Header = "Remove" };
            remove.Click += ContextMenu_RemoveClick;
            menu.Items.Add(remove);

            MenuItem duplicate = new MenuItem { Header = "Duplicate" };
            duplicate.Click += ContextMenu_Duplicate_Click;
            menu.Items.Add(duplicate);

            this.ListContextMenu = menu;
        }

        protected void ContextMenu_Duplicate_Click(object sender, RoutedEventArgs e)
        {
            if (EntityListControl.SelectedIndex != -1)
                duplicate(EntityListControl.SelectedItem);
        }

        protected void ContextMenu_RemoveClick(object sender, RoutedEventArgs e)
        {
            if (EntityListControl.SelectedIndex != -1)
                remove(EntityListControl.SelectedItem);
        }

        protected void ContextMenu_EditClick(object sender, RoutedEventArgs e)
        {
            if (EntityListControl.SelectedIndex != -1)
                edit(EntityListControl.SelectedItem);
        }

        #endregion ContextMenu

        #region ListChanged
        public delegate void ListChangedEventHandler();
        public ListChangedEventHandler ListChanged;

        public void OnListEdited()
        {
            this.ListChanged?.Invoke();
        }

        #endregion ListChanged

        private void FilterTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            EntityListControl.FilterINameableListBox(FilterControl.TextBoxControl.Text);
        }

        #region ClickEvents

        protected virtual void AddButtonControl_Click(object sender, RoutedEventArgs e)
        {
            add_new();
        }

        protected virtual void RemoveButtonControl_Click(object sender, RoutedEventArgs e)
        {
            if (EntityListControl.SelectedIndex != -1)
                remove(EntityListControl.SelectedItem);
        }

        protected virtual void EntityList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EntityListControl.SelectedItem != null)
                edit(EntityListControl.SelectedItem);
        }

        #endregion ClickEvents

        #region ListControl

        public virtual void edit(object obj)
        {
            Logger.Log("WARN: UserControls should override the edit() method as the base one in PlayableEntityListEditableUserControl is empty");
        }

        public virtual void remove(object obj)
        {
            Logger.Log("WARN: UserControls should override the remove() method as the base one in PlayableEntityListEditableUserControl is empty");
        }

        public virtual void duplicate(object obj)
        {
            Logger.Log("WARN: UserControls should override the duplicate() method as the base one in PlayableEntityListEditableUserControl is empty");
        }

        public virtual void add_new(object obj = null)
        {
            Logger.Log("WARN: UserControls should override the add_new() method as the base one in PlayableEntityListEditableUserControl is empty");
        }

        #endregion ListControl

        protected virtual void EntityListControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                if (EntityListControl.SelectedIndex != -1)
                {
                    remove(EntityListControl.SelectedItem);
                    e.Handled = true;
                }
        }

        private void EntityList_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            e.Handled = !ContextMenuCanOpen;
        }
    }
}
