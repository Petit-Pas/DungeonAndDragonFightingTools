using DDFight.ValidationRules;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DDFight.Controlers.InputBoxes
{
    /// <summary>
    /// Logique d'interaction pour NotEmptyStringTextBox.xaml
    /// </summary>
    public partial class NotEmptyStringTextBox : UserControl
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

        public bool IsValid()
        {
            return !Validation.GetHasError(StringBox);
        }

        public void SetFocus()
        {
            StringBox.Focus();
        }
    }
}
