using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BaseToolsLibrary.Extensions
{
    public static class ListExtensions
    {
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

            for (var i = 0; i < sortableList.Count; i++)
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
            var result = new ObservableCollection<T>();

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
    }
}
