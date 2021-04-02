using DDFight.Game.Dices.SavingThrow;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using TempExtensionsOnHitStatus;
using WpfToolsLibrary.ConsoleTools;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Logique d'interaction pour OnHitStatusApplyWindow.xaml
    /// </summary>
    public partial class OnHitStatusApplyWindow : Window, INotifyPropertyChanged
    {
        private OnHitStatus data_context
        {
            get => (OnHitStatus)DataContext;
        }

        public PlayableEntity Target
        {
            get => _target;
            set
            {
                _target = value;
                NotifyPropertyChanged();
            }
        }
        private PlayableEntity _target;

        public PlayableEntity Applicant
        {
            get => _applicant;
            set
            {
                _applicant = value;
                NotifyPropertyChanged();
            }
        }
        private PlayableEntity _applicant;

        private bool first_application = true;

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="applicant"> The applicant of the status </param>
        /// <param name="target"> The target of the status </param>
        /// <param name="first_application"> set to false if this window is used to retry the saving throw in a further round
        ///                                  set to true (default) if the window is used to determine wether or not the target is affected in the first place
        /// </param>
        public OnHitStatusApplyWindow(PlayableEntity applicant, PlayableEntity target, bool first_application = true)
        {
            Target = target;
            Applicant = applicant;
            this.first_application = first_application;

            InitializeComponent();
            DataContextChanged += OnHitStatusApplyWindow_DataContextChanged;
        }

        private void OnHitStatusApplyWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            refresh_saving_control();
            refresh_validate_button();
            foreach (DamageTemplate dmg in data_context.OnApplyDamageList.Elements)
            {
                dmg.Damage.PropertyChanged += Dmg_PropertyChanged;
            }
        }

        private void Dmg_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            refresh_validate_button();
        }

        private void OnHitStatusApplyWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            refresh_validate_button();
        }

        private void refresh_validate_button()
        {
            ValidateButtonControl.IsEnabled = true;
            if (((SavingThrow)SavingThrowControl.DataContext).SavingRoll == 0)
                ValidateButtonControl.IsEnabled = false;
            foreach (DamageTemplate dmg in data_context.OnApplyDamageList.Elements)
            {
                if (dmg.Damage.LastRoll == 0)
                    ValidateButtonControl.IsEnabled = false;
            }
        }

        private void refresh_saving_control()
        {
            if (data_context.HasApplyCondition)
            {
                SavingThrowControl.DataContext = data_context.GetSavingThrow(Applicant, Target);
                SavingThrowControl.Visibility = Visibility.Visible;
                ((SavingThrow)SavingThrowControl.DataContext).PropertyChanged += OnHitStatusApplyWindow_PropertyChanged;
            }
        }

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
        #endregion

        /// <param name="success"> mirrors the success parameter of the OnHitStatus.Apply() method </param>
        private void applyStatus(bool success = true)
        {
            data_context.Apply(Applicant, Target, application_success: success);
        }

        /// <summary>
        ///     Method that validates wether or not the status affects the character
        ///     In opposition with validateResist() below
        /// </summary>
        private void validateOnHit()
        {
            Paragraph paragraph = (Paragraph)FightConsole.Instance.UserLogs.Blocks.LastBlock;
            SavingThrow saving = ((SavingThrow)SavingThrowControl.DataContext);

            if (data_context.HasApplyCondition)
            {
                // there is a saving throw to resist the status
                Characteristic charac = Target.Characteristics.GetCharacteristic(data_context.ApplySavingCharacteristic);
                
                paragraph.Inlines.Add(RunExtensions.BuildRun(Target.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(RunExtensions.BuildRun(" tries to resist the ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(RunExtensions.BuildRun(data_context.Header, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(RunExtensions.BuildRun(" status from ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(RunExtensions.BuildRun(Applicant.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(RunExtensions.BuildRun(". ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(RunExtensions.BuildRun(saving.Result.ToString() + "/" + saving.Difficulty.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(RunExtensions.BuildRun(" ==> ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

                if (saving.Result >= saving.Difficulty)
                {
                    //resist
                    paragraph.Inlines.Add(RunExtensions.BuildRun("Success\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    applyStatus(false);
                }
                else
                {
                    //fails
                    paragraph.Inlines.Add(RunExtensions.BuildRun("Failure\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    applyStatus();
                }
            }
        }

        /// <summary>
        ///     Method that validates wether or not the target finally remove the status
        ///     In opposition with validateOnHit() below
        /// </summary>
        private void validateResist()
        {
            Paragraph paragraph = (Paragraph)FightConsole.Instance.UserLogs.Blocks.LastBlock;

            SavingThrow saving = ((SavingThrow)SavingThrowControl.DataContext);

            paragraph.Inlines.Add(RunExtensions.BuildRun(Target.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(" tries again to resist the ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(RunExtensions.BuildRun(data_context.Header, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(" status from ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(RunExtensions.BuildRun(Applicant.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(" ==> ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(RunExtensions.BuildRun(saving.Result.ToString() + "/" + saving.Difficulty.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(" ==> ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

            if (saving.Result >= saving.Difficulty)
            {
                //resist
                paragraph.Inlines.Add(RunExtensions.BuildRun("Success\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                data_context.Affected.CustomVerboseStatusList.RemoveElement(data_context);
                //TODO check if not too dangerous
                //data_context.Unregister();
                data_context.Dispose();
            }
            else
            {
                //fails
                paragraph.Inlines.Add(RunExtensions.BuildRun("Failure\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            }
        }

        private void ValidateButtonControl_Click(object sender, RoutedEventArgs e)
        {
            if (first_application == true)
                validateOnHit();
            else
                validateResist();
            this.Close();
        }

        private void CancelButtonControl_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (this.IsRollControlPressed(e))
            {
                this.RollRollableChildren();
                e.Handled = true;
                foreach (DamageTemplate dmg in data_context.OnApplyDamageList.Elements)
                {
                    if (dmg.Damage.LastRoll == 0)
                        dmg.Damage.Roll();
                }
            }
        }
    }
}
