using DDFight.Controlers.InputBoxes;
using DDFight.Game;
using DDFight.Game.Characteristics;
using DDFight.Game.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Interaction logic for MainFightWindow.xaml
    /// </summary>
    public partial class MainFightWindow : Window
    {
        private FightingCharactersDataContext data_context
        {
            get => (FightingCharactersDataContext)DataContext;
        }
        public MainFightWindow()
        {
            InitializeComponent();
            Loaded += MainFightWindow_Loaded;
        }

        private void MainFightWindow_Loaded(object sender, RoutedEventArgs e)
        {
            data_context.SetTurnOrders();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (PlayableEntity tmp in Global.Context.FightersList.Fighters)
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
