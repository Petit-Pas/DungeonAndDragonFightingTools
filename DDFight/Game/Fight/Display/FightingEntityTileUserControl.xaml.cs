using DDFight.Game;
using DDFight.Game.Dices;
using DDFight.Game.Fight.FightEvents;
using DDFight.Tools;
using DDFight.Windows;
using DDFight.Windows.FightWindows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DDFight.Controlers.Fight
{
    /// <summary>
    /// Logique d'interaction pour FightingCharacterTileDataContext.xaml
    /// </summary>
    public partial class FightingEntityTileUserControl : UserControl, IEventUnregisterable
    {
        public PlayableEntity data_context
        {
            get {
                return (PlayableEntity)DataContext;
            }
        }
        public FightingEntityTileUserControl()
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
            this.UnregisterAll();
            data_context.NewTurnStarted -= Data_context_NewTurnStarted;
            data_context.TurnEnded -= Data_context_TurnEnded;
            Global.Context.FightContext.CharacterSelected -= FightContext_CharacterSelected;
        }

        private void ContextTakeDamage_Click(object sender, RoutedEventArgs e)
        {
            TakeDamageWindow window = new TakeDamageWindow();
            window.Owner = Window.GetWindow(this);

            window.DataContext = data_context;

            window.ShowDialog();
        }

        public class embedded_roll
        {
            public DiceRoll Roll
            {
                get => _roll;
                set
                {
                    _roll = value;
                }
            }
            private DiceRoll _roll;
        }

        private void ContextHeal_Click(object sender, RoutedEventArgs e)
        {
            HealWindow window = new HealWindow();
            window.Owner = Window.GetWindow(this);

            window.DataContext = data_context;

            window.ShowDialog();
        }

        private void ContextManageStatus_Click(object sender, RoutedEventArgs e)
        {
            data_context.CustomVerboseStatusList.OpenEditWindow();
        }

        private void RemoveFromFight_Click(object sender, RoutedEventArgs e)
        {
            AskYesNoWindow window = new AskYesNoWindow();
            AskYesNoDataContext dc = new AskYesNoDataContext()
            {
                Message = "Are you sure you want to remove " + data_context.DisplayName + " from the fight?",
            };
            window.DataContext = dc;

            window.ShowDialog();

            if (dc.Yes)
            {
                UnregisterToAll();
                Global.Context.FightContext.RemoveCharacterFromFight(data_context);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            data_context.OpenEditWindow();
        }

        private void Transform_Click(object sender, RoutedEventArgs e)
        {
            if (data_context.IsTransformed)
                data_context.TransformBack();
            else
            {
                SelectPlayableEntityWindow window = new SelectPlayableEntityWindow();
                window.DataContext = Global.Context.MonsterList.Monsters.Clone<PlayableEntity, Monster>();
                window.TitleControl.Text = "Select the monster in which to transform";

                window.ShowDialog();

                if (window.SelectedCharacter != null)
                    data_context.Transform(window.SelectedCharacter);
            }
        }

        private void SavingThrow_Click(object sender, RoutedEventArgs e)
        {
            CustomSavingThrowWindow window = new CustomSavingThrowWindow();
            window.DataContext = this.DataContext;
            window.ShowDialog();
        }
    }
}
