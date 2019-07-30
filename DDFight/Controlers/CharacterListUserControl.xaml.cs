using DDFight.Game;
using DDFight.Tools.Save;
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
            if (data_context.CharacterList != null)
            {
                foreach (CharacterDataContext character in data_context.CharacterList?.Characters)
                {
                    this.CharacterList.Items.Add(character);
                }
            }
        }

        /// <summary>
        ///     handler for the Add character button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCharacter(object sender, RoutedEventArgs e)
        {
            NewCharacterWindow window = new NewCharacterWindow();
            CharacterDataContext character = new CharacterDataContext();
            window.DataContext = character;
            window.ShowDialog();

            this.CharacterList.Items.Add(character);
            data_context.CharacterList.AddCharacter(character);
        }

        /// <summary>
        ///     handler for the Remove character button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveCharacter_Click(object sender, RoutedEventArgs e)
        {
            data_context.CharacterList.RemoveCharacter((CharacterDataContext)CharacterList.SelectedItem);
            CharacterList.Items.Remove(CharacterList.SelectedItem);
        }

        /// <summary>
        ///     Handler for the double click on a character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NewCharacterWindow character = new NewCharacterWindow();
            CharacterDataContext context = (CharacterDataContext)CharacterList.SelectedItem;
            character.DataContext = context;

            character.ShowDialog();

            CharacterList.SelectedItem = context;
            data_context.CharacterList.Save();
        }
    }
}
