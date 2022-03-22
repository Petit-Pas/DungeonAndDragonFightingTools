using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfCustomControlLibrary.Animations;

namespace WpfCustomControlLibrary.ComboBoxes
{
    public class ComboBoxControl : ComboBox
    {
    
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/ComboBoxes/ComboBoxStyles.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style defaultStyle = styleDict["ComboBoxStyle"] as Style;
        private static readonly Style itemStyle = styleDict["ComboBoxItemStyle"] as Style;

        public ComboBoxControl() : base()
        {
            if (this.Style == null && defaultStyle != null)
                this.Style = defaultStyle;
            if (this.ItemContainerStyle == null && itemStyle != null)
                this.ItemContainerStyle = itemStyle;
            this.FocusVisualStyle = null;
            this.KeyDown += ComboBoxControl_KeyDown;
            this.MouseEnter += ComboBoxControl_MouseEnter;
            this.MouseLeave += ComboBoxControl_MouseLeave;
            this.GotFocus += ComboBoxControl_GotFocus;
            this.LostFocus += ComboBoxControl_LostFocus;
            BaseColor = (SolidColorBrush)System.Windows.Application.Current.Resources["ButtonBaseColor"];
            ActiveColor = (SolidColorBrush)System.Windows.Application.Current.Resources["ComboBoxHoverColor"];
            Background = BaseColor;
            DropDownClosed += ComboBoxControl_DropDownClosed;
            DropDownOpened += ComboBoxControl_DropDownOpened;
        }

        // We need to use a custom value, because the actual one fires after a MouseEnter/MouseLeave cycle used to click on it by WPF
        private bool innerIsDropDownOpen = false;

        private void ComboBoxControl_DropDownOpened(object sender, EventArgs e)
        {
            innerIsDropDownOpen = true;
        }

        private void ComboBoxControl_DropDownClosed(object sender, EventArgs e)
        {
            innerIsDropDownOpen = false;
        }

        private void ComboBoxControl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!innerIsDropDownOpen) // event is fired every time you mouseover a new element => avoid blinking
            {
                animate_normal();
            }
        }

        private void ComboBoxControl_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!innerIsDropDownOpen) // event is fired every time you mouseover a new element => avoid blinking
            {
                animate_active();
            }
        }

        private void ComboBoxControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.IsFocused)
            {
                // DropDown menu was closed
            }
            else
            {
                animate_normal();
            }
        }

        private void ComboBoxControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.IsFocused)
            {
                // DropDown menu was open
            }
            else
            {
                animate_active();
            }
        }

        private void animate_active()
        {
            var test = ((SolidColorBrush)this.Background).Color;
            AnimationLibrary.AnimateBrush(
                ((x) => this.Background = x),
                ((SolidColorBrush)this.Background).Color,
                ActiveColor.Color,
                TimeSpan.FromSeconds(0.15));
        }

        private void animate_normal()
        {
            AnimationLibrary.AnimateBrush(
                ((x) => this.Background = x),
                ((SolidColorBrush)this.Background).Color,
                BaseColor.Color,
                TimeSpan.FromSeconds(0.15));
        }

        private void ComboBoxControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Enter)
                this.IsDropDownOpen = true;
        }

        public SolidColorBrush BaseColor
        {
            get { return (SolidColorBrush)this.GetValue(BaseColorProperty); }
            set { this.SetValue(BaseColorProperty, value); }
        }
        private static readonly DependencyProperty BaseColorProperty = DependencyProperty.Register(
            nameof(BaseColor),
            typeof(SolidColorBrush),
            typeof(ComboBoxControl),
            new PropertyMetadata());
        
        public SolidColorBrush ActiveColor
        {
            get { return (SolidColorBrush)this.GetValue(HoverColorProperty); }
            set { this.SetValue(HoverColorProperty, value); }
        }
        private static readonly DependencyProperty HoverColorProperty = DependencyProperty.Register(
            nameof(ActiveColor),
            typeof(SolidColorBrush),
            typeof(ComboBoxControl),
            new PropertyMetadata());
    }
}
