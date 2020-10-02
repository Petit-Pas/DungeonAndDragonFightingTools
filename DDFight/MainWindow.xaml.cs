﻿using DDFight.Game;
using DDFight.Tools;
using DDFight.Tools.Save;
using DDFight.Windows;
using DDFight.Windows.FightWindows;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace DDFight
{
    public class Global
    {
        /// <summary>
        ///     required for any constructor that builds a list in an item that can be saved
        ///     Loading == trues ==> do not instantiate the list as it means you are in the Loading phase where the program deserializes .xml files, the list will be done by the xmlDerserializer
        ///     Loading == false ==> you really asked for a new instance of whatever the class you did instantiate and you can then initialize its lists.
        /// </summary>
        public static bool Loading = true;

        public static GameDataContext Context = new GameDataContext();

        public static void Save ()
        {
            Context.CharacterList.Save();
            Context.MonsterList.Save();
            Context.SpellList.Save();
        }

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


            Logger.Init();
            SaveManager.Init();

            Global.Context.CharacterList = SaveManager.LoadPlayers();
            Global.Context.MonsterList = SaveManager.LoadMonsters();
            Global.Context.SpellList = SaveManager.LoadSpells();

            Global.Loading = false;

            Global.Context.FightContext.FightersList.Fighters.CollectionChanged += FightingCharacters_CollectionChanged;

            DataContext = Global.Context;

            InitializeComponent();
        }

        private void FightingCharacters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FightButton.IsEnabled = Global.Context.FightContext.FightersList.Fighters.Count >= 2;
        }

        private void FightButton_Click(object sender, RoutedEventArgs e)
        {
            RollInitiativeWindow window = new RollInitiativeWindow();
            window.DataContext = Global.Context.FightContext.FightersList;

            window.ShowCentered();

            if (window.Cancelled == false)
            {
                Global.Context.FightContext.FightersList.SetTurnOrders();

                Global.Context.UserLogs = new FlowDocument();

                MainFightWindow fightWindow = new MainFightWindow();
                fightWindow.DataContext = Global.Context;
                fightWindow.ShowCentered();

                // Clean up after a Fight
                fightWindow.UnregisterAll();
                foreach (Character character in Global.Context.FightContext.FightersList.Fighters.OfType<Character>())
                {
                    character.GetOutOfFight();
                }
                Global.Context.FightContext.Reset();
                Global.Context.CharacterList.Save();
            }

        }
    }
}
