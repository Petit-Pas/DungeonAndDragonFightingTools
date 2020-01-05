using DDFight.Game.Characteristics;
using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDFight.Game
{
    public class MonsterDataContext : PlayableEntity
    {
        public MonsterDataContext()
        {
        }

        /// <summary>
        ///     This is used when the MonsterDataContext is used as a DataContext for an edit window. If the user Cancelled the operation, it is set to false.
        /// </summary>
        [XmlIgnore]
        public bool Validated = false;

        #region MonsterProperties

        /// <summary>
        ///     Level of the Monster
        /// </summary>
        [XmlAttribute]
        public uint Level
        {
            get => _level;
            set
            {
                if (value != _level)
                {
                    _level = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private uint _level = 1;

        #endregion

        #region INotifyPropertyChangedSub
        /*
        /// <summary>
        ///     Subscribes the given event handler to this + all nested classes' PropertyChanged events
        /// </summary>
        /// <param name="handler"></param>
        public void PropertyChangedSubscript(PropertyChangedEventHandler handler)
        {
            this.PropertyChanged += handler;
            Characteristics.PropertyChangedSubscript(handler);
        }
        */

        #endregion

        protected MonsterDataContext(MonsterDataContext to_copy) : base(to_copy)
        {
            Level = to_copy.Level;
        }

        #region ICloneable
        /// <summary>
        ///     Method to clone a character (Deep Copy)
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new MonsterDataContext(this);
        }

        #endregion
    }
}
