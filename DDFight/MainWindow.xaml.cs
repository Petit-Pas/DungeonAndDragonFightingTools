using DDFight.Game;
using DDFight.Tools.Save;
using System.Windows;

namespace DDFight
{
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
            };

            DataContext = context;

            InitializeComponent();
        }
    }
}
