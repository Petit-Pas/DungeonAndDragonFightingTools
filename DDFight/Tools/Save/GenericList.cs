using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDFight.Tools.Save
{
    public class GenericList<T> : INotifyPropertyChanged
        where T : class, IListable, new()
    {
        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        [XmlElement]
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
            if (elem.Edit())
            {
                NotifyPropertyChanged("Elements");
                return true;
            }
            return false;
        }

        public void RemoveElement(T elem)
        {
            Elements.Remove(elem);
            NotifyPropertyChanged("Elements");
        }

        public void DuplicateElement(T elem)
        {
            T new_one = elem.Clone() as T;
            new_one.Name = new_one.Name = " - Copy";
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
            if (elem.Edit())
            {
                Elements.Add(elem);
                NotifyPropertyChanged("Elements");
            }
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
    }
}
