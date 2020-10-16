using DDFight.Game;
using DDFight.Game.Aggression;
using DDFight.Tools;
using DDFight.ValidationRules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DDFight
{
    public static class Extensions
    {

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

        /// <summary>
        ///     Checks if all the IValidable children are Valid (elem.IsValid())
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static bool AreAllChildrenValid(this FrameworkElement root)
        {
            if (root == null)
                return true;

            foreach (Control ctrl in root.FindAllChildren<IValidable>())
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

        /// <summary>
        ///     Not gonna lie, a bit of black magic due to lazy WPF was needed here
        ///     The First loop is supposed to go trough anything logical control (tabs, grids, etc) but controls such as Listview wont have their children in it.
        ///     The Second loop is supposed to counter that problem, by taking any control that didn't have any logical child, and checking for any visual one.
        ///     
        ///     Important to note that some components that haven't been showed yet (like in an unopened tab) wont be visible.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public static List<FrameworkElement> FindAllChildren<T>(this FrameworkElement root)
        {
            List<FrameworkElement> found_here = new List<FrameworkElement>();

            if (root == null)
                return found_here;

            bool hadLogicalChildren = false;

            foreach (object item in LogicalTreeHelper.GetChildren(root))
            {
                hadLogicalChildren = true;
                if (item is FrameworkElement)
                {
                    FrameworkElement element = (FrameworkElement)item;
                    if (element is T)
                    {
                        found_here.Add(element);
                    }
                    else
                    {
                        found_here.AddRange(element.FindAllChildren<T>());
                    }
                }
            }
            if (!hadLogicalChildren)
            {
                for (int i = 0; i != VisualTreeHelper.GetChildrenCount(root); i += 1)
                {
                var child = VisualTreeHelper.GetChild(root, i) as FrameworkElement;
                    if (child != null)
                    {
                        if (child is T)
                            found_here.Add(child);
                        else
                            found_here.AddRange(child.FindAllChildren<T>());
                    }
                }
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

        public static List<FrameworkElement> GetAllChildrenByName(this FrameworkElement element, string name)
        {
            List<FrameworkElement> result = new List<FrameworkElement>();

            if (element == null)
                return null;
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(element); i += 1)
            {
                var child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
                if (child != null)
                {
                    if (child.Name == name)
                    {
                        result.Add(child);
                    }
                    result.AddRange(child.GetAllChildrenByName(name));
                }
            }
            return result;
        }

        public static void FilterSpellListBox(this FrameworkElement element, string filter)
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
                            Spell ctx = (Spell)child.DataContext;
                            if (!ctx.Name.ToLower().Contains(filter.ToLower()))
                                child.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        child.FilterSpellListBox(filter);
                    }
                }
            }
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
