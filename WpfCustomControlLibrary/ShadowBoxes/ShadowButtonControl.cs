using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfCustomControlLibrary.Animations.Extensions;
using WpfCustomControlLibrary.Animations;
using System.Windows.Input;
using WpfToolsLibrary.Extensions;

namespace WpfCustomControlLibrary.ShadowBoxes
{
    public class ShadowButtonControl : Button
    {
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/ShadowBoxes/ShadowButtonStyle.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style style = styleDict["ShadowButtonStyle"] as Style;

        private BaseDoubleShadowBoxControl shadowBox
        {
            get => this.Template.FindName("ContentPresenterContainer", this) as BaseDoubleShadowBoxControl;
        }

        public ShadowButtonControl() : base()
        {
            if (this.Style == null && style != null)
                this.Style = style;
            MouseEnter += ShadowButtonControl_MouseEnter;
            MouseLeave += ShadowButtonControl_MouseLeave;
            GotFocus += ShadowButtonControl_GotFocus;
            LostFocus += ShadowButtonControl_LostFocus;
            IsEnabledChanged += ShadowButtonControl_IsEnabledChanged;

            BaseColor = (SolidColorBrush)System.Windows.Application.Current.Resources["ButtonBaseColor"];
            HoverColor = (SolidColorBrush)System.Windows.Application.Current.Resources["ButtonHoverColor"];
            ClickedColor = (SolidColorBrush)System.Windows.Application.Current.Resources["ButtonClickedColor"];

            Foreground = (SolidColorBrush)System.Windows.Application.Current.Resources["Light"];
            Loaded += ShadowButtonControl_Loaded;
        }

        private void ShadowButtonControl_Loaded(object sender, RoutedEventArgs e)
        {
            ShadowButtonControl_IsEnabledChanged(this, new DependencyPropertyChangedEventArgs(Button.IsEnabledProperty, !IsEnabled, IsEnabled));
        }

        private void ShadowButtonControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
                animate_enabled();
            else
                animate_disabled();
        }

        private void animate_enabled()
        {
            AnimationLibrary.AnimateDouble(this, Button.OpacityProperty, 1, TimeSpan.FromSeconds(0.25));
        }

        private void animate_disabled()
        {
            AnimationLibrary.AnimateDouble(this, Button.OpacityProperty, 0.3, TimeSpan.FromSeconds(0.25));
        }

        protected override void OnIsPressedChanged(System.Windows.DependencyPropertyChangedEventArgs e)
        {
            BaseDoubleShadowBoxControl ShadowBox = shadowBox;
            if (ShadowBox != null)
            {
                if ((bool)e.NewValue == true)
                {
                    shadowBox.OuterShadowVisibility = Visibility.Collapsed;
                    shadowBox.InnerShadowVisibility = Visibility.Visible;
                    shadowBox.BackgroundColor = ClickedColor;
                }
                else
                {
                    shadowBox.OuterShadowVisibility = Visibility.Visible;
                    shadowBox.InnerShadowVisibility = Visibility.Collapsed;
                    shadowBox.BackgroundColor = HoverColor;
                }
            }
        }

        private void ShadowButtonControl_LostFocus(object sender, RoutedEventArgs e)
        {
            animate_normal();
        }

        private void ShadowButtonControl_GotFocus(object sender, RoutedEventArgs e)
        {
            animate_hover();
        }

        private void ShadowButtonControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (IsPressed)
            {
                // button was pressed with spacebar
            }
            else
            {
                if (!IsFocused)
                animate_normal();
            }
        }

        private void ShadowButtonControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (IsPressed)
            {
                // button was pressed with spacebar
            }
            else
            {
                animate_hover();
            }
        }

        private void animate_hover()
        {
            BaseDoubleShadowBoxControl ShadowBox = shadowBox;
            if (ShadowBox != null)
            {
                AnimationLibrary.AnimateBrush(
                    ((x) => ShadowBox.BackgroundColor = x),
                    ((SolidColorBrush)ShadowBox.BackgroundColor).Color,
                    HoverColor.Color,
                    TimeSpan.FromSeconds(0.15));
            }
        }

        private void animate_normal()
        {
            BaseDoubleShadowBoxControl ShadowBox = shadowBox;
            if (ShadowBox != null)
            {
                AnimationLibrary.AnimateBrush(
                    ((x) => ShadowBox.BackgroundColor = x),
                    ((SolidColorBrush)ShadowBox.BackgroundColor).Color,
                    BaseColor.Color,
                    TimeSpan.FromSeconds(0.15));
            }
        }

        public SolidColorBrush BaseColor
        {
            get { return (SolidColorBrush)this.GetValue(BaseColorProperty); }
            set { this.SetValue(BaseColorProperty, value); }
        }
        private static readonly DependencyProperty BaseColorProperty = DependencyProperty.Register(
            nameof(BaseColor),
            typeof(SolidColorBrush),
            typeof(ShadowButtonControl),
            new PropertyMetadata());

        public SolidColorBrush HoverColor
        {
            get { return (SolidColorBrush)this.GetValue(HoverColorProperty); }
            set { this.SetValue(HoverColorProperty, value); }
        }
        private static readonly DependencyProperty HoverColorProperty = DependencyProperty.Register(
            nameof(HoverColor),
            typeof(SolidColorBrush),
            typeof(ShadowButtonControl),
            new PropertyMetadata());

        public SolidColorBrush ClickedColor
        {
            get { return (SolidColorBrush)this.GetValue(ClickedColorProperty); }
            set { this.SetValue(ClickedColorProperty, value); }
        }
        private static readonly DependencyProperty ClickedColorProperty = DependencyProperty.Register(
            nameof(ClickedColor),
            typeof(SolidColorBrush),
            typeof(ShadowButtonControl),
            new PropertyMetadata());
    }
}
