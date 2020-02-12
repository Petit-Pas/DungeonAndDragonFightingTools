using DDFight.Game.Fight.FightEvents;
using DDFight.Tools;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Controlers.Fight
{
    /// <summary>
    /// Logique d'interaction pour GeneralInfoFightUserControl.xaml
    /// </summary>
    public partial class GeneralInfoFightUserControl : UserControl, IEventUnregisterable
    {
        public GeneralInfoFightUserControl()
        {
            InitializeComponent();
            Loaded += GeneralInfoFightUserControl_Loaded;
        }

        private void setCharactersTurnName()
        {
            CharactersTurnTextBox.Text = Global.Context.FightContext.FightersList.Fighters.ElementAt((int)Global.Context.FightContext.TurnIndex).DisplayName;
        }

        private void GeneralInfoFightUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Global.Context.FightContext.NewTurnStarted += FightContext_NewTurnStarted;
            setCharactersTurnName();
        }

        private void FightContext_NewTurnStarted(object sender, StartNewTurnEventArgs args)
        {
            CharactersTurnTextBox.Text = args.Character.DisplayName;
        }

        public void UnregisterToAll()
        {
            Global.Context.FightContext.NewTurnStarted -= FightContext_NewTurnStarted;
        }
    }
}
