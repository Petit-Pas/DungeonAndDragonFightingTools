﻿using BaseToolsLibrary;
using BaseToolsLibrary.Extensions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DnDToolsLibrary.Memory
{
    /// <summary>
    ///    This class will be used everywhere where a collection is needed, it will allow easy heritance with BaseListUserControl
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericList<T> : INotifyPropertyChanged, ICloneable, IDisposable
        where T : class, ICloneable, new()
    {

        public GenericList()
        {
        }

        #region UpdateNotifications

        public class ListChangedArgs : EventArgs
        {
            public T Element;
            public GenericListOperation Operation;
        }

        public delegate void ListChanged_EventHandler(object sender, ListChangedArgs e);

        public event ListChanged_EventHandler ListChanged;

        protected void OnListChanged(ListChangedArgs e)
        {
            if (ListChanged != null)
            {
                ListChanged(this, e);
            }
        }

        public enum GenericListOperation
        {
            Addition,
            Deletion,
            Modification,
            Sort,
        }


        public class ListElementChangedArgs : EventArgs
        {
            public T Element;
            public GenericListOperation Operation;
        }

        public delegate void ListElementChanged_EventHandler(object sender, ListElementChangedArgs e);

        public event ListElementChanged_EventHandler ListElementChanged;

        public void OnListElementChanged(ListElementChangedArgs e)
        {
            if (ListElementChanged != null)
            {
                ListElementChanged(this, e);
            }
        }

        #endregion UpdateNotifications

        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public ObservableCollection<T> Elements
        {
            get => _elements;
            set
            {
                _elements = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<T> _elements = new ObservableCollection<T>();

        public void RemoveAt(int index)
        {
            Elements.RemoveAt(index);
        }

        public void RemoveElement(T elem)
        {
            Elements.Remove(elem);
            OnListChanged(new ListChangedArgs { Operation = GenericListOperation.Deletion, Element = elem });
            OnListElementChanged(new ListElementChangedArgs { Element = elem, Operation = GenericListOperation.Deletion });
        }

        /// <summary>
        ///     Adds the new element to Elements without opening any window
        /// </summary>
        /// <param name="elem"></param>
        public void AddElementSilent(T elem = null)
        {
            if (elem == null)
                elem = new T();
            Elements.Add(elem);
            OnListChanged(new ListChangedArgs { Operation = GenericListOperation.Addition, Element = elem });
            OnListElementChanged(new ListElementChangedArgs { Operation = GenericListOperation.Addition, Element = elem });
        }

        public void SortElements(Comparison<T> comparison)
        {
            Elements.Sort(comparison);
            NotifyPropertyChanged();
        }

        public static void SaveAll<U>(GenericList<U> list)
            where U : class, INameable, ICloneable, new()
        {
            SaveManager.SaveGenericList<U>(list);
        }

        private void init_copy(GenericList<T> to_copy)
        {
            Elements = to_copy.Elements.Clone();
        }

        protected GenericList(GenericList<T> to_copy)
        {
            init_copy(to_copy);
        }

        public virtual object Clone()
        {
            return new GenericList<T>(this);
        }

        public void Dispose()
        {
            if (Elements.Count != 0 && Elements[0] is IDisposable)
            {
                foreach (IDisposable disposable in Elements)
                {
                    disposable.Dispose();
                }
            }
        }

        public int Count
        { get => Elements.Count;}
    }
}
