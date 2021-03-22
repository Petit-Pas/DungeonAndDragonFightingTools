using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace WpfCustomControlLibrary.SliderButton
{
    public class SliderButtonControl : ToggleButton
    {
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/SliderButton/SliderButtonStyles.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style style = styleDict["SliderButtonStyle"] as Style;

        public SliderButtonControl() : base()
        {
            this.Loaded += SliderButton_Loaded;
            if (style != null)
            {
                this.Style = style;
            }
        }

        private void SliderButton_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContextChanged += SliderButton_DataContextChanged;
            this.Checked += SliderButton_Checked;
            this.Unchecked += SliderButton_Unchecked;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            init();
        }

        private void init()
        {
            if (this.IsChecked == true)
            {
                SliderButton_Checked(this, null);
            }
            else
                SliderButton_Unchecked(this, null);
        }

        private void SliderButton_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            init();
        }

        private void SliderButton_Unchecked(object sender, RoutedEventArgs e)
        {
            // button
            Ellipse ellipse = this.Template.FindName("SliderButton_ButtonControl", this) as Ellipse;
            TranslateTransform trans = new TranslateTransform();
            ellipse.RenderTransform = trans;
            DoubleAnimation x_animation = new DoubleAnimation(ButtonWidth, 0, TimeSpan.FromSeconds(0.25));
            trans.BeginAnimation(TranslateTransform.XProperty, x_animation);

            // unchecked label
            TextBlock unchecked_label = this.Template.FindName("SliderButton_UncheckLabel", this) as TextBlock;
            DoubleAnimation opacity_animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.25));
            unchecked_label.BeginAnimation(TextBlock.OpacityProperty, opacity_animation);

            // checked label
            TextBlock checked_label = this.Template.FindName("SliderButton_CheckLabel", this) as TextBlock;
            opacity_animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.25));
            checked_label.BeginAnimation(TextBlock.OpacityProperty, opacity_animation);

            // middle part
            Border middle_part = this.Template.FindName("SliderButton_MiddlePart", this) as Border;
            ColorAnimation color_animation = new ColorAnimation((Color)Application.Current.Resources["RawIndigo"], (Color)Application.Current.Resources["RawGray"], TimeSpan.FromSeconds(0.25));
            SolidColorBrush brush = new SolidColorBrush();
            middle_part.Background = brush;


            // right side
            Ellipse right_side = this.Template.FindName("SliderButton_RightBorder", this) as Ellipse;
            right_side.Fill = brush;

            // left side
            Ellipse left_side = this.Template.FindName("SliderButton_LeftBorder", this) as Ellipse;
            left_side.Fill = brush;

            brush.BeginAnimation(SolidColorBrush.ColorProperty, color_animation);

        }

        private void SliderButton_Checked(object sender, RoutedEventArgs e)
        {
            //button
            Ellipse ellipse = this.Template.FindName("SliderButton_ButtonControl", this) as Ellipse;
            TranslateTransform trans = new TranslateTransform();
            ellipse.RenderTransform = trans;
            DoubleAnimation x_animation = new DoubleAnimation(0, ButtonWidth, TimeSpan.FromSeconds(0.25));
            trans.BeginAnimation(TranslateTransform.XProperty, x_animation);

            // unchecked label
            TextBlock no_label = this.Template.FindName("SliderButton_UncheckLabel", this) as TextBlock;
            DoubleAnimation opacity_animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.25));
            no_label.BeginAnimation(TextBlock.OpacityProperty, opacity_animation);

            // checked label
            TextBlock checked_label = this.Template.FindName("SliderButton_CheckLabel", this) as TextBlock;
            opacity_animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.25));
            checked_label.BeginAnimation(TextBlock.OpacityProperty, opacity_animation);

            // middle part
            Border middle_part = this.Template.FindName("SliderButton_MiddlePart", this) as Border;
            ColorAnimation color_animation = new ColorAnimation((Color)Application.Current.Resources["RawGray"], (Color)Application.Current.Resources["RawIndigo"], TimeSpan.FromSeconds(0.25));
            SolidColorBrush brush = new SolidColorBrush();
            middle_part.Background = brush;

            // right side
            Ellipse right_side = this.Template.FindName("SliderButton_RightBorder", this) as Ellipse;
            right_side.Fill = brush;

            // left side
            Ellipse left_side = this.Template.FindName("SliderButton_LeftBorder", this) as Ellipse;
            left_side.Fill = brush;

            brush.BeginAnimation(SolidColorBrush.ColorProperty, color_animation);
        }

        public int LabelFontSize
        {
            get { return (int)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }
        public static readonly System.Windows.DependencyProperty LabelFontSizeProperty = System.Windows.DependencyProperty.Register
            (nameof(LabelFontSize), typeof(int), typeof(SliderButtonControl),
            new PropertyMetadata(15));

        public double ButtonWidth
        {
            get { return (double)GetValue(ButtonWidthProperty); }
            set { SetValue(ButtonWidthProperty, value); }
        }
        public static readonly System.Windows.DependencyProperty ButtonWidthProperty = System.Windows.DependencyProperty.Register
            (nameof(ButtonWidth), typeof(double), typeof(SliderButtonControl),
            new PropertyMetadata(40.0));

        public double ButtonHeight
        {
            get { return (double)GetValue(ButtonHeightProperty); }
            set { SetValue(ButtonHeightProperty, value); }
        }
        public static readonly System.Windows.DependencyProperty ButtonHeightProperty = System.Windows.DependencyProperty.Register
            (nameof(ButtonHeight), typeof(double), typeof(SliderButtonControl), 
            new PropertyMetadata(25.0));

        public string CheckedLabel
        {
            get { return (string)GetValue(CheckedLabelProperty); }
            set { SetValue(CheckedLabelProperty, value); }
        }

        public static readonly System.Windows.DependencyProperty CheckedLabelProperty = System.Windows.DependencyProperty.Register
            (nameof(CheckedLabel), typeof(string), typeof(SliderButtonControl), 
            new System.Windows.PropertyMetadata("Yes"));

        public string UncheckedLabel
        {
            get { return (string)GetValue(UncheckedLabelProperty); }
            set { SetValue(UncheckedLabelProperty, value); } 
        }
        public static readonly System.Windows.DependencyProperty UncheckedLabelProperty = System.Windows.DependencyProperty.Register
            (nameof(UncheckedLabel), typeof(string), typeof(SliderButtonControl), 
            new System.Windows.PropertyMetadata("No"));
    }
}