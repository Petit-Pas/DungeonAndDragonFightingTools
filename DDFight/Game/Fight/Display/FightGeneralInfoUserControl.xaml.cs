using DDFight.Tools;
using DDFight.Windows;
using DDFight.Windows.FightWindows;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight.Events;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfToolsLibrary.Extensions;

namespace DDFight.Controlers.Fight
{
    /// <summary>
    /// Logique d'interaction pour GeneralInfoFightUserControl.xaml
    /// </summary>
    public partial class FightGeneralInfoUserControl : UserControl, IEventUnregisterable
    {
        public FightGeneralInfoUserControl()
        {
            InitializeComponent();
            Loaded += GeneralInfoFightUserControl_Loaded;
        }

        private void setCharactersTurnName()
        {
            CharacterTurnTextboxCountrol.Text = "Turn of: " + GlobalContext.Context.FightContext.FightersList.ElementAt((int)GlobalContext.Context.FightContext.TurnIndex).DisplayName;
        }

        private void GeneralInfoFightUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalContext.Context.FightContext.NewTurnStarted += FightContext_NewTurnStarted;
            setCharactersTurnName();
        }

        private void FightContext_NewTurnStarted(object sender, StartNewTurnEventArgs args)
        {
            CharacterTurnTextboxCountrol.Text = "Turn of: " + args.Character.DisplayName;
        }

        public void UnregisterToAll()
        {
            GlobalContext.Context.FightContext.NewTurnStarted -= FightContext_NewTurnStarted;
        }

        private void AddToFightButton_Click(object sender, RoutedEventArgs e)
        {
            PlayableEntity currently_playing = GlobalContext.Context.FightContext.CurrentlyPlaying;

            AddToFightWindow window = new AddToFightWindow();
             
            window.ShowCentered();

            RollInitiativeWindow rollInitiativeWindow = new RollInitiativeWindow();
            rollInitiativeWindow.DataContext = GlobalContext.Context.FightContext.FightersList;

            rollInitiativeWindow.ShowCentered();

            GlobalContext.Context.FightContext.FightersList.SetTurnOrdersMiddleFight();

            for (int i = 0; i != GlobalContext.Context.FightContext.FightersList.Count; i++)
            {
                if (GlobalContext.Context.FightContext.FightersList[i] == currently_playing)
                {
                    GlobalContext.Context.FightContext.TurnIndex = i;
                }
            }

        }

        private void NextTurnButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalContext.Context.FightContext.NextTurn();
        }
    }
}
