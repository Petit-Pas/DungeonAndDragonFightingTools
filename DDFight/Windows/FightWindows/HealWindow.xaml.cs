using DDFight.Game;
using DDFight.Game.Dices;
using System.Windows;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Logique d'interaction pour HealWindow.xaml
    /// </summary>
    public partial class HealWindow : Window
    {
        public PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
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

        public HealWindow()
        {
            InitializeComponent();
            Loaded += HealWindow_Loaded;
        }

        private void HealWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DiceRollControl.DataContext = context;
            DiceRollControl.PropertyPath = "Roll";
        }

        private embedded_roll context = new embedded_roll {
            Roll = new DiceRoll("1d4"),
        };

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!this.AreAllChildrenValid())
            {
                ErrorControl.Visibility = Visibility.Visible;
                return;
            }

            data_context.Heal(context.Roll);
            this.Close();            
        }
    }
}
