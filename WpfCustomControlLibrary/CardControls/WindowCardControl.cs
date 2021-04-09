using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfToolsLibrary.Extensions;

namespace WpfCustomControlLibrary.CardControls
{
    public class WindowCardControl : UpCardControl
    {
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/CardControls/WindowCardStyle.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style style = styleDict["WindowCardStyle"] as Style;

        public WindowCardControl() : base()
        {
            // a bug in this package forces us to use it in order for the dll to be loaded
            Microsoft.Xaml.Behaviors.TriggerAction nullable = null;

            if (style != null)
                this.Style = style;
            LeftButtonDownCommand = new PreviewMouseLeftButtonDownCommand(this);
            MouseMoveCommand = new PreviewMouseMoveCommand(this);
            ClickCommand = new OnClickCommand();
        }

        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }
        private static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(WindowCardControl),
                new PropertyMetadata("Title")
            );

        public OnClickCommand ClickCommand { get; set; }
        public class OnClickCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                Application.Current.GetCurrentWindow().Close();
            }
        }

        public Point startPoint;

        public PreviewMouseLeftButtonDownCommand LeftButtonDownCommand { get; set; }
        public class PreviewMouseLeftButtonDownCommand : ICommand
        {
            private readonly WindowCardControl ctrl;

            public event EventHandler CanExecuteChanged;

            public PreviewMouseLeftButtonDownCommand(WindowCardControl ctrl)
            {
                this.ctrl = ctrl;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                ctrl.startPoint = Mouse.GetPosition(Application.Current.GetCurrentWindow());
            }
        }

        public PreviewMouseMoveCommand MouseMoveCommand { get; set; }
        public class PreviewMouseMoveCommand : ICommand
        {
            private readonly WindowCardControl ctrl;

            public event EventHandler CanExecuteChanged;

            public PreviewMouseMoveCommand(WindowCardControl ctrl)
            {
                this.ctrl = ctrl;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    Point newPoint = Mouse.GetPosition(Application.Current.GetCurrentWindow());
                    if ((Math.Abs(newPoint.X - ctrl.startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                        Math.Abs(newPoint.Y - ctrl.startPoint.Y) > SystemParameters.MinimumVerticalDragDistance))
                    {
                        Application.Current.GetCurrentWindow().DragMove();
                    }
                }
            }
        }
    }
}
