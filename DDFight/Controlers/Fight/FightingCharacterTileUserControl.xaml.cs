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

namespace DDFight.Controlers.Fight
{
    /// <summary>
    /// Logique d'interaction pour FightingCharacterTileDataContext.xaml
    /// </summary>
    public partial class FightingCharacterTileUserControl : UserControl
    {
        public PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }

        public FightingCharacterTileUserControl()
        {
            InitializeComponent();
            Loaded += FightingCharacterTileUserControl_Loaded;
        }

        private void FightingCharacterTileUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            data_context.NewTurnStarted += Data_context_NewTurnStarted;
            data_context.TurnEnded += Data_context_TurnEnded;
        }

        private void Data_context_TurnEnded(object sender, DDFight.Game.Fight.FightEvents.EndTurnEventArgs args)
        {
            CharacterTileGroupBoxControl.Background = (Brush)Application.Current.Resources["Gray"];
        }

        private void Data_context_NewTurnStarted(object sender, DDFight.Game.Fight.FightEvents.StartNewTurnEventArgs args)
        {
            CharacterTileGroupBoxControl.Background = (Brush)Application.Current.Resources["Indigo"];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("in button click");
        }

        private void MainControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("in mouse up event");
        }
    }
}
