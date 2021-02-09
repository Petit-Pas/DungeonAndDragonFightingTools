using DDFight.Tools;
using DDFight.ValidationRules;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DDFight.Controlers.InputBoxes
{
    /// <summary>
    ///     Logique d'interaction pour IntTextBox.xaml
    /// </summary>
    public partial class IntTextBox : UserControl, IFocusable, IValidable
    {
        /// <summary>
        ///     Ctor
        /// </summary>
        public IntTextBox()
        {
            InitializeComponent();
        }

        public int Integer
        {
            get { return (int)this.GetValue(IntegerProperty); }
            set { this.SetValue(IntegerProperty, value); }
        }

        public static readonly DependencyProperty IntegerProperty = DependencyProperty.Register(
            "Integer", typeof(int), typeof(IntTextBox),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #region IIsValidable

        /// <summary>
        ///     Tells wether the validation Rule is in error or not 
        ///     /!\ The input event triggers before the check, so we can't just check the Error by hand after each input
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return !Validation.GetHasError(IntBox);
        }

        #endregion

        public void SetFocus()
        {
            IntBox.Focus();
        }

        /// <summary>
        ///     Simulates a Tab input when the user presses the Enter Key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
