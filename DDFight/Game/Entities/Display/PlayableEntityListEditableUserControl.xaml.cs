using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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

namespace DDFight.Game.Entities.Display
{
    /// <summary>
    /// Interaction logic for PlayableEntityListEditableUserControl.xaml
    /// </summary>
    public partial class PlayableEntityListEditableUserControl : UserControl, INotifyPropertyChanged
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

        public PlayableEntityListEditableUserControl()
        {
            InitializeComponent();
            Loaded += PlayableEntityListEditableUserControl_Loaded;
        }

        private void PlayableEntityListEditableUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ContextMenu_Populate();
        }

        public bool ContextMenuCanOpen
        {
            get { return (bool)this.GetValue(ContextMenuCanOpenProperty); }
            set { this.SetValue(ContextMenuCanOpenProperty, value); }
        }
        private static readonly DependencyProperty ContextMenuCanOpenProperty = DependencyProperty.Register(
            nameof(ContextMenuCanOpen), typeof(bool), typeof(PlayableEntityListEditableUserControl),
            new FrameworkPropertyMetadata(true));

        public Visibility ButtonsVisibility
        {
            get { return (Visibility)this.GetValue(ButtonsVisbilityProperty); }
            set { this.SetValue(ButtonsVisbilityProperty, value); }
        }
        private static readonly DependencyProperty ButtonsVisbilityProperty = DependencyProperty.Register(
            nameof(ButtonsVisibility), typeof(Visibility), typeof(PlayableEntityListEditableUserControl),
            new FrameworkPropertyMetadata(Visibility.Visible));

        public object EntityList
        {
            get { return this.GetValue(EntityListPoprety); }
            set { this.SetValue(EntityListPoprety, value); }
        }
        private static readonly DependencyProperty EntityListPoprety = DependencyProperty.Register(
            nameof(EntityList), typeof(object), typeof(PlayableEntityListEditableUserControl),
            new FrameworkPropertyMetadata(null));

        #region ContextMenu

        public ContextMenu ListContextMenu
        {
            get => _listContextMenu.Items.Count != 0 ? _listContextMenu : null;
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
                duplicate(EntityListControl.SelectedItem as PlayableEntity);
        }

        protected void ContextMenu_RemoveClick(object sender, RoutedEventArgs e)
        {
            if (EntityListControl.SelectedIndex != -1)
                remove(EntityListControl.SelectedItem as PlayableEntity);
        }

        protected void ContextMenu_EditClick(object sender, RoutedEventArgs e)
        {
            if (EntityListControl.SelectedIndex != -1)
                edit(EntityListControl.SelectedItem as PlayableEntity);
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
                remove((PlayableEntity)EntityListControl.SelectedItem);
        }

        protected virtual void EntityList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EntityListControl.SelectedItem != null)
            {
                PlayableEntity entity = EntityListControl.SelectedItem as PlayableEntity;
                edit(entity);
            }
        }

        #endregion ClickEvents

        #region ListControl

        protected virtual void edit(PlayableEntity entity)
        {
            Logger.Log("WARN: UserControls should override the edit() method as the base one in PlayableEntityListEditableUserControl is empty");
        }

        protected virtual void remove(PlayableEntity entity)
        {
            Logger.Log("WARN: UserControls should override the remove() method as the base one in PlayableEntityListEditableUserControl is empty");
        }

        protected virtual void duplicate(PlayableEntity entity)
        {
            Logger.Log("WARN: UserControls should override the duplicate() method as the base one in PlayableEntityListEditableUserControl is empty");
        }

        protected virtual void add_new(PlayableEntity entity = null)
        {
            Logger.Log("WARN: UserControls should override the add_new() method as the base one in PlayableEntityListEditableUserControl is empty");
        }

        #endregion ListControl

        protected virtual void EntityListControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                if (EntityListControl.SelectedIndex != -1)
                {
                    remove(EntityListControl.SelectedItem as PlayableEntity);
                    e.Handled = true;
                }
        }

        private void EntityList_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            e.Handled = !ContextMenuCanOpen;
        }
    }
}
