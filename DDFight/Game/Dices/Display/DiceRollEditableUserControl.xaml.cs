using DDFight.Converters;
using DDFight.ValidationRules;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DDFight.Controlers.Game.Dices
{
    /// <summary>
    /// Interaction logic for DiceRollUserControl.xaml
    /// </summary>
    public partial class DiceRollEditableUserControl : UserControl, IValidable
    {
        public DiceRollEditableUserControl()
        {
            InitializeComponent();
        }

        public String PropertyPath
        {
            get { return (String)GetValue(PropertyPathProperty); }
            set { SetValue(PropertyPathProperty, value); }
        }

        public static readonly DependencyProperty PropertyPathProperty =
            DependencyProperty.Register(nameof(PropertyPath), typeof(String),
                typeof(DiceRollEditableUserControl),
                new FrameworkPropertyMetadata(PropertyPath_PropertyChanged));

        protected static void PropertyPath_PropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var ctl = d as DiceRollEditableUserControl;

            var binding = new Binding(ctl.PropertyPath)
            {
                ValidationRules = { new DiceRollRule() },

                //  Optional. With this, the bound property will be updated and validation 
                //  will be applied on every keystroke. 
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = new DiceRollToStringConverter(),
            };
            Console.WriteLine("COCHON binding, PropertyPath:" + ctl.PropertyPath.ToString() + " " + ctl.DataContext?.ToString());
            ctl.textBox.SetBinding(TextBox.TextProperty, binding);

        }

        #region IIsValidable

        /// <summary>
        ///     Tells wether the validation Rule is in error or not 
        ///     /!\ The input event triggers before the check, so we can't just check the Error by hand after each input
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return !Validation.GetHasError(textBox);
        }

        #endregion

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                System.Windows.Input.KeyEventArgs args = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, Key.Tab);
                args.RoutedEvent = Keyboard.KeyDownEvent;
                InputManager.Current.ProcessInput(args);
            }
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textBox.SelectAll();
        }
    }
}
