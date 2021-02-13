using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DDFight
{
    public static class Extensions
    {
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public static void ShowCentered(this Window window)
        {
            window.Owner = Global.CurrentMainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        /// <summary>
        ///     Filter a collection with the given comparison
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="comparison"></param>
        public static void Sort<T>(this ObservableCollection<T> collection, Comparison<T> comparison)
        {
            var sortableList = new List<T>(collection);
            sortableList.Sort(comparison);

            for (int i = 0; i < sortableList.Count; i++)
            {
                collection.Move(collection.IndexOf(sortableList[i]), i);
            }
        }

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> listToTransform) 
        {
            ObservableCollection<T> result = new ObservableCollection<T>();

            foreach (T obj in listToTransform)
                result.Add(obj);
            return result;
        }

        public static ObservableCollection<T> Clone<T>(this ObservableCollection<T> listToClone) where T : ICloneable
        {
            return new ObservableCollection<T>(listToClone.Select(item => (T)item.Clone()).ToList());
        }

        public static ObservableCollection<T> Clone<T, U>(this ObservableCollection<U> listToClone) where T : ICloneable where U : T
        {
            return new ObservableCollection<T>(listToClone.Select(item => (T)item.Clone()).ToList());
        }

        public static IList<uint> Clone(this IList<uint> listToClone)
        {
            return listToClone.Select(item => item).ToList();
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
