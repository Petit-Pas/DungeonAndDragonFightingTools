using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfCustomControlLibrary.ShadowBoxes
{
    public class BaseShadowBoxControl : ContentControl
    {
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/ShadowBoxes/BaseShadowBoxStyle.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style style = styleDict["BaseShadowBoxStyle"] as Style;

        public BaseShadowBoxControl() : base()
        {
            if (this.Style == null && style != null)
                this.Style = style;
            Padding = new Thickness(9);
        }

        public Brush BackgroundColor
        {
            get { return (Brush)this.GetValue(BackgroundColorProperty); }
            set { this.SetValue(BackgroundColorProperty, value); }
        }
        private static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
            nameof(BackgroundColor),
            typeof(Brush),
            typeof(BaseShadowBoxControl),
            new PropertyMetadata(System.Windows.Application.Current.Resources["LightestGray"]));

        #region ShadowVisibilities

        public Visibility InnerShadowVisibility
        {
            get { return (Visibility)this.GetValue(InnerShadowVisibilityProperty); }
            set { this.SetValue(InnerShadowVisibilityProperty, value); }
        }
        private static readonly DependencyProperty InnerShadowVisibilityProperty = DependencyProperty.Register(
            nameof(InnerShadowVisibility),
            typeof(Visibility),
            typeof(BaseShadowBoxControl),
            new PropertyMetadata(Visibility.Collapsed));

        public Visibility OuterShadowVisibility
        {
            get { return (Visibility)this.GetValue(OuterShadowVisibilityProperty); }
            set { this.SetValue(OuterShadowVisibilityProperty, value); }
        }
        private static readonly DependencyProperty OuterShadowVisibilityProperty = DependencyProperty.Register(
            nameof(OuterShadowVisibility),
            typeof(Visibility),
            typeof(BaseShadowBoxControl),
            new PropertyMetadata(Visibility.Visible));

        #endregion ShadowVisibilities

        #region ShadowColors

        public Color UpperOuterShadowColor
        {
            get { return (Color)this.GetValue(UpperOuterShadowColorProperty); }
            set { this.SetValue(UpperOuterShadowColorProperty, value); }
        }
        private static readonly DependencyProperty UpperOuterShadowColorProperty = DependencyProperty.Register(
            nameof(UpperOuterShadowColor),
            typeof(Color),
            typeof(BaseShadowBoxControl),
            new PropertyMetadata((Color)ColorConverter.ConvertFromString("#D7D7D7")));

        public Color LowerOuterShadowColor
        {
            get { return (Color)this.GetValue(LowerOuterShadowColorProperty); }
            set { this.SetValue(LowerOuterShadowColorProperty, value); }
        }
        private static readonly DependencyProperty LowerOuterShadowColorProperty = DependencyProperty.Register(
            nameof(LowerOuterShadowColor),
            typeof(Color),
            typeof(BaseShadowBoxControl),
            new PropertyMetadata(System.Windows.Application.Current.Resources["RawIndigo"]));

        public Color UpperInnerShadowColor
        {
            get { return (Color)this.GetValue(UpperInnerShadowColorProperty); }
            set { this.SetValue(UpperInnerShadowColorProperty, value); }
        }
        private static readonly DependencyProperty UpperInnerShadowColorProperty = DependencyProperty.Register(
            nameof(UpperInnerShadowColor),
            typeof(Color),
            typeof(BaseShadowBoxControl),
            new PropertyMetadata(Colors.Black));

        public Color LowerInnerShadowColor
        {
            get { return (Color)this.GetValue(LowerInnerShadowColorProperty); }
            set { this.SetValue(LowerInnerShadowColorProperty, value); }
        }
        private static readonly DependencyProperty LowerInnerShadowColorProperty = DependencyProperty.Register(
            nameof(LowerInnerShadowColor),
            typeof(Color),
            typeof(BaseShadowBoxControl),
            new PropertyMetadata((Color)ColorConverter.ConvertFromString("#D7D7D7")));

        #endregion ShadowColors

        public CornerRadius BorderCornerRadius
        {
            get { return (CornerRadius)this.GetValue(BorderCornerRadiusProperty); }
            set { this.SetValue(BorderCornerRadiusProperty, value); }
        }
        private static readonly DependencyProperty BorderCornerRadiusProperty = DependencyProperty.Register(
            nameof(BorderCornerRadius),
            typeof(CornerRadius),
            typeof(BaseShadowBoxControl),
            new PropertyMetadata(new CornerRadius(15)));

        #region BlurRadii

        public double LowerInnerShadowBlurRadius
        {
            get { return (double)this.GetValue(LowerInnerShadowBlurRadiusProperty); }
            set { this.SetValue(LowerInnerShadowBlurRadiusProperty, value); }
        }
        private static readonly DependencyProperty LowerInnerShadowBlurRadiusProperty = DependencyProperty.Register(
            nameof(LowerInnerShadowBlurRadius),
            typeof(double),
            typeof(BaseShadowBoxControl),
            new PropertyMetadata(6.0));

        public double UpperInnerShadowBlurRadius
        {
            get { return (double)this.GetValue(UpperInnerShadowBlurRadiusProperty); }
            set { this.SetValue(UpperInnerShadowBlurRadiusProperty, value); }
        }
        private static readonly DependencyProperty UpperInnerShadowBlurRadiusProperty = DependencyProperty.Register(
            nameof(UpperInnerShadowBlurRadius),
            typeof(double),
            typeof(BaseShadowBoxControl),
            new PropertyMetadata(10.0));

        public double LowerOuterShadowBlurRadius
        {
            get { return (double)this.GetValue(LowerOuterShadowBlurRadiusProperty); }
            set { this.SetValue(LowerOuterShadowBlurRadiusProperty, value); }
        }
        private static readonly DependencyProperty LowerOuterShadowBlurRadiusProperty = DependencyProperty.Register(
            nameof(LowerOuterShadowBlurRadius),
            typeof(double),
            typeof(BaseShadowBoxControl),
            new PropertyMetadata(6.0));

        public double UpperOuterShadowBlurRadius
        {
            get { return (double)this.GetValue(UpperOuterShadowBlurRadiusProperty); }
            set { this.SetValue(UpperOuterShadowBlurRadiusProperty, value); }
        }
        private static readonly DependencyProperty UpperOuterShadowBlurRadiusProperty = DependencyProperty.Register(
            nameof(UpperOuterShadowBlurRadius),
            typeof(double),
            typeof(BaseShadowBoxControl),
            new PropertyMetadata(6.0));

        #endregion BlurRadii

        /*public double ContentPadding
        {
            get { return (double)this.GetValue(ContentPaddingProperty); }
            set { this.SetValue(ContentPaddingProperty, value); }
        }
        private static readonly DependencyProperty ContentPaddingProperty = DependencyProperty.Register(
            nameof(ContentPadding),
            typeof(double),
            typeof(BaseShadowBoxControl),
            new PropertyMetadata(9.0));*/

    }
}
