using DDFight.Game.Aggression;
using DDFight.Game.Aggression.Attacks;
using DDFight.Tools;
using System.Windows;
using WpfToolsLibrary.Navigation;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Interaction logic for ExecuteHitAttackWindow.xaml
    /// </summary>
    public partial class ExecuteHitAttackWindow : Window
    {
        public HitAttackTemplate data_context
        {
            get => DataContext as HitAttackTemplate;
        }

        public HitAttackResult attackResult = null;

        public ExecuteHitAttackWindow()
        {
            DataContextChanged += ExecuteHitAttackWindow_DataContextChanged;
            InitializeComponent();
        }

        private void ExecuteHitAttackWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (data_context != null)
            {
                unsubscribe();
                attackResult = data_context.GetResultTemplate();
                AttackControl.DataContext = attackResult;
                subscribe();
                refresh_buttons();
            }
        }

        private void unsubscribe()
        {
            if (attackResult != null)
            {
                attackResult.PropertyChanged -= AttackResult_PropertyChanged;
                foreach (DamageResult dmg in attackResult.DamageList.Elements)
                {
                    dmg.Damage.PropertyChanged -= AttackResult_PropertyChanged;
                }
            }
        }

        private void subscribe()
        {
            if (attackResult != null)
            {
                attackResult.PropertyChanged += AttackResult_PropertyChanged;
                foreach (DamageResult dmg in attackResult.DamageList.Elements)
                {
                    dmg.Damage.PropertyChanged += AttackResult_PropertyChanged;
                }
            }
        }

        private void AttackResult_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            refresh_buttons();
        }

        void refresh_buttons()
        {
            RollButtonControl.IsEnabled = false;
            ValidateAndExitButtonControl.IsEnabled = false;
            ValidateAndResetButtonControl.IsEnabled = false;
            if (attackResult.RollResult.AttackRoll == 0)
                RollButtonControl.IsEnabled = true;
            foreach (DamageResult dmg in attackResult.DamageList.Elements)
            {
                if (dmg.Damage.LastRoll == 0)
                    RollButtonControl.IsEnabled = true;
            }
            if (RollButtonControl.IsEnabled == false && attackResult.Target != null && AttackControl.AreAllChildrenValid())
            {
                ValidateAndExitButtonControl.IsEnabled = true;
                ValidateAndResetButtonControl.IsEnabled = true;
            }
        }

        private void RollButtonControl_Click(object sender, RoutedEventArgs e)
        {
            this.RollRollableChildren();
            refresh_buttons();
        }

        private void ValidateAndExitButtonControl_Click(object sender, RoutedEventArgs e)
        {
            AttackControl.Validate();
            this.Close();
        }

        private void ValidateAndResetButtonControl_Click(object sender, RoutedEventArgs e)
        {
            AttackControl.Validate();
            attackResult.Reset();
            refresh_buttons();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (this.IsRollControlPressed(e))
            {
                RollButtonControl_Click(sender, null);
                e.Handled = true;
            }
        }

        private void ResetButtonControl_Click(object sender, RoutedEventArgs e)
        {
            attackResult.Reset();
        }
    }
}
