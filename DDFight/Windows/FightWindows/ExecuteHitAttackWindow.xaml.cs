using DDFight.Game;
using DDFight.Game.Aggression.Attacks;
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
    /// Interaction logic for ExecuteHitAttackWindow.xaml
    /// </summary>
    public partial class ExecuteHitAttackWindow : Window
    {
        public HitAttackTemplate data_context
        {
            get => (HitAttackTemplate)DataContext;
        }

        public ExecuteHitAttackWindow()
        {
            InitializeComponent();
            Loaded += ExecuteHitAttackWindow_Loaded;
        }

        private List<HitAttackResult> attacks = new List<HitAttackResult>();

        private void ExecuteHitAttackWindow_Loaded(object sender, RoutedEventArgs e)
        {
            attacks = data_context.GetResultTemplate();
            AttackList.ItemsSource = attacks;
            AttackList.LayoutUpdated += AttackList_LayoutUpdated;
        }

        private List<FrameworkElement> buttons = new List<FrameworkElement>();

        private void AttackList_LayoutUpdated(object sender, EventArgs e)
        {
            List<FrameworkElement> comboBoxes = this.GetAllChildrenByName("HitAttackTargetComboControl");
            foreach (ComboBox box in comboBoxes)
            {
                box.ItemsSource = Global.Context.FightContext.FightersList.Fighters;
            }
            AttackList.LayoutUpdated -= AttackList_LayoutUpdated;

            List<FrameworkElement> damageControls = this.GetAllChildrenByName("DamageControl");
            foreach (ItemsControl control in damageControls)
            {
                control.ItemsSource = data_context.DamageList.Clone();
            }
            Console.WriteLine(damageControls.Count);

            buttons = this.GetAllChildrenByName("AttackButtonControl");
            foreach (Button btn in buttons)
            {
                btn.IsEnabled = false;
            }
            if (buttons.Count != 0)
                ((Button)buttons.ElementAt(0)).IsEnabled = true;
        }

        private void AttackButtonControl_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i != buttons.Count; i += 1)
            {
                if (((Button)buttons[i]).IsEnabled == true)
                {
                    ((Button)buttons[i]).IsEnabled = false;
                    if (i + 1 != buttons.Count)
                    {
                        ((Button)buttons[i + 1]).IsEnabled = true;
                    }
                    else
                    {
                        this.QuitButtonControl.Visibility = Visibility.Visible;
                        this.AttackScrollViewControl.ScrollToEnd();
                    }
                    return;
                }
            }
        }

        private void QuitButtonControl_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
