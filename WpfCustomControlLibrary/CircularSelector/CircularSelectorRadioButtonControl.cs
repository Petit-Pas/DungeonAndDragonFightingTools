using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WpfCustomControlLibrary.Animations;

namespace WpfCustomControlLibrary.CircularSelector
{
    public class CircularSelectorRadioButtonControl : RadioButton, INotifyPropertyChanged
    {
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

        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/CircularSelector/CircularSelectorRadioButtonStyle.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style style = styleDict["CircularSelectorRadioButtonStyle"] as Style;

        public object RadioValue
        {
            get { return (object)GetValue(RadioValueProperty); }
            set { SetValue(RadioValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RadioValue.
        // This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RadioValueProperty =
        DependencyProperty.Register(
            "RadioValue",
            typeof(object),
            typeof(CircularSelectorRadioButtonControl),
            new UIPropertyMetadata(null));

        public object RadioBinding
        {
            get { return (object)GetValue(RadioBindingProperty); }
            set { SetValue(RadioBindingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RadioBinding.
        // This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RadioBindingProperty =
        DependencyProperty.Register(
            "RadioBinding",
            typeof(object),
            typeof(CircularSelectorRadioButtonControl),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnRadioBindingChanged));

        private static void OnRadioBindingChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            CircularSelectorRadioButtonControl rb = (CircularSelectorRadioButtonControl)d;
            if (rb.RadioValue.Equals(e.NewValue))
                rb.SetCurrentValue(RadioButton.IsCheckedProperty, true);
        }

        protected override void OnChecked(RoutedEventArgs e)
        {
            base.OnChecked(e);
            SetCurrentValue(RadioBindingProperty, RadioValue);
        }


        public CircularSelectorRadioButtonControl() : base()
        {
            if (style != null)
                this.Style = style;
            this.MouseEnter += TriangleRadioButton_MouseEnter;
            this.MouseLeave += TriangleRadioButton_MouseLeave;

            GotFocus += CircularSelectorRadioButtonControl_GotFocus;
            LostFocus += CircularSelectorRadioButtonControl_LostFocus;
            Unchecked += CircularSelectorRadioButtonControl_Unchecked;
            Checked += CircularSelectorRadioButtonControl_Checked;
        }

        private void CircularSelectorRadioButtonControl_Checked(object sender, RoutedEventArgs e)
        {
            animate_selected();
        }

        private void CircularSelectorRadioButtonControl_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!IsMouseOver)
                animate_normal();
        }

        private void CircularSelectorRadioButtonControl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (false == IsChecked)
                // lost focus AND is not the one clicked
                animate_normal();
        }

        private void CircularSelectorRadioButtonControl_GotFocus(object sender, RoutedEventArgs e)
        {
            if (false == IsChecked)
                animate_hover();
        }

        private void TriangleRadioButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (IsPressed)
            {
                // button was pressed with spacebar
            }
            else
            {
                if (IsChecked == false && IsFocused == false)
                    animate_normal();
            }
        }

        private void TriangleRadioButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (IsPressed || IsChecked == true)
            {
                // button was pressed with spacebar
            }
            else
            {
                animate_hover();
            }
        }
        private Path _path
        {
            get => this.Template.FindName("ThePath", this) as Path;
        }

        private void animate_hover()    
        {
            Path path = _path;
            if (path != null)
            {
                AnimationLibrary.AnimateBrush(
                    ((x) => path.Fill = x),
                    ((SolidColorBrush)path.Fill).Color,
                    (CircularSelectorCenterControl.GetHoverColorBrush(this) as SolidColorBrush).Color,
                    TimeSpan.FromSeconds(0.15));
            }
            DoubleAnimation double_animation = new DoubleAnimation(this.Scale, 1.1, TimeSpan.FromSeconds(0.15));
            this.BeginAnimation(CircularSelectorRadioButtonControl.ScaleProperty, double_animation);
        }

        private void animate_normal()
        {
            Path path = _path;
            if (path != null)
            {
                AnimationLibrary.AnimateBrush(
                    ((x) => path.Fill = x),
                    ((SolidColorBrush)path.Fill).Color,
                    (CircularSelectorCenterControl.GetBaseColorBrush(this) as SolidColorBrush).Color,
                    TimeSpan.FromSeconds(0.15));
            }

            DoubleAnimation double_animation = new DoubleAnimation(this.Scale, 1.0, TimeSpan.FromSeconds(0.15));
            this.BeginAnimation(CircularSelectorRadioButtonControl.ScaleProperty, double_animation);
        }

        private void animate_selected()
        {
            Path path = _path;
            if (path != null)
            {
                AnimationLibrary.AnimateBrush(
                    ((x) => path.Fill = x),
                    ((SolidColorBrush)path.Fill).Color,
                    (CircularSelectorCenterControl.GetSelectedColorBrush(this) as SolidColorBrush).Color,
                    TimeSpan.FromSeconds(0.15));
            }
        }

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register(
                nameof(Angle),
                typeof(double),
                typeof(CircularSelectorRadioButtonControl),
                new FrameworkPropertyMetadata(0.0));

        public double Scale
        {
            get { return (double)this.GetValue(ScaleProperty); }
            set { this.SetValue(ScaleProperty, value); }
        }
        private static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            nameof(Scale),
            typeof(double),
            typeof(CircularSelectorRadioButtonControl),
            new PropertyMetadata(1.0));
    }
}
