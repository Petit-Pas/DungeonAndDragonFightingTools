using DDFight.Controlers.InputBoxes;
using DDFight.Game;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System;
using System.Windows;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Interaction logic for MainFightWindow.xaml
    /// </summary>
    public partial class MainFightWindow : Window
    {
        private GameDataContext data_context
        {
            get => (GameDataContext)DataContext;
        }

        private FightersList fighters
        {
            get => data_context.FightContext.FightersList;
        }

        public MainFightWindow()
        {
            InitializeComponent();
            Loaded += MainFightWindow_Loaded;
        }

        private void setupAttacksOwner()
        {
            foreach (PlayableEntity tmp in data_context.FightContext.FightersList.Elements)
            {
                foreach (HitAttackTemplate atk in tmp.HitAttacks.Elements)
                {
                    atk.Owner = tmp;
                }
            }
        }

        private void MainFightWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GeneralInfoControl.DataContext = GlobalContext.Context;
            setupAttacksOwner();
            GlobalContext.Context.FightContext.NextTurn();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (PlayableEntity tmp in GlobalContext.Context.FightContext.FightersList.Elements)
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
