﻿using BaseToolsLibrary;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Counters
{
    public class Counter : INotifyPropertyChanged, ICloneable, INameable, IEquivalentComparable<Counter>
    {
        public Counter() { }

        #region NameProperties

        [XmlAttribute]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        private string _name = "Counter Name";

        [XmlIgnore]
        public string DisplayName { get => Name; set { } }

        #endregion NameProperties

        #region ValueProperties

        /// <summary>
        ///     The maximum of the value, 0 for no maximum value
        /// </summary>
        [XmlAttribute]
        public int MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                NotifyPropertyChanged();
            }
        }
        private int _maxValue = 0;

        [XmlAttribute]
        public int CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                NotifyPropertyChanged();
            }
        }
        private int _currentValue = 0;

        #endregion ValueProperties

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

        #region IClonable

        /// <summary>
        ///     this method is required to completely initialize an instance of this by copying another object
        /// </summary>
        private void init_copy(Counter to_copy)
        {
            Name = (string)to_copy.Name.Clone();
            MaxValue = to_copy.MaxValue;
            CurrentValue = to_copy.CurrentValue;
        }

        /// <summary>
        ///     copy contructor, required for the Clone method to work properly with derived classes
        /// </summary>
        /// <param name=""></param>
        protected Counter(Counter to_copy)
        {
            init_copy(to_copy);
        }

        /// <summary>
        ///     Method to clone a character (Deep Copy)
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            return new Counter(this);
        }

        /// <summary>
        ///     reinitialize this object by copying the received one
        /// </summary>
        /// <param name="_to_copy"></param>
        public virtual void CopyAssign(object _to_copy)
        {
            Counter to_copy = (Counter)_to_copy;
            init_copy(to_copy);
        }

        #endregion

        public bool IsEquivalentTo(Counter toCompare)
        {
            if (Name != toCompare.Name)
                return false;
            if (MaxValue != toCompare.MaxValue)
                return false;
            if (CurrentValue != toCompare.CurrentValue)
                return false;
            return true;
        }

    }
}
