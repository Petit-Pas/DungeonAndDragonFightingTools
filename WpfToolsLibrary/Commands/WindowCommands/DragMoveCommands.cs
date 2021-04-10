using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfToolsLibrary.Commands.WindowCommands
{
    internal static class DragMoveCommand
    {
        internal static Point startPoint = new Point();
    }


    public class DragMoveCommand_PreviewMouseLeftButtonDown : ICommand
    {
        private Point relativePoint;

    #pragma warning disable 0067
        public event EventHandler CanExecuteChanged;
    #pragma warning restore 0067


        public DragMoveCommand_PreviewMouseLeftButtonDown()
        {
            this.relativePoint = DragMoveCommand.startPoint;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Point tmp = Mouse.GetPosition((Window)parameter);
            relativePoint.X = tmp.X;
            relativePoint.Y = tmp.Y;
        }
    }

    public class DragMoveCommand_PreviewMouseMove : ICommand
    {
        private Point relativePoint;

#pragma warning disable 0067
        public event EventHandler CanExecuteChanged;
#pragma warning disable 0067

        public DragMoveCommand_PreviewMouseMove()
        {
            this.relativePoint = DragMoveCommand.startPoint;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Point newPoint = Mouse.GetPosition((Window)parameter);
                if ((Math.Abs(newPoint.X - relativePoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(newPoint.Y - relativePoint.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    ((Window)parameter).DragMove();
                }
            }
        }
    }

}
