using DDFight.Game;
using DDFight.Tools.Save;
using DDFight.Windows;
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
        private GameDataContext data_context;

        public CharacterListUserControl()
        {
            InitializeComponent();
            data_context = (GameDataContext)this.DataContext;

            foreach (CharacterDataContext character in data_context.CharacterList?.Characters)
            {
                this.CharacterList.Items.Add(character);
            }
        }

        private void AddCharacter(object sender, RoutedEventArgs e)
        {
            NewCharacterWindow window = new NewCharacterWindow();
            CharacterDataContext context = new CharacterDataContext();
            window.DataContext = context;
            window.ShowDialog();

            this.CharacterList.Items.Add(context);
            data_context.CharacterList.AddCharacter(context);
        }

        private void RemoveCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterList.Items.Remove(CharacterList.SelectedItem);
        }

        private void CharacterList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NewCharacterWindow character = new NewCharacterWindow();
            CharacterDataContext context = (CharacterDataContext)CharacterList.SelectedItem;
            character.DataContext = context;

            character.ShowDialog();

            CharacterList.SelectedItem = context;
        }
    }
}
