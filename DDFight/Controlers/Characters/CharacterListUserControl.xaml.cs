﻿using DDFight.Game;
using DDFight.Tools.Save;
using DDFight.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Controlers
{
    /// <summary>
    /// Interaction logic for CharacterListUserControl.xaml
    /// </summary>
    public partial class CharacterListUserControl : UserControl
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
        public CharacterListUserControl()
        {
            Loaded += CharacterListUserControl_Loaded;

            InitializeComponent();
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
        private void add_character(CharacterDataContext character)
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
            CharacterDataContext character = new CharacterDataContext();

            add_character(character);
        }

        private void DuplicateCharacter_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            CharacterDataContext character = (CharacterDataContext)CharacterList.SelectedItem;
            CharacterDataContext new_character = (CharacterDataContext)character.Clone();

            new_character.Name = new_character.Name + " - Copie";

            add_character(new_character);
        }

        #endregion

        #region Delete

        private void delete_character(CharacterDataContext character)
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
            delete_character((CharacterDataContext)CharacterList.SelectedItem);
        }

        private void DeleteCharacter_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            delete_character((CharacterDataContext)CharacterList.SelectedItem);
        }

        #endregion

        #region Edit

        private void update_character(CharacterDataContext to_update)
        {
            EditCharacterWindow window = new EditCharacterWindow
            {
                Owner = Window.GetWindow(this),
            };
            CharacterDataContext temporary = (CharacterDataContext)to_update.Clone();
            window.DataContext = temporary;

            window.ShowDialog();

            if (temporary.Validated == true)
            {
                data_context.CharacterList.Replace(to_update, temporary);
                data_context.CharacterList.Save();
            }
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
                update_character((CharacterDataContext)CharacterList.SelectedItem);
            }
        }

        /// <summary>
        ///     Hendler for Edit selection on menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditCharacter_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            update_character((CharacterDataContext)CharacterList.SelectedItem);
        }

        #endregion

        #region AddToFight

        private void AddToFight_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<PlayableEntity> list = data_context.FightingCharacters;

            if (list.Count != 0)
            {
                if (list.Single(x => x.Name == ((CharacterDataContext)CharacterList.SelectedItem).Name) == null)
                    data_context.FightingCharacters.Add((CharacterDataContext)CharacterList.SelectedItem);
            }
            else
            {
                data_context.FightingCharacters.Add((CharacterDataContext)CharacterList.SelectedItem);
            }

        }

        #endregion



    }
}
