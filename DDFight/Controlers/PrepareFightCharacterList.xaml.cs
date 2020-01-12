using DDFight.Game;
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
    /// Logique d'interaction pour PrepareFightCharacterList.xaml
    /// </summary>
    public partial class PrepareFightCharacterList : UserControl
    {
        private GameDataContext data_context {
            get => (GameDataContext)DataContext;
        }

        public PrepareFightCharacterList()
        {
            InitializeComponent();

            Loaded += PrepareFightCharacterList_Loaded;

        }

        private void PrepareFightCharacterList_Loaded(object sender, RoutedEventArgs e)
        {
            CharactersList.ItemsSource = data_context.FightingCharacters;
        }
    }
}
