using DDFight.Commands;
using DDFight.Commands.AttackCommands;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.HitAttacks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace DDFight.Game.Aggression.Attacks
{
    /// <summary>
    /// Interaction logic for HitAttacksExecuteWindow.xaml
    /// </summary>
    public partial class HitAttackExecuteWindow : Window, INotifyPropertyChanged
    {
        public HitAttackExecuteWindow()
        {
            DataContextChanged += HitAttacksExecuteWindow_DataContextChanged;
            PropertyChanged += HitAttackExecuteWindow_PropertyChanged;
            InitializeComponent();
        }

        private void HitAttacksExecuteWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is HitAttackTemplate attackTemplate)
                AttackTemplate = attackTemplate;
        }

        private static void attackTemplate_Updated(DependencyObject o, DependencyPropertyChangedEventArgs a)
        {
            ((HitAttackExecuteWindow)o).AttackTemplate_Updated();
        }

        private void AttackTemplate_Updated()
        {
            AttackResult = AttackTemplate.GetResultTemplate();
        }

        private void unregister()
        {
            if (AttackResult != null)
            {
                AttackResult.PropertyChanged -= Context_PropertyChanged;
                AttackResult.RollResult.PropertyChanged -= Context_PropertyChanged;
                foreach (DamageResult dmg in AttackResult.DamageList.Elements)
                {
                    dmg.PropertyChanged -= Context_PropertyChanged;
                }
            }
        }

        private void register()
        {
            if (AttackResult != null)
            {
                AttackResult.PropertyChanged += Context_PropertyChanged;
                AttackResult.RollResult.PropertyChanged += Context_PropertyChanged;
                foreach (DamageResult dmg in AttackResult.DamageList.Elements)
                {
                    dmg.PropertyChanged += Context_PropertyChanged;
                }
            }
        }

        private void Context_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            refreshButtons();
        }

        public HitAttackResult AttackResult 
        {
            get => _attackResult;
            set
            {
                unregister();
                _attackResult = value;
                NotifyPropertyChanged();
                register();
            }
        }
        private HitAttackResult _attackResult;

        public HitAttackTemplate AttackTemplate
        {
            get { return (HitAttackTemplate)this.GetValue(AttackTemplateProperty); }
            set { this.SetValue(AttackTemplateProperty, value); }
        }
        private static readonly DependencyProperty AttackTemplateProperty = DependencyProperty.Register(
            nameof(AttackTemplate),
            typeof(HitAttackTemplate),
            typeof(HitAttackExecuteWindow),
            new PropertyMetadata(null, attackTemplate_Updated));
       
        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion INotifyPropertyChanged

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.IsRollControlPressed(e))
            {
                this.RollRollableChildren();
            }
        }

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            this.RollRollableChildren();
        }

        private void refreshButtons()
        {
            RollButton.IsEnabled = false;
            ValidateResetButton.IsEnabled = false;
            ValidateExitButton.IsEnabled = false;

            if (this.AreAllRollableChildrenRolled() == false)
                RollButton.IsEnabled = true;
            else if (this.AreAllChildrenValid() && AttackResult.Target != null)
            {
                ValidateResetButton.IsEnabled = true;
                ValidateExitButton.IsEnabled = true;
            }
        }

        private void HitAttackExecuteWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            refreshButtons();
        }

        private void validateAttack()
        {
            if (this.AreAllChildrenValid())
            {
                DnDCommandManager.StaticTryExecute(new ApplyHitAttackResultCommand(AttackResult, false));
            }
        }

        private void ValidateResetButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.AreAllChildrenValid())
            {
                validateAttack();
                AttackResult.Reset();
            }
        }

        private void ValidateExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.AreAllChildrenValid())
            {
                validateAttack();
                SelfClosing = true;
                this.Close();
            }
        }

        #region CloseHandling

        private bool SelfClosing = false;

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (SelfClosing == false)
            {
                // windows X key pressed
            }
        }
        #endregion CloseHandling
    }
}
