﻿using DDFight.Game;
using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Aggression.Spells;
using DDFight.Game.Entities;
using DDFight.Tools;
using DDFight.Tools.Save;
using DDFight.Windows;
using DDFight.Windows.FightWindows;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Serialization;

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

            Global.Context.CharacterList = SaveManager.LoadGenericList<Character, CharacterList>("new_test\\characters\\");
            Global.Context.MonsterList = SaveManager.LoadGenericList<Monster, MonsterList>("new_test\\monsters\\");
            Global.Context.SpellList = SaveManager.LoadGenericList<Spell, SpellList>("new_test\\spells\\");
            Global.Context.SpellList.IsMainSpellList = true;

            Global.Loading = false;


            Global.Context.FightContext.FightersList.Elements.CollectionChanged += FightingCharacters_CollectionChanged;

            DataContext = Global.Context;

            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalSpellListControl.DataContext = Global.Context.SpellList;
        }

        private void FightingCharacters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FightButton.IsEnabled = Global.Context.FightContext.FightersList.Elements.Count >= 2;
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
                foreach (Character character in Global.Context.FightContext.FightersList.Elements.OfType<Character>())
                {
                    character.GetOutOfFight();
                }
                Global.Context.FightContext.Reset();
                Global.Context.CharacterList.SaveAll();
            }

        }
    }
}
