using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using DDFight.Game.Dices.SavingThrow;
using DnDToolsLibrary.Attacks;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using WpfToolsLibrary.ConsoleTools;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Interaction logic for ConcentrationCheckWindow.xaml
    /// </summary>
    public partial class ConcentrationCheckWindow : Window, INotifyPropertyChanged
    {
        private ICustomConsole console = DIContainer.GetImplementation<ICustomConsole>();
        private IFontWeightProvider fontWeightProvider = DIContainer.GetImplementation<IFontWeightProvider>();

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
            console.AddEntry($"{data_context.DisplayName}", fontWeightProvider.SemiBold);
            console.AddEntry(": Concentration check: ");
            console.AddEntry($"{Roll + SituationalSavingThrowModifier.Modifier + data_context.Characteristics.GetSavingModifier(CharacteristicsEnum.Constitution)}/10: ", 
                fontWeightProvider.SemiBold);

            if (Roll + SituationalSavingThrowModifier.Modifier + data_context.Characteristics.GetSavingModifier(CharacteristicsEnum.Constitution) >= 10)
            {
                console.AddEntry("still focused\r\n");
                Success = true;
            }
            else
            {
                console.AddEntry("lost Focus.\r\n");
            }
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
            if (this.IsRollControlPressed(e))
            {
                roll_dice();
                e.Handled = true;
            }
        }
    }
}
