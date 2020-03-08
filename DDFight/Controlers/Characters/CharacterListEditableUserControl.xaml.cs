using DDFight.Game;
using DDFight.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Controlers
{
    /// <summary>
    /// Interaction logic for CharacterListUserControl.xaml
    /// </summary>
    public partial class CharacterListEditableUserControl : UserControl
    {
        /// <summary>
        ///     Getter for a casted DataContext
        /// </summary>
        private GameDataContext data_context
        {
            get
            {
                return (GameDataContext)DataContext;
            }
        }

        /// <summary>
        ///     Ctor
        /// </summary>
        public CharacterListEditableUserControl()
        {
            Loaded += CharacterListUserControl_Loaded;
            InitializeComponent();
            FilterTextBox.GotFocus += FilterTextBox_GotFocus;
            FilterTextBox.LostFocus += FilterTextBox_LostFocus;
            FilterTextBox.Text = filterPlaceHolder;
        }

        private string filterPlaceHolder = "Filter...";

        private void FilterTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilterTextBox.Text))
                FilterTextBox.Text = filterPlaceHolder;
        }

        private void FilterTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FilterTextBox.Text == filterPlaceHolder)
                FilterTextBox.Text = "";
        }

        /// <summary>
        ///     Initializer for when the DataContext is available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterListUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CharacterList.ItemsSource = data_context.CharacterList.Characters;
        }

        #region Add

        /// <summary>
        ///     add a new character
        /// </summary>
        private void add_character(Character character)
        {
            data_context.CharacterList.AddCharacter(character);
        }

        /// <summary>
        ///     handler for the Add character button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCharacter_Button_Click(object sender, RoutedEventArgs e)
        {
            Character character = new Character();

            if (character.OpenEditWindow())
                add_character(character);
        }

        private void DuplicateCharacter_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Character character = (Character)CharacterList.SelectedItem;
            Character new_character = (Character)character.Clone();

            new_character.Name = new_character.Name + " - Copie";

            add_character(new_character);
        }

        #endregion

        #region Delete

        private void delete_character(Character character)
        {
            data_context.CharacterList.RemoveCharacter(character);
        }

        /// <summary>
        ///     handler for the Remove character button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveCharacter_Button_Click(object sender, RoutedEventArgs e)
        {
            delete_character((Character)CharacterList.SelectedItem);
        }

        private void DeleteCharacter_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            delete_character((Character)CharacterList.SelectedItem);
        }

        #endregion

        #region Edit

        private void update_character(Character to_update)
        {
            to_update.OpenEditWindow();
            Global.Context.CharacterList.Save();
        }

        /// <summary>
        ///     Handler for the double click on a character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CharacterList.SelectedItem != null)
            {
                update_character((Character)CharacterList.SelectedItem);
            }
        }

        /// <summary>
        ///     Hendler for Edit selection on menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditCharacter_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            update_character((Character)CharacterList.SelectedItem);
        }

        #endregion

        #region AddToFight

        private void AddToFight_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (CharacterList.SelectedItem != null)
            {
                ((Character)CharacterList.SelectedItem).DisplayName = ((Character)CharacterList.SelectedItem).Name;
                data_context.FightContext.FightersList.AddCharacter((Character)CharacterList.SelectedItem);
            }
        }
        #endregion

        private void CharacterList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                this.DeleteCharacter_MenuItem_Click(sender, null);
            if (e.Key == Key.Right)
                AddToFight_MenuItem_Click(sender, null);
        }

        private void FilterTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            CharacterList.FilterPlayableEntityListBox(FilterTextBox.Text);
        }
    }
}
