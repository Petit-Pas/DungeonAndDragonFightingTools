﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DDFight
{
    public static class Extensions
    {
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

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
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

    }
}
