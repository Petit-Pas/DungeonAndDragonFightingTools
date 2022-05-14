using System;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using System.Windows;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.Heal;
using TempExtensionsPlayableEntity;
using WpfToolsLibrary.Extensions;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Logique d'interaction pour HealWindow.xaml
    /// </summary>
    public partial class HealWindow : Window
    {
        private Lazy<IMediator> _lazyMediator = new(DIContainer.GetImplementation<IMediator>());
        private IMediator _mediator => _lazyMediator.Value;

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

            context.Roll.Roll();
            var healCommand = new HealCommand(data_context, context.Roll.LastResult);
            _mediator.Execute(healCommand);

            this.Close();            
        }
    }
}
