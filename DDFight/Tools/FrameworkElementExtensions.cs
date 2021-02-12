using DDFight.ValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DDFight.Tools
{
    public static class FrameworkElementExtensions
    {
        #region Animation

        public static void Translate(this FrameworkElement elem, TimeSpan duration, int x = 0, int y = 0)
        {
            DoubleAnimation x_animation = new DoubleAnimation(x, duration);
            DoubleAnimation y_animation = new DoubleAnimation(y, duration);

            //elem.BeginAnimation(, x_animation);
            //elem.BeginAnimation(, y_animation);

        }

        #endregion Animation

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

        public static void FilterINameableListBox(this FrameworkElement element, string filter)
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
                            INameable ctx = (INameable)child.DataContext;
                            if (!ctx.GetName().ToLower().Contains(filter.ToLower()))
                                child.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        child.FilterINameableListBox(filter);
                    }
                }
            }
        }


    }
}
