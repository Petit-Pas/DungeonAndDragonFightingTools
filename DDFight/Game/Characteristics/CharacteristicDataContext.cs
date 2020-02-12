// /***********************************************************************************
//  * ERTMS Solutions
//  ***********************************************************************************/

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DDFight.Game.Characteristics
{
    /// <summary>
    ///     Respresents 1 characteristic
    /// </summary>
    public class CharacteristicDataContext : ICloneable, INotifyPropertyChanged
    {
        /// <summary>
        ///     Ctor
        /// </summary>
        public CharacteristicDataContext() { }

        /// <summary>
        ///     Ctor
        /// </summary>
        public CharacteristicDataContext(CharacteristicsEnum name)
        {
            _name = name;
        }

        #region CharacteristicsProperties

        /// <summary>
        ///     Ex: strength, wisdow, etc...
        /// </summary>
        [XmlAttribute]
        public CharacteristicsEnum Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private CharacteristicsEnum _name;

        /// <summary>
        ///     Says if we need to add the mastery bonus to the Modifier
        /// </summary>
        [XmlAttribute]
        public bool Mastery
        {
            get => _mastery;
            set
            {
                if (_mastery != value)
                {
                    _mastery = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _mastery = false;

        /// <summary>
        ///     the value to add to results of this characteristic
        /// </summary>
        [XmlAttribute]
        public int Modifier
        {
            get => _modifier;
            set
            {
                if (_modifier != value)
                {
                    _modifier = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _modifier = 0;

        #endregion

        #region INotifyPropertyChangedSub

        /// <summary>
        ///     Subscribes the given event handler to this + all nested classes' PropertyChanged events
        /// </summary>
        /// <param name="handler"></param>
        public void PropertyChangedSubscript(PropertyChangedEventHandler handler)
        {
            this.PropertyChanged += handler;
        }

        #endregion

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

        #region ICloneable

        protected CharacteristicDataContext(CharacteristicDataContext to_copy)
        {
            Mastery = to_copy.Mastery;
            Modifier = to_copy.Modifier;
            Name = to_copy.Name;
        }

        /// <summary>
        ///     Processes Deep copy on the item
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new CharacteristicDataContext(this);
        }

        #endregion
    }
}
