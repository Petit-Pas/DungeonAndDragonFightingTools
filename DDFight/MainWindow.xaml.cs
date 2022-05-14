using System;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Memory;
using DDFight.Game;
using DDFight.Game.Entities;
using DDFight.Windows;
using DDFight.Windows.FightWindows;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Memory;
using DnDToolsLibrary.Status;
using System.Windows;
using DnDToolsLibrary.Fight;
using TempExtensionsOnHitStatus;
using WpfDnDCommandHandlers.AttackQueries.DamageQueries.DamageResultListQueries;
using WpfToolsLibrary.Extensions;

namespace DDFight
{
    public class GlobalContext
    {
        /// <summary>
        ///     required for any constructor that builds a list in an item that can be saved
        ///     Loading == trues ==> do not instantiate the list as it means you are in the Loading phase where the program deserializes .xml files, the list will be done by the xmlDerserializer
        ///     Loading == false ==> you really asked for a new instance of whatever the class you did instantiate and you can then initialize its lists.
        /// </summary>

        public static GameDataContext Context = new GameDataContext();
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Lazy<IFightersProvider> _lazyFightManager = new(DIContainer.GetImplementation<IFightersProvider>);
        protected static IFightersProvider _fightersProvider => _lazyFightManager.Value;

        private static readonly Lazy<ITurnManager> _lazyTurnManager = new(DIContainer.GetImplementation<ITurnManager>);
        protected static ITurnManager _turnManager => _lazyTurnManager.Value;

        private ICustomConsole console;

        public MainWindow()
        {
            // this lines serves the purpose of force loading its .dll, enabling the mediator to find the handler.
            DamageResultListQueryHandler handler = new DamageResultListQueryHandler();

            Logger.Init();

            DIConfigurer.ConfigureCore();
            DIConfigurer.ConfigureWpf();
            DIConfigurer.Verify();


            GlobalContext.Context.CharacterList = SaveManager.LoadGenericList<Character, CharacterList>(SaveManager.players_folder);
            GlobalContext.Context.MonsterList = SaveManager.LoadGenericList<Monster, MonsterList>(SaveManager.monsters_folder);
            GlobalContext.Context.SpellList = SaveManager.LoadGenericList<Spell, SpellList>(SaveManager.spells_folder);
            GlobalContext.Context.SpellList.IsMainSpellList = true;

            HandlerToUiConfig.Configure();

            Global.Loading = false;

            console = DIContainer.GetImplementation<ICustomConsole>();

            _fightersProvider.GetObservableCollection().CollectionChanged += FightingCharacters_CollectionChanged;

            DataContext = GlobalContext.Context;

            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalSpellListControl.DataContext = GlobalContext.Context.SpellList;
        }

        private void FightingCharacters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FightButton.IsEnabled = _fightersProvider.FighterCount >= 2;
        }

        private void FightButton_Click(object sender, RoutedEventArgs e)
        {
            RollInitiativeWindow window = new RollInitiativeWindow();
            window.DataContext = _fightersProvider.Fighters;

            window.ShowCentered();

            if (window.Cancelled == false)
            {
                _turnManager.SetTurnOrders();

                console.Reset();

                MainFightWindow fightWindow = new MainFightWindow();
                fightWindow.DataContext = GlobalContext.Context;
                fightWindow.ShowCentered();

                // Clean up after a Fight
                fightWindow.UnregisterAllChildren();
                foreach (var character in _fightersProvider.Characters)
                {
                    character.GetOutOfFight();
                }

                _fightersProvider.Clear();
                _turnManager.Reset();
                
                GenericList<Character>.SaveAll(GlobalContext.Context.CharacterList);
            }

        }
    }
}
