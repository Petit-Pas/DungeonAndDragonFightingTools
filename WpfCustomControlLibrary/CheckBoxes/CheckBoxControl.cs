using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WpfCustomControlLibrary.CheckBoxes
{
    public class CheckBoxControl : CheckBox
    {
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/CheckBoxes/CheckBoxStyle.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style style = styleDict["CheckBoxStyle"] as Style;

        private static readonly ResourceDictionary pathsDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/CheckBoxes/CheckBoxGeometryPaths.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly PathGeometry checkMark = styleDict["CheckBoxVMark"] as PathGeometry;

        public CheckBoxControl() : base()
        {
            if (style != null)
                Style = style;
            if (checkMark != null)
                CheckMark = checkMark;
            Checked += CheckBoxControl_Checked;
            Unchecked += CheckBoxControl_Unchecked;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            init();
        }

        private bool initializing = false;

        private void init()
        {
            initializing = true;
            Path path = this.Template.FindName("CheckMark", this) as Path;
            if (this.IsChecked.Value == true)
            {
                CheckBoxControl_Checked(this, null);
            }
            else
                CheckBoxControl_Unchecked(this, null);
            if (path != null)
                path.Visibility = Visibility.Visible;
            initializing = false;
        }

        private void CheckBoxControl_Checked(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(15, 0, TimeSpan.FromSeconds(initializing ? 0.0 : 0.15));
            Path path = this.Template.FindName("CheckMark", this) as Path;
            if (path != null)
                path.BeginAnimation(Path.StrokeDashOffsetProperty, animation);
        }

        private void CheckBoxControl_Unchecked(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(0, 15, TimeSpan.FromSeconds(initializing ? 0.0 : 0.15));
            Path path = this.Template.FindName("CheckMark", this) as Path;
            if (path != null)
                path.BeginAnimation(Path.StrokeDashOffsetProperty, animation);
        }

        public Brush BackgroundColor
        {
            get { return (Brush)this.GetValue(BackgroundColorProperty); }
            set { this.SetValue(BackgroundColorProperty, value); }
        }
        private static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
                nameof(BackgroundColor),
                typeof(Brush),
                typeof(CheckBoxControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["LightestGray"])
            );

        public Brush BorderColor
        {
            get { return (Brush)this.GetValue(BorderColorProperty); }
            set { this.SetValue(BorderColorProperty, value); }
        }
        private static readonly DependencyProperty BorderColorProperty = DependencyProperty.Register(
                nameof(BorderColor),
                typeof(Brush),
                typeof(CheckBoxControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["Light"])
            );

        public Brush CheckColor
        {
            get { return (Brush)this.GetValue(CheckColorProperty); }
            set { this.SetValue(CheckColorProperty, value); }
        }
        private static readonly DependencyProperty CheckColorProperty = DependencyProperty.Register(
                nameof(CheckColor),
                typeof(Brush),
                typeof(CheckBoxControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["Light"])
            );

        /*public Brush AccentColor
        {
            get { return (Brush)this.GetValue(AccentColorProperty); }
            set { this.SetValue(AccentColorProperty, value); }
        }
        public static readonly DependencyProperty AccentColorProperty = DependencyProperty.Register(
                nameof(AccentColor),
                typeof(Brush),
                typeof(CheckBoxControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["CheckBoxColor"])
            );

        public Brush LightAccentColor
        {
            get { return (Brush)this.GetValue(SelectionColorProperty); }
            set { this.SetValue(SelectionColorProperty, value); }
        }
        private static readonly DependencyProperty SelectionColorProperty = DependencyProperty.Register(
                nameof(LightAccentColor),
                typeof(Brush),
                typeof(CheckBoxControl),
                new PropertyMetadata(Application.Current.Resources["LightIndigo"])
            );*/

        
        public PathGeometry CheckMark
        {
            get { return (PathGeometry)this.GetValue(CheckMarkProperty); }
            set { this.SetValue(CheckMarkProperty, value); }
        }
        private static readonly DependencyProperty CheckMarkProperty = DependencyProperty.Register(
            nameof(CheckMark),
            typeof(PathGeometry),
            typeof(CheckBoxControl),
            new PropertyMetadata(checkMark)
        );
    }
}
