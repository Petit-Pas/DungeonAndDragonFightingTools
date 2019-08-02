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
    /// Logique d'interaction pour IntTextBox.xaml
    /// </summary>
    public partial class IntTextBox : UserControl, IFocusable, IValidable
    {
        /// <summary>
        ///     Ctor
        /// </summary>
        public IntTextBox()
        {
            InitializeComponent();
            Loaded += IntTextBox_Loaded;
        }

        private void IntTextBox_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        ///     The Binded Property (on Path Value)
        /// </summary>
        public String PropertyPath
        {
            get { return (String)GetValue(PropertyPathProperty); }
            set { SetValue(PropertyPathProperty, value); }
        }

        /// <summary>
        ///     DependencyProperty for the Binded Property
        /// </summary>
        public static readonly DependencyProperty PropertyPathProperty =
            DependencyProperty.Register(nameof(PropertyPath), typeof(String),
                typeof(IntTextBox),
                new FrameworkPropertyMetadata(PropertyPath_PropertyChanged));

        /// <summary>
        ///     Event handler for 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        protected static void PropertyPath_PropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var ctl = d as IntTextBox;

            Binding binding = new Binding(ctl.PropertyPath)
            {
                ValidationRules = { new IntRule() },

                //  Optional. With this, the bound property will be updated and validation 
                //  will be applied on every keystroke. 
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            ctl.IntBox.SetBinding(TextBox.TextProperty, binding);
        }

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
                System.Windows.Input.KeyEventArgs args = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, Key.Tab);
                args.RoutedEvent = Keyboard.KeyDownEvent;
                InputManager.Current.ProcessInput(args);
            }
        }
    }
}
