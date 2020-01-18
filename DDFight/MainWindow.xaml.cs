using DDFight.Game;
using DDFight.Tools.Save;
using DDFight.Windows;
using DDFight.Windows.FightWindows;
using System;
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
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            SaveManager.Init();

            Global.Context.CharacterList = SaveManager.LoadPlayers();
            Global.Context.MonsterList = SaveManager.LoadMonsters();

            Global.Loading = false;

            Global.Context.FightersList.Fighters.CollectionChanged += FightingCharacters_CollectionChanged;

            DataContext = Global.Context;

            InitializeComponent();
        }

        private void FightingCharacters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FightButton.IsEnabled = Global.Context.FightersList.Fighters.Count >= 2;
        }

        private void FightButton_Click(object sender, RoutedEventArgs e)
        {
            RollInitiativeWindow window = new RollInitiativeWindow();
            window.DataContext = Global.Context.FightersList;
            window.Owner = this;

            window.ShowDialog();

            if (window.Cancelled == false)
            {
                Global.Context.UserLogs = new FlowDocument();
                Paragraph par = new Paragraph();
                par.Inlines.Add(new Run("toto\r\n"));
                par.Inlines.Add(new Run("toto\r\n"));
                par.Inlines.Add(new Run("toto\r\n"));
                par.Inlines.Add(new Run("toto\r\n"));
                par.Inlines.Add(new Run("toto\r\n"));
                par.Inlines.Add(new Run("toto\r\n"));
                Global.Context.UserLogs.Blocks.Add(par);

                MainFightWindow fightWindow = new MainFightWindow();
                fightWindow.DataContext = Global.Context.FightersList;
                fightWindow.ShowDialog();

            }

        }
    }
}
