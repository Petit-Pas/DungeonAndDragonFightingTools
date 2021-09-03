using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfCustomControlLibrary.CircularSelector
{
    public class CircularSelectorCenterControl : ContentControl
    {
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/CircularSelector/CircularSelectorCenterStyle.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style style = styleDict["CircularSelectorCenterStyle"] as Style;

        public CircularSelectorCenterControl()
            : base()
        {
            if (style != null)
                this.Style = style;
        }

        public static PathGeometry GetPathGeometry(DependencyObject obj)
        {
            return (PathGeometry)obj.GetValue(PathGeometryProperty);
        }
        public static void SetPathGeometry(DependencyObject obj, double value)
        {
            obj.SetValue(PathGeometryProperty, value);
        }
        public static readonly DependencyProperty PathGeometryProperty =
            DependencyProperty.RegisterAttached(
                "PathGeometry",
                typeof(PathGeometry),
                typeof(CircularSelectorCenterControl),
                new FrameworkPropertyMetadata(new PathGeometry(), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetBorderBaseColorBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(BorderBaseColorBrushProperty, value);
        }
        public static Brush GetBorderBaseColorBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BorderBaseColorBrushProperty);
        }
        public static readonly DependencyProperty BorderBaseColorBrushProperty =
            DependencyProperty.RegisterAttached(
                "BorderBaseColorBrush",
                typeof(Brush),
                typeof(CircularSelectorCenterControl),
                new FrameworkPropertyMetadata(Brushes.Gray,
                    FrameworkPropertyMetadataOptions.Inherits));

        public static void SetDisabledColorBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(DisabledColorBrushProperty, value);
        }
        public static Brush GetDisabledColorBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(DisabledColorBrushProperty);
        }
        public static readonly DependencyProperty DisabledColorBrushProperty =
            DependencyProperty.RegisterAttached(
                "DisabledColorBrush",
                typeof(Brush),
                typeof(CircularSelectorCenterControl),
                new FrameworkPropertyMetadata(Brushes.Gray,
                    FrameworkPropertyMetadataOptions.Inherits));


        public static void SetHoverColorBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(HoverColorBrushProperty, value);
        }
        public static Brush GetHoverColorBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HoverColorBrushProperty);
        }
        public static readonly DependencyProperty HoverColorBrushProperty =
            DependencyProperty.RegisterAttached(
                "HoverColorBrush",
                typeof(Brush),
                typeof(CircularSelectorCenterControl),
                new FrameworkPropertyMetadata(Brushes.LightGreen,
                    FrameworkPropertyMetadataOptions.Inherits));

        public static void SetSelectedColorBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectedColorBrushProperty, value);
        }
        public static Brush GetSelectedColorBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectedColorBrushProperty);
        }
        public static readonly DependencyProperty SelectedColorBrushProperty =
            DependencyProperty.RegisterAttached(
                "SelectedColorBrush",
                typeof(Brush),
                typeof(CircularSelectorCenterControl),
                new FrameworkPropertyMetadata(Brushes.Green,
                    FrameworkPropertyMetadataOptions.Inherits));

        public static void SetBaseColorBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(BaseColorBrushProperty, value);
        }
        public static Brush GetBaseColorBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BaseColorBrushProperty);
        }
        public static readonly DependencyProperty BaseColorBrushProperty =
            DependencyProperty.RegisterAttached(
                "BaseColorBrush",
                typeof(Brush),
                typeof(CircularSelectorCenterControl),
                new FrameworkPropertyMetadata(Brushes.Black,
                    FrameworkPropertyMetadataOptions.Inherits));

        public static int GetLabelFontSize(DependencyObject obj)
        {
            return (int)obj.GetValue(LabelFontSizeProperty);
        }
        public static void SetLabelFontSize(DependencyObject obj, int value)
        {
            obj.SetValue(LabelFontSizeProperty, value);
        }
        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.RegisterAttached(
                "LabelFontSize",
                typeof(int),
                typeof(CircularSelectorCenterControl),
                new FrameworkPropertyMetadata(15, FrameworkPropertyMetadataOptions.Inherits));

        public static double GetLabelYOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(LabelYOffsetProperty);
        }
        public static void SetLabelYOffset(DependencyObject obj, double value)
        {
            obj.SetValue(LabelYOffsetProperty, value);
        }
        public static readonly DependencyProperty LabelYOffsetProperty =
            DependencyProperty.RegisterAttached(
                "LabelYOffset",
                typeof(double),
                typeof(CircularSelectorCenterControl),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.Inherits));

        public static double GetLabelXOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(LabelXOffsetProperty);
        }
        public static void SetLabelXOffset(DependencyObject obj, double value)
        {
            obj.SetValue(LabelXOffsetProperty, value);
        }
        public static readonly DependencyProperty LabelXOffsetProperty =
            DependencyProperty.RegisterAttached(
                "LabelXOffset",
                typeof(double),
                typeof(CircularSelectorCenterControl),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.Inherits));
    }
}
