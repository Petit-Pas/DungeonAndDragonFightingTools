using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WpfToolsLibrary.Extensions
{
    public static class TreeReflexionExtensions
    {
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
    }
}
