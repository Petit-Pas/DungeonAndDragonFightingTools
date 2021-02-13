using DDFight.Game.Entities;
using DDFight.Game.Fight.FightEvents;
using DDFight.Tools;
using DDFight.Windows;
using DDFight.Windows.FightWindows;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            CharacterTurnTextboxCountrol.Text = "Turn of: " + Global.Context.FightContext.FightersList.Fighters.ElementAt((int)Global.Context.FightContext.TurnIndex).DisplayName;
        }

        private void GeneralInfoFightUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Global.Context.FightContext.NewTurnStarted += FightContext_NewTurnStarted;
            setCharactersTurnName();
        }

        private void FightContext_NewTurnStarted(object sender, StartNewTurnEventArgs args)
        {
            CharacterTurnTextboxCountrol.Text = "Turn of: " + args.Character.DisplayName;
        }

        public void UnregisterToAll()
        {
            Global.Context.FightContext.NewTurnStarted -= FightContext_NewTurnStarted;
        }

        private void AddToFightButton_Click(object sender, RoutedEventArgs e)
        {
            PlayableEntity currently_playing = Global.Context.FightContext.CurrentlyPlaying;

            AddToFightWindow window = new AddToFightWindow();
             
            window.ShowCentered();

            RollInitiativeWindow rollInitiativeWindow = new RollInitiativeWindow();
            rollInitiativeWindow.DataContext = Global.Context.FightContext.FightersList;

            rollInitiativeWindow.ShowCentered();

            Global.Context.FightContext.FightersList.SetTurnOrdersMiddleFight();

            for (int i = 0; i != Global.Context.FightContext.FightersList.Fighters.Count; i++)
            {
                if (Global.Context.FightContext.FightersList.Fighters[i] == currently_playing)
                {
                    Global.Context.FightContext.TurnIndex = i;
                }
            }

        }

        private void NextTurnButton_Click(object sender, RoutedEventArgs e)
        {
            Global.Context.FightContext.NextTurn();
        }
    }
}
