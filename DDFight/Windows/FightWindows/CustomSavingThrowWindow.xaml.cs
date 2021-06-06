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
using System.Windows.Input;
using System.Windows.Media;
using WpfToolsLibrary.ConsoleTools;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Interaction logic for CustomSavingThrowWindow.xaml
    /// </summary>
    public partial class CustomSavingThrowWindow : Window, INotifyPropertyChanged
    {
        private ICustomConsole console = DIContainer.GetImplementation<ICustomConsole>();
        private IFontWeightProvider fontWeightProvider = DIContainer.GetImplementation<IFontWeightProvider>();

        private PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }

        public CustomSavingThrowWindow()
        {
            InitializeComponent();
        }

        public CharacteristicsEnum Characteristic
        {
            get => _characteristic;
            set
            {
                _characteristic = value;
                NotifyPropertyChanged();
            }
        }
        private CharacteristicsEnum _characteristic = default;

        public SituationalAdvantageModifiers AdvantageModifiers
        {
            get => _advantageModifiers;
            set
            {
                _advantageModifiers = value;
                NotifyPropertyChanged();
            }
        }
        private SituationalAdvantageModifiers _advantageModifiers = new SituationalAdvantageModifiers();

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

        public int Difficulty
        {
            get => _difficulty;
            set
            {
                _difficulty = value;
                NotifyPropertyChanged();
            }
        }
        private int _difficulty = 10;

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
                do_saving_throw();
                this.Close();
            }
        }

        public bool Success = false;

        private void do_saving_throw()
        {
            console.AddEntry($"{data_context.DisplayName}", fontWeightProvider.Bold);
            console.AddEntry(": Saving Throw ", fontWeightProvider.Bold);
            console.AddEntry($"{Characteristic} ", fontWeightProvider.Bold);
            console.AddEntry($"{Roll + SituationalSavingThrowModifier.Modifier + data_context.Characteristics.GetSavingModifier(Characteristic)}/{Difficulty}\n", fontWeightProvider.Bold);
            if (Difficulty <= (Roll + SituationalSavingThrowModifier.Modifier + data_context.Characteristics.GetSavingModifier(Characteristic)))
                Success = true;
        }

        private void roll_dice()
        {
            if (Roll == 0)
                Roll = DiceRoll.Roll("1d20", AdvantageModifiers.SituationalAdvantage, AdvantageModifiers.SituationalDisadvantage);
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.IsRollControlPressed(e))
            {
                roll_dice();
                e.Handled = true;
            }
        }
    }
}
