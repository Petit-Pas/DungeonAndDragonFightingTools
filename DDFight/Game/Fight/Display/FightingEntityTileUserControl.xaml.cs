using DDFight.Game.Dices;
using DDFight.Game.Entities;
using DDFight.Game.Fight.FightEvents;
using DDFight.Tools;
using DDFight.Windows;
using DDFight.Windows.FightWindows;
using DDFight.Windows.ModalWindows.BlankDiceRollModal;
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
            DataContextChanged += FightingEntityTileUserControl_DataContextChanged;
        }

        private void FightingEntityTileUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UnregisterToAll();
            RegisterToAll();
            refresh_InspirationButton();
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

        private void Data_context_TurnEnded(object sender, DDFight.Game.Fight.FightEvents.TurnEndedEventArgs args)
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

        private void RegisterToAll()
        {
            data_context.NewTurnStarted += Data_context_NewTurnStarted;
            data_context.TurnEnded += Data_context_TurnEnded;
            Global.Context.FightContext.CharacterSelected += FightContext_CharacterSelected;
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

            window.ShowCentered();
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

        private void ContextTempHealClick(object sender, RoutedEventArgs e)
        {
            BlankDiceRollModalDataContext context = new BlankDiceRollModalDataContext
            {
                WindowDesc = "Enter the amount (or the roll) for the temporary Hps",
                WindowTitle = "Add Temporary Hps"
            };
            BlankDiceRollModal window = new BlankDiceRollModal { DataContext = context };

            window.ShowCentered();
            if (context.Validated == true)
            {
                data_context.HealTempHP(context.DiceRoll);
            }
        }

        private void ContextHeal_Click(object sender, RoutedEventArgs e)
        {
            HealWindow window = new HealWindow();
            window.Owner = Window.GetWindow(this);

            window.DataContext = data_context;

            window.ShowCentered();
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

            window.ShowCentered();

            if (dc.Yes)
            {
                UnregisterToAll();
                Global.Context.FightContext.RemoveCharacterFromFight(data_context);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            data_context.Edit();
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

                window.ShowCentered();

                if (window.SelectedCharacter != null)
                    data_context.Transform(window.SelectedCharacter);
            }
        }

        private void SavingThrow_Click(object sender, RoutedEventArgs e)
        {
            CustomSavingThrowWindow window = new CustomSavingThrowWindow();
            window.DataContext = this.DataContext;
            window.ShowCentered();
        }

        private void refresh_InspirationButton()
        {
            if (DataContext.GetType() == typeof(Character))
            {
                if (((Character)data_context).HasInspiration)
                {
                    InspirationImage.Source = Application.Current.Resources["InspirationOn"] as ImageSource;
                }
                else
                {
                    InspirationImage.Source = Application.Current.Resources["InspirationOff"] as ImageSource;
                }
            }
        }

        private void Inspiration_Click(object sender, RoutedEventArgs e)
        {
            ((Character)data_context).HasInspiration = !((Character)data_context).HasInspiration;
            refresh_InspirationButton();
        }

    }
}
