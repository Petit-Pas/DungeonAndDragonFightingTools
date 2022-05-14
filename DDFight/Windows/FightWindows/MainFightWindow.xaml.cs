using DDFight.Controlers.InputBoxes;
using DDFight.Game;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System;
using System.Windows;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight.FightCommands.TurnCommands.StartNextTurnCommands;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Interaction logic for MainFightWindow.xaml
    /// </summary>
    public partial class MainFightWindow : Window
    {
        private static readonly Lazy<IFightersProvider> _lazyFightManager = new(DIContainer.GetImplementation<IFightersProvider>);
        private static readonly IFightersProvider FightersProvider = _lazyFightManager.Value;

        private static Lazy<IMediator> _lazyMediator = new(DIContainer.GetImplementation<IMediator>);
        private static IMediator _mediator => _lazyMediator.Value;


        private GameDataContext data_context
        {
            get => (GameDataContext)DataContext;
        }

        public MainFightWindow()
        {
            InitializeComponent();
            Loaded += MainFightWindow_Loaded;
        }

        private void setupAttacksOwner()
        {
            foreach (PlayableEntity tmp in FightersProvider.Fighters)
            {
                foreach (HitAttackTemplate atk in tmp.HitAttacks)
                {
                    atk.Owner = tmp;
                }
            }
        }

        private void MainFightWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GeneralInfoControl.DataContext = GlobalContext.Context;
            setupAttacksOwner();

            var startNextTurnCommand = new StartNextTurnCommand();
            _mediator.Execute(startNextTurnCommand);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (PlayableEntity tmp in FightersProvider.Fighters)
            {
                tmp.HasAction = !tmp.HasAction;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ConsoleControl.RichTextBoxControl.ClearValue(BindableRichTextBox.DocumentProperty);
        }
    }
}
