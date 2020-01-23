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

        private void AttackList_LayoutUpdated(object sender, EventArgs e)
        {
            List<FrameworkElement> comboBoxes = this.GetAllChildrenByName("HitAttackTargetComboControl");
            Console.WriteLine(comboBoxes.Count);
            foreach (ComboBox box in comboBoxes)
            {
                box.ItemsSource = Global.Context.FightContext.FightersList.Fighters;
            }
            AttackList.LayoutUpdated -= AttackList_LayoutUpdated;

            List<FrameworkElement> tmp = this.GetAllChildrenByName("DamageControl");
            foreach (ItemsControl control in tmp)
            {
                control.ItemsSource = data_context.DamageList;
            }
            Console.WriteLine(tmp.Count);
        }
    }
}
