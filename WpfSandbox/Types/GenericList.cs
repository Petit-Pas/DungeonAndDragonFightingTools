using DDFight;
using DDFight.Tools;
using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfSandbox.Types
{/// <summary>
 ///    This class will be used everywhere where a collection is needed, it will allow easy heritance with BaseListUserControl
 /// </summary>
 /// <typeparam name="T"></typeparam>
    public class GenericList<T> : INotifyPropertyChanged, ICloneable
        where T : class, ICloneable, new()
    {

        public GenericList()
        {
        }

        #region UpdateNotifications

        public class ListChangedArgs : EventArgs
        {
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

        protected void OnListElementChanged(ListElementChangedArgs e)
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


        [XmlArrayItem("Spell", typeof(SpellType))]
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

        public bool EditElement(T elem)
        {
            
            return false;
        }

        public void RemoveElement(T elem)
        {
            Elements.Remove(elem);
            OnListChanged(new ListChangedArgs { Operation = GenericListOperation.Deletion });
            OnListElementChanged(new ListElementChangedArgs { Element = elem, Operation = GenericListOperation.Deletion });
        }

        public void DuplicateElement(T elem)
        {
            T new_one = elem.Clone() as T;
//            new_one.Name = new_one.Name + " - Copy";
            AddElement(new_one);
        }

        /// <summary>
        ///     Opens the editor window before adding the new element. If that window is canceled, the element is not added
        ///     Opposed to AddElementSilent()
        /// </summary>
        /// <param name="elem"></param>
        public void AddElement(T elem = null)
        {
            if (elem == null)
                elem = new T();
            
        }

        /// <summary>
        ///     Adds the new element to Elements, opposed to AddElement()
        /// </summary>
        /// <param name="elem"></param>
        public void AddElementSilent(T elem = null)
        {
            if (elem == null)
                elem = new T();
            Elements.Add(elem);
            NotifyPropertyChanged("Elements");
        }

        public void SortElements(Comparison<T> comparison)
        {
            Elements.Sort(comparison);
            NotifyPropertyChanged();
        }

        public void SaveAll()
        {

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
    }
}
