using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TempExtensionsOnHitStatus;
using WpfToolsLibrary.Navigation;

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Logique d'interaction pour OnHitStatusApplyWindow.xaml
    /// </summary>
    /// 

    // TODO the was this control works is absolutely awful and it should be changed when switching to commands
    public partial class OnHitStatusApplyWindow : Window, INotifyPropertyChanged
    {
        private ICustomConsole console = DIContainer.GetImplementation<ICustomConsole>();
        private IFontWeightProvider fontWeightProvider = DIContainer.GetImplementation<IFontWeightProvider>();

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
            DataContextChanged += OnHitStatusApplyWindow_DataContextChanged;
            Loaded += OnHitStatusApplyWindow_Loaded;

            InitializeComponent();
        }

        private void OnHitStatusApplyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            refresh_saving_control();
            refresh_validate_button();
        }

        private void OnHitStatusApplyWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsLoaded)
            {
                refresh_saving_control();
                refresh_validate_button();
            }
            foreach (DamageTemplate dmg in data_context.OnApplyDamageList)
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
            //TODO crach tibo
            ValidateButtonControl.IsEnabled = true;
            ;
            try
            {
                if (((SavingThrow)SavingThrowControl.DataContext).SavingRoll == 0)
                    ValidateButtonControl.IsEnabled = false;
                foreach (DamageTemplate dmg in data_context.OnApplyDamageList)
                {
                    if (dmg.Damage.LastRoll == 0)
                        ValidateButtonControl.IsEnabled = false;
                }
            }
            catch (Exception) { }
        }

        private void refresh_saving_control()
        {
            if (data_context != null && (data_context.HasApplyCondition || !first_application))
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
            SavingThrow saving = ((SavingThrow)SavingThrowControl.DataContext);

            if (data_context.HasApplyCondition)
            {
                // there is a saving throw to resist the status
                Characteristic charac = Target.Characteristics.GetCharacteristic(data_context.ApplySavingCharacteristic);

                console.AddEntry($"{Target.DisplayName}", fontWeightProvider.Bold);
                console.AddEntry(" tries to resist the ");
                console.AddEntry($"{data_context.Header}", fontWeightProvider.Bold);
                console.AddEntry(" status from ");
                console.AddEntry($"{Applicant.DisplayName}", fontWeightProvider.Bold);
                console.AddEntry(". ");
                console.AddEntry($"{saving.Result}/{saving.Difficulty}", fontWeightProvider.Bold);
                console.AddEntry(" ==> ");

                if (saving.Result >= saving.Difficulty)
                {
                    //resist
                    console.AddEntry("Success\r\n", fontWeightProvider.Bold);
                    applyStatus(false);
                }
                else
                {
                    //fails
                    console.AddEntry("Failure\r\n", fontWeightProvider.Bold);
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

            SavingThrow saving = ((SavingThrow)SavingThrowControl.DataContext);
            console.AddEntry($"{Target.DisplayName}", fontWeightProvider.Bold);
            console.AddEntry(" tries again to resist the ");
            console.AddEntry($"{data_context.Header}", fontWeightProvider.Bold);
            console.AddEntry(" status from ");
            console.AddEntry($"{Applicant.DisplayName}", fontWeightProvider.Bold);
            console.AddEntry(". ");
            console.AddEntry($"{saving.Result}/{saving.Difficulty}", fontWeightProvider.Bold);
            console.AddEntry(" ==> ");

            if (saving.Result >= saving.Difficulty)
            {
                //resist
                console.AddEntry("Success\r\n", fontWeightProvider.Bold);
                data_context.Target.CustomVerboseStatusList.RemoveElement(data_context);
                //TODO check if not too dangerous
                //data_context.Unregister();
                data_context.Dispose();
            }
            else
            {
                //fails
                console.AddEntry("Failure\r\n", fontWeightProvider.Bold);
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
                foreach (DamageTemplate dmg in data_context.OnApplyDamageList)
                {
                    if (dmg.Damage.LastRoll == 0)
                        dmg.Damage.Roll();
                }
            }
        }
    }
}
