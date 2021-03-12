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

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.RegisterAttached(
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
            Scale = 1.1;
        }
    }

    /// <summary>
    ///     Contains properties that will be used for a collection of TriangleRadioButtons
    /// </summary>
    public static class TriangleRadioButtonProperties
    {
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
    }
}
