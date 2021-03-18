using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfSandbox
{
    public class TriangleRadioButton : RadioButton, INotifyPropertyChanged
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

        public TriangleRadioButton() : base()
        {
            this.MouseEnter += TriangleRadioButton_MouseEnter;
            this.MouseLeave += TriangleRadioButton_MouseLeave;
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(
                nameof(Label),
                typeof(string),
                typeof(TriangleRadioButton),
                new FrameworkPropertyMetadata("XX"));

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register(
                nameof(Angle),
                typeof(double),
                typeof(TriangleRadioButton),
                new FrameworkPropertyMetadata(0.0));

        public double Scale
        {
            get => _scale;
            set {
                _scale = value;
                NotifyPropertyChanged();
            }
        }
        private double _scale = 1.0;

        private void TriangleRadioButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Scale = 1.0;
        }

        private void TriangleRadioButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (IsEnabled)
                Scale = 1.1;
        }
    }

    /// <summary>
    ///     Contains properties that will be used for a collection of TriangleRadioButtons
    /// </summary>
    public class TriangleRadioButtonProperties
    {
        #region Colors

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
                typeof(TriangleRadioButtonProperties),
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
                typeof(TriangleRadioButtonProperties),
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
                typeof(TriangleRadioButtonProperties),
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
                typeof(TriangleRadioButtonProperties),
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
                typeof(TriangleRadioButtonProperties),
                new FrameworkPropertyMetadata(Brushes.Black, 
                    FrameworkPropertyMetadataOptions.Inherits));

        #endregion Colors

        #region PathGeometryProperty
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
                typeof(TriangleRadioButtonProperties),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.Inherits));

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
                typeof(TriangleRadioButtonProperties),
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
                typeof(TriangleRadioButtonProperties),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.Inherits));

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
                typeof(TriangleRadioButtonProperties),
                new FrameworkPropertyMetadata(new PathGeometry(), FrameworkPropertyMetadataOptions.Inherits));

        #endregion PathGeometryProperty
    }
}
