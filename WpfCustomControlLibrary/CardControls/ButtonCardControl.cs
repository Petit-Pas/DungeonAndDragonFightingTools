using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfCustomControlLibrary.Animations.Extensions;

namespace WpfCustomControlLibrary.CardControls
{
    public class ButtonCardControl : UpCardControl, INotifyPropertyChanged
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
            Source=new Uri("/WpfCustomControlLibrary;component/CardControls/ButtonCardStyle.xaml", UriKind.RelativeOrAbsolute) 
        };
        private static readonly Style style = styleDict["ButtonCardStyle"] as Style;

        public ButtonCardControl() 
            : base ()
        {
            if (style != null)
                this.Style = style;
            this.MouseLeftButtonDown += ButtonCardControl_MouseLeftButtonDown;
            this.MouseLeftButtonUp += ButtonCardControl_MouseLeftButtonUp;
            this.MouseEnter += ButtonCardControl_MouseEnter;
            this.MouseLeave += ButtonCardControl_MouseLeave;
            this.KeyDown += ButtonCardControl_KeyDown;
            this.GotFocus += ButtonCardControl_GotFocus;
            this.LostFocus += ButtonCardControl_LostFocus;
        }

        private void ButtonCardControl_LostFocus(object sender, RoutedEventArgs e)
        {
            animate_inactive();
        }

        private void ButtonCardControl_GotFocus(object sender, RoutedEventArgs e)
        {
            animate_active();
        }

        private void ButtonCardControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
                this.OnClick();
        }

        private void animate_active()
        {
            Border border = this.Template.FindName("ButtonCard_BorderControl", this) as Border;
            border.AnimateBackground(
                (Color)Application.Current.Resources["RawLightestGray"],
                (Color)Application.Current.Resources["RawIndigo"],
                TimeSpan.FromSeconds(0.15));
        }

        private void animate_inactive()
        {
            Border border = this.Template.FindName("ButtonCard_BorderControl", this) as Border;
            border.AnimateBackground(
                (Color)Application.Current.Resources["RawIndigo"],
                (Color)Application.Current.Resources["RawLightestGray"],
                TimeSpan.FromSeconds(0.15));
        }

        private void ButtonCardControl_MouseLeave(object sender, MouseEventArgs e)
        {
            animate_inactive();
        }

        private void ButtonCardControl_MouseEnter(object sender, MouseEventArgs e)
        {
            animate_active();
        }

        public bool IsPressed
        {
            get => _isPressed;
            set
            {
                _isPressed = value;
                NotifyPropertyChanged();
            }
        }
        private bool _isPressed = false;

        private void ButtonCardControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Border border = this.Template.FindName("ButtonCard_BorderControl", this) as Border;
            border.AnimateBackground(
                (Color)Application.Current.Resources["RawLightIndigo"],
                (Color)Application.Current.Resources["RawIndigo"],
                TimeSpan.FromSeconds(0.05));
            IsPressed = false;
        }

        private void ButtonCardControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border = this.Template.FindName("ButtonCard_BorderControl", this) as Border;
            border.AnimateBackground(
                (Color)Application.Current.Resources["RawIndigo"],
                (Color)Application.Current.Resources["RawLightIndigo"],
                TimeSpan.FromSeconds(0.05));
            IsPressed = true;
        }

        public static RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ButtonCardControl));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }
        protected virtual void OnClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent, this);

            RaiseEvent(args);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            OnClick();
        }

        #region Click

        public Brush ClickBackgroundColor
        {
            get { return (Brush)this.GetValue(ClickBackgroundColorProperty); }
            set { this.SetValue(ClickBackgroundColorProperty, value); }
        }
        private static readonly DependencyProperty ClickBackgroundColorProperty = DependencyProperty.Register(
                nameof(ClickBackgroundColor),
                typeof(Brush),
                typeof(ButtonCardControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["LightIndigo"])
            );

        #endregion Click

        #region Hover

        public Brush HoverBackgroundColor
        {
            get { return (Brush)this.GetValue(HoverBackgroundColorProperty); }
            set { this.SetValue(HoverBackgroundColorProperty, value); }
        }
        private static readonly DependencyProperty HoverBackgroundColorProperty = DependencyProperty.Register(
                nameof(HoverBackgroundColor),
                typeof(Brush),
                typeof(ButtonCardControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["Indigo"])
            );

        #endregion Hover

    }
}
