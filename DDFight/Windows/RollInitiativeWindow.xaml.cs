using DDFight.Resources;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace DDFight.Windows
{
    /// <summary>
    /// Interaction logic for RollInitiativeWindow.xaml
    /// </summary>
    public partial class RollInitiativeWindow : Window
    {
        public class InitiativeCellDataContext
        {
            public PlayableEntity Entity
            {
                get => _entity;
                set
                {
                    _entity = value;
                }
            }
            private PlayableEntity _entity;

            public string Name
            {
                get => _entity.Name;
            }

            public int Amount
            {
                get => _amount;
                set
                {
                    _amount = value;
                }
            }
            private int _amount;
        }

        public bool Cancelled = true;

        private FightersList data_context
        {
            get => (FightersList)DataContext;
        }

        public RollInitiativeWindow()
        {
            InitializeComponent();
            Loaded += RollInitiativeWindow_Loaded;
        }

        private ObservableCollection<InitiativeCellDataContext> contextList;

        private void RollInitiativeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            contextList = new ObservableCollection<InitiativeCellDataContext>();
            foreach (PlayableEntity entity in data_context)
            {
                if (contextList.Any(x => x.Entity.Name == entity.Name))
                {
                    contextList.First(x => x.Entity.Name == entity.Name).Amount += 1;
                }
                else
                {
                    InitiativeCellDataContext tmp = new InitiativeCellDataContext() {
                        Amount = 1,
                        Entity = entity,
                    };
                    contextList.Add(tmp);
                }
            }
            CharactersItemsControl.ItemsSource = contextList;
        }

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.AreAllChildrenValid())
            {
                StatusMessageWindow message = new StatusMessageWindow();
                StatusMessageWindowDataContext dc = new StatusMessageWindowDataContext();
                dc.Message = "At least one of the value is invalid";
                dc.Icon = ResourceManager.BmUnchecked();

                message.DataContext = dc;
                message.ShowCentered();
            }
            else
            {
                foreach (InitiativeCellDataContext tmp in contextList)
                {
                    if (tmp.Entity.InitiativeRoll == 0)
                        tmp.Entity.InitiativeRoll = (uint)DiceRoll.Roll("1d20");
                }
            }
        }

        private void LaunchFightButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.AreAllChildrenValid())
            {
                StatusMessageWindow message = new StatusMessageWindow();
                StatusMessageWindowDataContext dc = new StatusMessageWindowDataContext();
                dc.Message = "At least one of the value is invalid";
                dc.Icon = ResourceManager.BmUnchecked();

                message.DataContext = dc;
                message.ShowCentered();
            }
            else
            {
                foreach (InitiativeCellDataContext tmp in contextList)
                {
                    if (tmp.Entity.InitiativeRoll == 0)
                        tmp.Entity.InitiativeRoll = (uint)DiceRoll.Roll("1d20");
                }
                this.Cancelled = false;
                this.Close();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            foreach (PlayableEntity entity in data_context)
            {
                if (entity.InitiativeRoll == 0)
                {
                    entity.InitiativeRoll = contextList.First(x => x.Entity.Name == entity.Name).Entity.InitiativeRoll;
                }
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (this.IsRollControlPressed(e))
            {
                RollButton_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
