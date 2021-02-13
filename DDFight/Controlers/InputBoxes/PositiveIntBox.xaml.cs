using DDFight.Tools;
using DDFight.ValidationRules;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Controlers.InputBoxes
{
    /// <summary>
    /// Logique d'interaction pour PositiveIntTextBox.xaml
    /// </summary>
    public partial class PositiveIntTextBox : UserControl, IFocusable, IValidable
    {
        public PositiveIntTextBox()
        {
            InitializeComponent();
        }

        public int Integer
        {
            get { return (int)this.GetValue(IntegerProperty); }
            set { this.SetValue(IntegerProperty, value); }
        }
        public static readonly DependencyProperty IntegerProperty = DependencyProperty.Register(
            "Integer", typeof(int), typeof(PositiveIntTextBox),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #region IIsValidable

        /// <summary>
        ///     Tells wether the validation Rule is in error or not 
        ///     /!\ The input event triggers before the check, so we can't just check the Error by hand after each input
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            bool result = !Validation.GetHasError(IntBox);
            return result;
        }

        #endregion

        public void SetFocus()
        {
            IntBox.Focus();
        }

        private void IntBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                System.Windows.Input.KeyEventArgs args = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, Key.Tab)
                {
                    RoutedEvent = Keyboard.KeyDownEvent
                };
                InputManager.Current.ProcessInput(args);
            }
        }

        private void IntBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.IntBox.SelectAll();
        }
    }
}
