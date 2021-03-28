using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DDFight
{
    public static class Extensions
    {

        public static void DisposeAllDisposableMembers(this object target)
        {
            if (target == null) return;
            FieldInfo[] fields = target.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Concat(target.GetType().BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)).ToArray();
            var disposables = fields.Where(x => x.FieldType.GetInterfaces().Contains(typeof(IDisposable)));

            foreach (var disposableField in disposables)
            {
                var value = (IDisposable)disposableField.GetValue(target);
                if (value != null)
                    value.Dispose();
            }
        }

        public static void ShowCentered(this Window window)
        {
            window.Owner = Global.CurrentMainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        public static List<T> GetChildrenOfType<T>(this ItemsControl depObj)
            where T : DependencyObject
        {
            var result = new List<T>();
            if (depObj == null) return null;
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(depObj);
            while (queue.Count > 0)
            {
                var currentElement = queue.Dequeue();
                var childrenCount = VisualTreeHelper.GetChildrenCount(currentElement);
                for (var i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(currentElement, i);
                    if (child is T)
                        result.Add(child as T);
                    queue.Enqueue(child);
                }
            }
            return result;
        }

        public static Run BuildRun(string text, Brush color, int fontSize, FontWeight fontWeight)
        {
            Run result = new Run();

            result.Text = text;
            result.Foreground = color;
            result.FontSize = fontSize;
            result.FontWeight = fontWeight;

            return result;
        }

    }
}
