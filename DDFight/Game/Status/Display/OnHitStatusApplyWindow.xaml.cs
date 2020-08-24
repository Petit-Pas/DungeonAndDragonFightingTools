using DDFight.Game.Characteristics;
using DDFight.Game.Dices.SavingThrow;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

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
        }

        private void OnHitStatusApplyWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            refresh_validate_button();
        }

        private void refresh_validate_button()
        {
            if (((SavingThrow)SavingThrowControl.DataContext).SavingRoll == 0)
                ValidateButtonControl.IsEnabled = false;
            else
                ValidateButtonControl.IsEnabled = true;
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

        private void applyStatus()
        {
            data_context.Apply(Applicant, Target);
        }

        /// <summary>
        ///     Method that validates wether or not the status affects the character
        ///     In opposition with validateResist() below
        /// </summary>
        private void validateOnHit()
        {
            Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            if (data_context.HasApplyCondition)
            {
                // there is a saving throw to resist the status
                Characteristic charac = Target.Characteristics.GetCharacteristic(data_context.ApplySavingCharacteristic);
                int total = ((SavingThrow)SavingThrowControl.DataContext).SavingRoll + ((SavingThrow)SavingThrowControl.DataContext).Modifier + charac.Modifier + (int)(charac.Mastery ? Target.Characteristics.MasteryBonus : 0);

                paragraph.Inlines.Add(Extensions.BuildRun(Target.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" tries to resist the ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(data_context.Header, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" status from ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(Applicant.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" ==> ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(total.ToString() + "/" + ((SavingThrow)SavingThrowControl.DataContext).Difficulty.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" ==> ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

                if (total >= data_context.ApplySavingDifficulty)
                {
                    //resist
                    paragraph.Inlines.Add(Extensions.BuildRun("Success\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                }
                else
                {
                    //fails
                    paragraph.Inlines.Add(Extensions.BuildRun("Failure\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    applyStatus();
                }
            }
            else
            {
                // there is no saving throw to resist the status 
                paragraph.Inlines.Add(Extensions.BuildRun(Applicant.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" applies ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(data_context.Header, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" on ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(Target.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                applyStatus();
            }
        }

        /// <summary>
        ///     Method that validates wether or not the target finally remove the status
        ///     In opposition with validateOnHit() below
        /// </summary>
        private void validateResist()
        {
            Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            Characteristic charac = Target.Characteristics.GetCharacteristic(data_context.ApplySavingCharacteristic);
            int total = ((SavingThrow)SavingThrowControl.DataContext).SavingRoll + ((SavingThrow)SavingThrowControl.DataContext).Modifier + charac.Modifier + (int)(charac.Mastery ? Target.Characteristics.MasteryBonus : 0);

            paragraph.Inlines.Add(Extensions.BuildRun(Target.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" tries again to resist the ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(Extensions.BuildRun(data_context.Header, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" status from ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(Extensions.BuildRun(Applicant.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" ==> ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(Extensions.BuildRun(total.ToString() + "/" + ((SavingThrow)SavingThrowControl.DataContext).Difficulty.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" ==> ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

            if (total >= data_context.ApplySavingDifficulty)
            {
                //resist
                paragraph.Inlines.Add(Extensions.BuildRun("Success\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                data_context.UnregisterToAll();
                data_context.Affected.CustomVerboseStatusList.List.Remove(data_context);
            }
            else
            {
                //fails
                paragraph.Inlines.Add(Extensions.BuildRun("Failure\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            }
        }

        private void ValidateButtonControl_Click(object sender, RoutedEventArgs e)
        {
            if ((string)ValidateButtonControl.Content == "Validate")
            {
                if (first_application == true)
                    validateOnHit();
                else
                    validateResist();
                this.Close();
            }
            else
                SavingThrowControl.Roll();
        }

        private void CancelButtonControl_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
