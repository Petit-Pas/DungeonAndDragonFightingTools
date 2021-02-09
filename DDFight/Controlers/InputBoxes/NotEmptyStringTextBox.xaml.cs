using DDFight.Tools;
using DDFight.ValidationRules;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DDFight.Controlers.InputBoxes
{
    /// <summary>
    /// Logique d'interaction pour NotEmptyStringTextBox.xaml
    /// </summary>
    public partial class NotEmptyStringTextBox : UserControl, IFocusable, IValidable
    {
        public NotEmptyStringTextBox()
        {
            InitializeComponent();
        }

        public string String
        {
            get { return (string)this.GetValue(StringProperty); }
            set { this.SetValue(StringProperty, value); }
        }
        public static DependencyProperty StringProperty = DependencyProperty.Register(
            "String", typeof(string), typeof(NotEmptyStringTextBox),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #region IIsValidable
        
        /// <summary>
        ///     Tells wether the validation Rule is in error or not 
        ///     /!\ The input event triggers before the check, so we can't just check the Error by hand after each input
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return !Validation.GetHasError(StringBox);
        }

        #endregion

        public void SetFocus()
        {
            StringBox.Focus();
        }

        private void StringBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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

        private void StringBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.StringBox.SelectAll();
        }
    }
}
