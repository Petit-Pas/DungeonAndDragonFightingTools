using System;
using DDFight.Resources;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using BaseToolsLibrary.DependencyInjection;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace DDFight.Windows
{
    /// <summary>
    /// Interaction logic for RollInitiativeWindow.xaml
    /// </summary>
    public partial class RollInitiativeWindow : Window
    {
        private static readonly Lazy<IFightersProvider> _lazyFightManager = new(DIContainer.GetImplementation<IFightersProvider>);
        private static readonly IFightersProvider FightersProvider = _lazyFightManager.Value;

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

        public RollInitiativeWindow()
        {
            InitializeComponent();
            Loaded += RollInitiativeWindow_Loaded;
        }

        private ObservableCollection<InitiativeCellDataContext> contextList;

        private void RollInitiativeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            contextList = new ObservableCollection<InitiativeCellDataContext>();
            foreach (var entity in FightersProvider.Fighters)
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
            foreach (PlayableEntity entity in FightersProvider.Fighters)
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
