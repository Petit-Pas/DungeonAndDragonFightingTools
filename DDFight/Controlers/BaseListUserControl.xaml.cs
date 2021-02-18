using DDFight.Tools;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Controlers
{
    /// <summary>
    ///     A Base class to display a list of element by their name with easy access to basic list management (add, delete, update, duplicate)
    ///     /!\ classes to display should inherit the IListable Property
    /// </summary>
    public abstract partial class BaseListUserControl : UserControl, INotifyPropertyChanged
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

        public BaseListUserControl()
        {
            InitializeComponent();
            ContextMenu_Populate();
        }

        /// <summary>
        ///     Will enable/disable functionalities such as edit/duplicate/remove
        /// </summary>
        public bool IsEditable
        {
            get { return (bool)this.GetValue(IsEditableProperty); }
            set { this.SetValue(IsEditableProperty, value); }
        }
        private static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register(
            nameof(IsEditable), typeof(bool), typeof(BaseListUserControl),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(IsEditableChanged)));

        private static void IsEditableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BaseListUserControl control = d as BaseListUserControl;
            control.ContextMenu_Populate();
        }

        /// <summary>
        ///     if set to false, disables the ContextMenu
        /// </summary>
        public bool ContextMenuCanOpen
        {
            get { return (bool)this.GetValue(ContextMenuCanOpenProperty); }
            set { this.SetValue(ContextMenuCanOpenProperty, value); }
        }
        private static readonly DependencyProperty ContextMenuCanOpenProperty = DependencyProperty.Register(
            nameof(ContextMenuCanOpen), typeof(bool), typeof(BaseListUserControl),
            new FrameworkPropertyMetadata(true));

        /// <summary>
        ///     Binded to the Visibility of the Grid containing the add and the remove button
        /// </summary>
        public Visibility ButtonsVisibility
        {
            get { return (Visibility)this.GetValue(ButtonsVisbilityProperty); }
            set { this.SetValue(ButtonsVisbilityProperty, value); }
        }
        private static readonly DependencyProperty ButtonsVisbilityProperty = DependencyProperty.Register(
            nameof(ButtonsVisibility), typeof(Visibility), typeof(BaseListUserControl),
            new FrameworkPropertyMetadata(Visibility.Visible));

        /// <summary>
        ///     The list of entities to display, it should uinherit the IListable interface
        /// </summary>
        public object EntityList
        {
            get { return this.GetValue(EntityListPoprety); }
            set { this.SetValue(EntityListPoprety, value); }
        }
        private static readonly DependencyProperty EntityListPoprety = DependencyProperty.Register(
            nameof(EntityList), typeof(object), typeof(BaseListUserControl),
            new FrameworkPropertyMetadata(null));

        #region ContextMenu

        public ContextMenu ListContextMenu
        {
            get {
                try
                {
                    return _listContextMenu?.Items.Count != 0 ? _listContextMenu : null;
                }
                catch (Exception) { return null; }
                }
            set
            {
                if (value.Items.Count == 0)
                    _listContextMenu = null;
                else 
                    _listContextMenu = value;
                NotifyPropertyChanged();
            }
        }
        private ContextMenu _listContextMenu = new ContextMenu();

        protected virtual void ContextMenu_Populate()
        {
            ContextMenu menu = new ContextMenu();

            if (IsEditable)
            {
                MenuItem edit = new MenuItem { Header = "Edit" };
                edit.Click += ContextMenu_EditClick;
                menu.Items.Add(edit);
            }
            if (IsEditable)
            {
                MenuItem remove = new MenuItem { Header = "Remove" };
                remove.Click += ContextMenu_RemoveClick;
                menu.Items.Add(remove);
            }
            if (IsEditable)
            {
                MenuItem duplicate = new MenuItem { Header = "Duplicate" };
                duplicate.Click += ContextMenu_Duplicate_Click;
                menu.Items.Add(duplicate);
            }
            
            this.ListContextMenu = menu;
        }

        protected void ContextMenu_Duplicate_Click(object sender, RoutedEventArgs e)
        {
            if (EntityListControl.SelectedIndex != -1 && IsEditable)
                duplicate(EntityListControl.SelectedItem as IListable);
        }

        protected void ContextMenu_RemoveClick(object sender, RoutedEventArgs e)
        {
            if (EntityListControl.SelectedIndex != -1 && IsEditable)
                remove(EntityListControl.SelectedItem);
        }

        protected void ContextMenu_EditClick(object sender, RoutedEventArgs e)
        {
            if (EntityListControl.SelectedIndex != -1 && IsEditable)
                edit(EntityListControl.SelectedItem as IListable);
        }

        #endregion ContextMenu

        private void FilterTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            EntityListControl.FilterINameableListBox(FilterControl.TextBoxControl.Text);
        }

        #region ClickEvents

        protected virtual void AddButtonControl_Click(object sender, RoutedEventArgs e)
        {
            if (IsEditable)
                add_new();
        }

        protected virtual void RemoveButtonControl_Click(object sender, RoutedEventArgs e)
        {
            if (EntityListControl.SelectedItem != null && IsEditable)
                remove(EntityListControl.SelectedItem);
        }

        protected virtual void EntityList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EntityListControl.SelectedItem != null && IsEditable)
                edit(EntityListControl.SelectedItem as IListable);
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
                    if (IsEditable)
                    {
                        remove(EntityListControl.SelectedItem);
                        e.Handled = true;
                    }
                }
        }

        private void EntityList_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            e.Handled = !ContextMenuCanOpen;
        }
    }
}
