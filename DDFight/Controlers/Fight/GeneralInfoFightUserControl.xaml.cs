using DDFight.Game.Fight.FightEvents;
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
    /// Logique d'interaction pour GeneralInfoFightUserControl.xaml
    /// </summary>
    public partial class GeneralInfoFightUserControl : UserControl
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
    }
}
