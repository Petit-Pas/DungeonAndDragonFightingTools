using DDFight.Controlers.InputBoxes;
using DDFight.Game;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System;
using System.Windows;
using BaseToolsLibrary.DependencyInjection;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Interaction logic for MainFightWindow.xaml
    /// </summary>
    public partial class MainFightWindow : Window
    {
        private static readonly Lazy<IFightManager> _lazyFightManager = new(DIContainer.GetImplementation<IFightManager>);
        private static readonly IFightManager _fightManager = _lazyFightManager.Value;

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
            foreach (PlayableEntity tmp in _fightManager.GetAllFighters())
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
            GlobalContext.Context.FightContext.NextTurn();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (PlayableEntity tmp in _fightManager.GetAllFighters())
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
