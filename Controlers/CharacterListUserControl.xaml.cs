using DDFight.Tools;
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
        public CharacterListUserControl()
        {
            InitializeComponent();
        }

        private void AddCharacter(object sender, RoutedEventArgs e)
        {
            NewCharacterWindow character = new NewCharacterWindow();
            NewCharacterDataContext context = new NewCharacterDataContext();
            character.DataContext = context;
            context.Initiative = 15;
            character.ShowDialog();

            System.Console.WriteLine("Initiative is now {0}", context.Initiative);
            this.CharacterList.Items.Add(context);
        }

        private void RemoveCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterList.Items.Remove(CharacterList.SelectedItem);
        }

        private void CharacterList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NewCharacterWindow character = new NewCharacterWindow();
            NewCharacterDataContext context = (NewCharacterDataContext)CharacterList.SelectedItem;
            character.DataContext = context;

            character.ShowDialog();

            CharacterList.SelectedItem = context;
        }
    }
}
