using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Characteristics;
using DDFight.Game.Dices;
using DDFight.Game.Dices.SavingThrow;
using DDFight.Game.Entities;
using DDFight.Tools;
using DDFight.Tools.UXShortcuts;
using DnDToolsLibrary.Dice;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Interaction logic for ConcentrationCheckWindow.xaml
    /// </summary>
    public partial class ConcentrationCheckWindow : Window, INotifyPropertyChanged
    {
        private PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }

        public ConcentrationCheckWindow()
        {
            InitializeComponent();
        }

        public SituationalAdvantageModifiers SituationalAdvantageModifiers
        {
            get => _situationalAdvantageModifiers;
            set
            {
                _situationalAdvantageModifiers = value;
                NotifyPropertyChanged();
            }
        }
        private SituationalAdvantageModifiers _situationalAdvantageModifiers = new SituationalAdvantageModifiers();

        public SituationalSavingThrowModifier SituationalSavingThrowModifier
        {
            get => _situationalSavingThrowModifier;
            set 
            {
                _situationalSavingThrowModifier = value;
                NotifyPropertyChanged();
            }
        }
        private SituationalSavingThrowModifier _situationalSavingThrowModifier = new SituationalSavingThrowModifier();

        public int Roll
        {
            get => _roll;
            set
            {
                _roll = value;
                NotifyPropertyChanged();
                if (value != 0)
                {
                    ButtonControl.Content = "Validate";
                }
                else
                {
                    ButtonControl.Content = "Roll";
                }
            }
        }
        private int _roll = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.AreAllChildrenValid()))
            {
                ErrorMessageControl.Visibility = Visibility.Visible;
                return;
            }
            ErrorMessageControl.Visibility = Visibility.Collapsed;
            if (Roll == 0)
            {
                roll_dice();
            }
            else
            {
                check_concentration();
                this.Close();
            }
        }

        public bool Success = false;

        private void check_concentration()
        {
            Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;
            paragraph.Inlines.Add(Extensions.BuildRun(data_context.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
            paragraph.Inlines.Add(Extensions.BuildRun(": Concentration check: ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(Extensions.BuildRun((Roll + SituationalSavingThrowModifier.Modifier + data_context.Characteristics.GetSavingModifier(CharacteristicsEnum.Constitution)).ToString() + "/10: ", 
                (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));

            if (Roll + SituationalSavingThrowModifier.Modifier + data_context.Characteristics.GetSavingModifier(CharacteristicsEnum.Constitution) >= 10)
            {
                paragraph.Inlines.Add(Extensions.BuildRun("still focused.", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                Success = true;
            }
            else
            {
                paragraph.Inlines.Add(Extensions.BuildRun("lost Focus.", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            }
            paragraph.Inlines.Add(Extensions.BuildRun("\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
        }

        private void roll_dice()
        {
            if (Roll == 0)
                Roll = DiceRoll.Roll("1d20", SituationalAdvantageModifiers.SituationalAdvantage, SituationalAdvantageModifiers.SituationalDisadvantage);
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

        private void CurrentWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (RollableWindowTool.IsRollControlPressed(e))
            {
                roll_dice();
                e.Handled = true;
            }
        }
    }
}
