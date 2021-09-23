using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.HitAttackCommands.ApplyHitAttackResult;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.HitAttacks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfCustomControlLibrary.ModalWindows;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCustomControlLibrary.Attacks.HitAttacks
{
    /// <summary>
    /// Interaction logic for HitAttacksExecuteWindow.xaml
    /// </summary>
    public partial class HitAttackExecuteWindow : Window, INotifyPropertyChanged, IValidableWindow
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
                foreach (DamageResult dmg in AttackResult.DamageList)
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
                foreach (DamageResult dmg in AttackResult.DamageList)
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

        public bool Validated { get ; set; }

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

        private static IMediator mediator = DIContainer.GetImplementation<IMediator>();

        private void validateAttack()
        {
            if (this.AreAllChildrenValid())
            {
                mediator.Execute(new ApplyHitAttackResultCommand(AttackResult));
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
                Validated = true;
                this.Close();
            }
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
        }
    }
}
