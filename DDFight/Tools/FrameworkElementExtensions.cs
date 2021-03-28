using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.ValidationRules;

namespace DDFight.Tools
{
    public static class FrameworkElementExtensions
    {
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

        public static void UnregisterAllChildren(this FrameworkElement element)
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
                child.UnregisterAllChildren();
            }
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
                            if (!ctx.Name.ToLower().Contains(filter.ToLower()))
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
