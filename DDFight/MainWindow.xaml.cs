using DDFight.Game;
using DDFight.Tools.Save;
using System.Windows;

namespace DDFight
{
    public class GlobalVariables
    {
        /// <summary>
        ///     required for any constructor that builds a list in an item that can be saved
        ///     Loading == trues ==> do not instantiate the list as it means you are in the Loading phase where the program deserializes .xml files, the list will be done by the xmlDerserializer
        ///     Loading == false ==> you really asked for a new instance of whatever the class you did instantiate and you can then initialize its lists.
        /// </summary>
        public static bool Loading = true;
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            SaveManager.Init();

            GameDataContext context = new GameDataContext
            {
                CharacterList = SaveManager.LoadPlayers(),
                MonsterList = SaveManager.LoadMonsters(),
            };

            GlobalVariables.Loading = false;

            DataContext = context;

            InitializeComponent();
        }
    }
}
