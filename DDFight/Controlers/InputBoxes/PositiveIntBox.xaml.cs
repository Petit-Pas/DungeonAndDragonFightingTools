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
    /// Logique d'interaction pour PositiveIntTextBox.xaml
    /// </summary>
    public partial class PositiveIntTextBox : UserControl, IFocusable, IValidable
    {
        public PositiveIntTextBox()
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
                typeof(PositiveIntTextBox),
                new FrameworkPropertyMetadata(PropertyPath_PropertyChanged));

        protected static void PropertyPath_PropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var ctl = d as PositiveIntTextBox;

            var binding = new Binding(ctl.PropertyPath)
            {
                ValidationRules = { new PositiveIntRule() },

                //  Optional. With this, the bound property will be updated and validation 
                //  will be applied on every keystroke. 
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            ctl.IntBox.SetBinding(TextBox.TextProperty, binding);
        }

        public bool IsValid()
        {
            return !Validation.GetHasError(IntBox);
        }

        public void SetFocus()
        {
            IntBox.Focus();
        }

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
