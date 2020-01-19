using DDFight.Game;
using DDFight.Game.Fight.FightEvents;
using DDFight.Tools;
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
    public partial class FightingCharacterTileUserControl : UserControl, IEventUnregisterable
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
            Global.Context.FightContext.CharacterSelected += FightContext_CharacterSelected;
        }

        private void FightContext_CharacterSelected(object sender, SelectedCharacterEventArgs args)
        {
            if (args.Character == data_context)
            {
                CharacterTileGroupBoxControl.BorderThickness = new Thickness(2);
            }
            else
            {
                CharacterTileGroupBoxControl.BorderThickness = new Thickness(0);
            }
        }

        private void Data_context_TurnEnded(object sender, DDFight.Game.Fight.FightEvents.EndTurnEventArgs args)
        {
            CharacterTileGroupBoxControl.Background = (Brush)Application.Current.Resources["Gray"];
        }

        private void Data_context_NewTurnStarted(object sender, DDFight.Game.Fight.FightEvents.StartNewTurnEventArgs args)
        {
            CharacterTileGroupBoxControl.Background = (Brush)Application.Current.Resources["Indigo"];
        }

        private void MainControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Global.Context.FightContext.OnSelectedCharacter(new SelectedCharacterEventArgs()
            {
                Character = data_context,
            });
        }

        public void UnregisterToAll()
        {
            data_context.NewTurnStarted -= Data_context_NewTurnStarted;
            data_context.TurnEnded -= Data_context_TurnEnded;
            Global.Context.FightContext.CharacterSelected -= FightContext_CharacterSelected;
        }
    }
}
