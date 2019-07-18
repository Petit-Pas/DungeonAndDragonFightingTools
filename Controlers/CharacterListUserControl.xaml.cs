using DDFight.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
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
            character.ShowDialog();

            context.FromRawToReal();

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
            context.FromRealToRaw();
            character.DataContext = context;

            character.ShowDialog();

            context.FromRawToReal();
            CharacterList.SelectedItem = context;
        }
    }
}
