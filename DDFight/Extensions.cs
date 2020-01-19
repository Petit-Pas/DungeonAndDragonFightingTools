using DDFight.Game;
using DDFight.Tools;
using DDFight.ValidationRules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DDFight
{
    public static class Extensions
    {
        public static void Sort<T>(this ObservableCollection<T> collection, Comparison<T> comparison)
        {
            var sortableList = new List<T>(collection);
            sortableList.Sort(comparison);

            for (int i = 0; i < sortableList.Count; i++)
            {
                collection.Move(collection.IndexOf(sortableList[i]), i);
            }
        }


        public static bool AreAllChildrenValid(this FrameworkElement element)
        {
            foreach (Control ctrl in element.FindAllChildren<IValidable>())
            {
                IValidable test = ctrl as IValidable;
                if (test != null)
                    if (test.IsValid() == false)
                    {
                        return false;
                    }
            }
            return true;
        }

        public static void UnregisterAll(this FrameworkElement element)
        {
            if (element == null)
                return;
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(element); i += 1)
            {
                var child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
                if (child is IEventUnregisterable)
                {
                    ((IEventUnregisterable)child).UnregisterToAll();
                }
                child.UnregisterAll();
            }
        }

        public static List<FrameworkElement> FindAllChildren<T>(this FrameworkElement element) 
        {
            List<FrameworkElement> found_here = new List<FrameworkElement>();

            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(element); i += 1)
            {
                var child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
                if (child is T)
                    found_here.Add(child);
                else
                    found_here.AddRange(child.FindAllChildren<T>());
            }
            return found_here;
        }

        public static FrameworkElement GetFirstChildByName(this FrameworkElement element, string name)
        {
            if (element == null)
                return null;
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(element); i += 1)
            {
                var child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
                if (child.Name == name)
                    return child;
                FrameworkElement tmp = child.GetFirstChildByName(name);
                if (tmp != null)
                    return tmp;
            }
            return null;
        }

        public static void FilterPlayableEntityListBox(this FrameworkElement element, string filter)
        {
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(element); i += 1)
            {
                FrameworkElement child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
                if (child != null)
                {
                    if (child is ListBoxItem)
                    {
                        child.Visibility = Visibility.Visible;
                        if (filter != "")
                        {
                            PlayableEntity ctx = (PlayableEntity)child.DataContext;
                            if (!ctx.Name.ToLower().Contains(filter.ToLower()))
                                child.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        child.FilterPlayableEntityListBox(filter);
                    }
                }
            }
        }

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static ObservableCollection<T> Clone<T>(this ObservableCollection<T> listToClone) where T : ICloneable
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
