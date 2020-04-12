using DDFight.Game.Characteristics;
using DDFight.Game.Dices.SavingThrow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public OnHitStatusApplyWindow(PlayableEntity applicant, PlayableEntity target)
        {
            Target = target;
            Applicant = applicant;
            
            InitializeComponent();
            DataContextChanged += OnHitStatusApplyWindow_DataContextChanged;
        }

        private void OnHitStatusApplyWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            refresh_saving_control();
        }

        private void OnHitStatusApplyWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (((SavingThrow)SavingThrowControl.DataContext).SavingRoll == 0)
                ValidateButtonControl.Content = "Automatic Roll";
            else
                ValidateButtonControl.Content = "Validate";
        }

        private void refresh_saving_control()
        {
            if (data_context.HasApplyCondition)
            {
                ValidateButtonControl.Content = "Automatic Roll";
                SavingThrowControl.DataContext = data_context.GetSavingThrow(Target);
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

        private void validateOnHit()
        {
            Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            if (data_context.HasApplyCondition)
            {
                // there is a saving throw to resist the status
                Characteristic charac = Target.Characteristics.GetCharacteristic(data_context.ApplySavingCharacteristic);
                int total = ((SavingThrow)SavingThrowControl.DataContext).SavingRoll + ((SavingThrow)SavingThrowControl.DataContext).Modifier + charac.Modifier + (int)(charac.Mastery ? Target.Characteristics.MasteryBonus : 0);

                paragraph.Inlines.Add(Extensions.BuildRun(Target.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" tries to resist ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(data_context.Header, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" from ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(Applicant.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" ==> ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(total.ToString() + "/" + data_context.ApplySavingDifficulty.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
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
                    Target.CustomVerboseStatusList.List.Add(data_context);
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
                Target.CustomVerboseStatusList.List.Add(data_context);
            }
        }

        private void ValidateButtonControl_Click(object sender, RoutedEventArgs e)
        {
            if ((string)ValidateButtonControl.Content == "Validate")
            {
                validateOnHit();
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
