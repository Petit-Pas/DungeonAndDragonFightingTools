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
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using TempExtensionsOnHitStatus;
using WpfToolsLibrary.ConsoleTools;
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

        public static Window CurrentMainWindow { 
            get => Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            OnHitStatus.RegisterEvents = OnHitStatusGameExtension.Register;
            OnHitStatus.UnregisterEvents = OnHitStatusGameExtension.Unregister;

            Logger.Init();

            GlobalContext.Context.CharacterList = SaveManager.LoadGenericList<Character, CharacterList>(SaveManager.players_folder);
            GlobalContext.Context.MonsterList = SaveManager.LoadGenericList<Monster, MonsterList>(SaveManager.monsters_folder);
            GlobalContext.Context.SpellList = SaveManager.LoadGenericList<Spell, SpellList>(SaveManager.spells_folder);
            GlobalContext.Context.SpellList.IsMainSpellList = true;

            Global.Loading = false;


            GlobalContext.Context.FightContext.FightersList.Elements.CollectionChanged += FightingCharacters_CollectionChanged;

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
            FightButton.IsEnabled = GlobalContext.Context.FightContext.FightersList.Elements.Count >= 2;
        }

        private void FightButton_Click(object sender, RoutedEventArgs e)
        {
            RollInitiativeWindow window = new RollInitiativeWindow();
            window.DataContext = GlobalContext.Context.FightContext.FightersList;

            window.ShowCentered();

            if (window.Cancelled == false)
            {
                GlobalContext.Context.FightContext.FightersList.SetTurnOrders();

                FightConsole.Instance.Reset();

                MainFightWindow fightWindow = new MainFightWindow();
                fightWindow.DataContext = GlobalContext.Context;
                fightWindow.ShowCentered();

                // Clean up after a Fight
                fightWindow.UnregisterAllChildren();
                foreach (Character character in GlobalContext.Context.FightContext.FightersList.Elements.OfType<Character>())
                {
                    character.GetOutOfFight();
                }
                GlobalContext.Context.FightContext.Reset();
                GenericList<Character>.SaveAll<Character>(GlobalContext.Context.CharacterList);
                //Global.Context.CharacterList.SaveAll();
            }

        }
    }
}
