using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfToolsLibrary.ValidationRules;

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

        public int Integer
        {
            get { return (int)this.GetValue(IntegerProperty); }
            set { this.SetValue(IntegerProperty, value); }
        }
        public static readonly DependencyProperty IntegerProperty = DependencyProperty.Register(
            "Integer", typeof(int), typeof(RangedIntBox),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(onIntegerPropertyChanged)));

        protected virtual void integer_property_updated() 
        { 
        }

        private static void onIntegerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangedIntBox control = d as RangedIntBox;
            if (control != null)
            {
                control.integer_property_updated();
            }
        }

        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register(nameof(Max), typeof(int),
                typeof(RangedIntBox));

        public int Min
        {
            get { return (int)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }
        
        public static readonly DependencyProperty MinProperty =
            DependencyProperty.Register(nameof(Min), typeof(int),
                typeof(RangedIntBox));

        #region IIsValidable

        /// <summary>
        ///     Tells wether the validation Rule is in error or not 
        ///     /!\ The input event triggers before the check, so we can't just check the Error by hand after each input
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid()
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
