using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfSandbox
{
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        public ModalWindow()
        {
            InitializeComponent();
        }

        private Point startPoint;

        private void PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(this);
        }

        private void PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point newPoint = e.GetPosition(this);
            if (e.LeftButton == MouseButtonState.Pressed && (Math.Abs(newPoint.X - startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(newPoint.Y - startPoint.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                this.DragMove();
            }
        }

    }
}
