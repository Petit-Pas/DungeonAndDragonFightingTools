using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
        }

        public bool IsPressed
        {
            get
            {
                return _isPressed;
            }
            set
            {
                _isPressed = value;
                NotifyPropertyChanged();
            }
        }
        private bool _isPressed = false;

        private void ButtonCardControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsPressed = false;
        }

        private void ButtonCardControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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

        public Brush ClickBorderColor
        {
            get { return (Brush)this.GetValue(ClickBorderColorProperty); }
            set { this.SetValue(ClickBorderColorProperty, value); }
        }
        private static readonly DependencyProperty ClickBorderColorProperty = DependencyProperty.Register(
                nameof(ClickBorderColor),
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

        public Brush HoverBorderColor
        {
            get { return (Brush)this.GetValue(HoverBorderColorProperty); }
            set { this.SetValue(HoverBorderColorProperty, value); }
        }
        private static readonly DependencyProperty HoverBorderColorProperty = DependencyProperty.Register(
                nameof(HoverBorderColor),
                typeof(Brush),
                typeof(ButtonCardControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["Indigo"])
            );

        #endregion Hover

    }
}
