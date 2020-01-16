using DDFight.Game;
using DDFight.Game.Dices;
using DDFight.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace DDFight.Windows
{
    /// <summary>
    /// Interaction logic for RollInitiativeWindow.xaml
    /// </summary>
    public partial class RollInitiativeWindow : Window
    {
        public bool Cancelled = true;

        private ObservableCollection<PlayableEntity> data_context
        {
            get => (ObservableCollection<PlayableEntity>)DataContext;
        }

        public RollInitiativeWindow()
        {
            InitializeComponent();
            Loaded += RollInitiativeWindow_Loaded;
        }

        private void RollInitiativeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (PlayableEntity entity in data_context)
            {
                entity.Initiative = 0;
            }
            CharactersItemsControl.ItemsSource = data_context;
        }

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            DiceRoll roll = new DiceRoll("1d20");
            if (!this.AreAllChildrenValid())
            {
                StatusMessageWindow message = new StatusMessageWindow();
                StatusMessageWindowDataContext dc = new StatusMessageWindowDataContext();
                dc.Message = "At least one of the value is invalid";
                dc.Icon = ResourceManager.BmUnchecked();

                message.DataContext = dc;
                message.Owner = this;
                message.ShowDialog();
            }
            else
            {
                foreach (PlayableEntity entity in data_context)
                {
                    if (entity.Initiative == 0)
                        entity.Initiative = (uint)roll.Roll();
                }
            }
        }

        private void LaunchFightButton_Click(object sender, RoutedEventArgs e)
        {
            DiceRoll roll = new DiceRoll("1d20");
            if (!this.AreAllChildrenValid())
            {
                StatusMessageWindow message = new StatusMessageWindow();
                StatusMessageWindowDataContext dc = new StatusMessageWindowDataContext();
                dc.Message = "At least one of the value is invalid";
                dc.Icon = ResourceManager.BmUnchecked();

                message.DataContext = dc;
                message.Owner = this;
                message.ShowDialog();
            }
            else
            {
                foreach (PlayableEntity entity in data_context)
                {
                    if (entity.Initiative == 0)
                        entity.Initiative = (uint)roll.Roll();
                }
                this.Cancelled = false;
                this.Close();
            }
        }
    }
}
