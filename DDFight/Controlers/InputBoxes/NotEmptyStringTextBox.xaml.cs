﻿using DDFight.Tools;
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

        public String PropertyPath
        {
            get { return (String)GetValue(PropertyPathProperty); }
            set { SetValue(PropertyPathProperty, value); }
        }

        public static readonly DependencyProperty PropertyPathProperty =
            DependencyProperty.Register(nameof(PropertyPath), typeof(String),
                typeof(NotEmptyStringTextBox),
                new FrameworkPropertyMetadata(PropertyPath_PropertyChanged));

        protected static void PropertyPath_PropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var ctl = d as NotEmptyStringTextBox;

            var binding = new Binding(ctl.PropertyPath)
            {
                ValidationRules = { new NotEmptyStringRule() },

                //  Optional. With this, the bound property will be updated and validation 
                //  will be applied on every keystroke. 
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            ctl.StringBox.SetBinding(TextBox.TextProperty, binding);

        }

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
                System.Windows.Input.KeyEventArgs args = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, Key.Tab);
                args.RoutedEvent = Keyboard.KeyDownEvent;
                InputManager.Current.ProcessInput(args);
            }
        }
    }
}
