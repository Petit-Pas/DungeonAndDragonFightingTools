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
    /// Interaction logic for RangedIntBox.xaml
    /// </summary>
    public partial class RangedIntBox : UserControl, IValidable, INotifyPropertyChanged

    {
        public RangedIntBox()
        {
            InitializeComponent();
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

        public int PropertyMax
        {
            get { return (int)GetValue(PropertyMaxProperty); }
            set { SetValue(PropertyMaxProperty, value); }
        }

        public static readonly DependencyProperty PropertyMaxProperty =
            DependencyProperty.Register(nameof(PropertyMax), typeof(int),
                typeof(RangedIntBox));

        public int PropertyMin
        {
            get { return (int)GetValue(PropertyMinProperty); }
            set { SetValue(PropertyMinProperty, value); }
        }
        
        public static readonly DependencyProperty PropertyMinProperty =
            DependencyProperty.Register(nameof(PropertyMin), typeof(int),
                typeof(RangedIntBox));

        public String PropertyPath
        {
            get { return (String)GetValue(PropertyPathProperty); }
            set { SetValue(PropertyPathProperty, value); }
        }

        public static readonly DependencyProperty PropertyPathProperty =
            DependencyProperty.Register(nameof(PropertyPath), typeof(String),
                typeof(RangedIntBox),
                new FrameworkPropertyMetadata(PropertyPath_PropertyChanged));

        protected static void PropertyPath_PropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var ctl = d as RangedIntBox;

            Console.WriteLine("{2}: Max is {0}, Min is {1}", ctl.PropertyMax, ctl.PropertyMin, ctl.Name);
            var binding = new Binding(ctl.PropertyPath)
            {
                ValidationRules = { new RangedIntRule { Max=ctl.PropertyMax, Min=ctl.PropertyMin} },

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

        private void IntBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                System.Windows.Input.KeyEventArgs args = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, Key.Tab);
                args.RoutedEvent = Keyboard.KeyDownEvent;
                InputManager.Current.ProcessInput(args);
            }
        }

        private void IntBox_GotFocus(object sender, RoutedEventArgs e)
        {
            IntBox.SelectAll();
        }
    }
}
