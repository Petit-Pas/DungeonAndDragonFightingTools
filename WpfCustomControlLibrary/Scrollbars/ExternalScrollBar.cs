using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WpfCustomControlLibrary.Scrollbars
{
    public class ExternalScrollBar : ScrollBar
    {

        public ExternalScrollBar()
        {
            this.Scroll += ScrollBar_Scroll;
            this.Orientation = Orientation.Vertical;
            Loaded += CustomScrollBar_Loaded;
        }

        private void CustomScrollBar_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollableChanged();
        }

        public ScrollViewer LinkedScrollViewer
        {
            get { return (ScrollViewer)this.GetValue(LinkedScrollViewerProperty); }
            set { this.SetValue(LinkedScrollViewerProperty, value); }
        }
        private static readonly DependencyProperty LinkedScrollViewerProperty = DependencyProperty.Register(
            nameof(LinkedScrollViewer),
            typeof(ScrollViewer),
            typeof(ExternalScrollBar),
            new PropertyMetadata(null, StaticScrollableChanged));

        private static void StaticScrollableChanged(DependencyObject o, DependencyPropertyChangedEventArgs a)
        {
            ((ExternalScrollBar)o).ScrollableChanged();
        }

        private void ScrollableChanged()
        {
            if (LinkedScrollViewer != null)
            {
                LinkedScrollViewer.ScrollChanged -= LinkedScrollViewer_ScrollChanged;
                LinkedScrollViewer.ScrollChanged += LinkedScrollViewer_ScrollChanged;

                refresh_scrollbar(LinkedScrollViewer.ExtentHeight, LinkedScrollViewer.ViewportHeight);
            }
        }

        private void refresh_scrollbar(double content_height, double viewport_height)
        {
            this.Minimum = 0;
            this.Maximum = LinkedScrollViewer.ExtentHeight - LinkedScrollViewer.ViewportHeight;
            if (LinkedScrollViewer.ViewportHeight != 0)
            {
                this.ViewportSize = (this.ActualHeight / (LinkedScrollViewer.ExtentHeight / LinkedScrollViewer.ViewportHeight)) * 2;
            }
            if (this.Maximum == 0)
                this.Visibility = Visibility.Collapsed;
            else
                this.Visibility = Visibility.Visible;
        }

        private void LinkedScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ViewportHeightChange != 0)
                refresh_scrollbar(LinkedScrollViewer.ExtentHeight, LinkedScrollViewer.ViewportHeight);
            this.Value = e.VerticalOffset;
        }

        private void ScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            UpdateScrollOffset(this.Value);
        }

        public void UpdateScrollOffset(double newOffset)
        {
            if (LinkedScrollViewer != null)
            {
                LinkedScrollViewer.ScrollToVerticalOffset(newOffset);
            }
        }
    }
}
