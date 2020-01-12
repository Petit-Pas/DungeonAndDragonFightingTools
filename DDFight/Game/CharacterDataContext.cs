using DDFight.Game.Characteristics;
using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DDFight.Game
{
    /// <summary>
    ///     Represents a Character for D&D (not to confound with Monsters)
    /// </summary>
    public class CharacterDataContext : PlayableEntity /*, INotifyPropertyChangedSub*/
    {
        public CharacterDataContext() : base()
        {
        }

        /// <summary>
        ///     This is used when the CharacterDataContext is used as a DataContext for an edit window. If the user Cancelled the operation, it is set to false.
        /// </summary>
        [XmlIgnore]
        public bool Validated = false;

        #region CharacterProperties

        /// <summary>
        ///     Level of the Character
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

        public override string DisplayName
        {
            get => Name;
        }

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

        #region IClonable

        /// <summary>
        ///     Copy Ctor, required for the Clone method to work properly with derived classes
        /// </summary>
        /// <param name=""></param>
        protected CharacterDataContext(CharacterDataContext to_copy) : base(to_copy)
        {
            Level = to_copy.Level;
        }

        public override object Clone()
        {
            return new CharacterDataContext(this);
        }

        #endregion
    }
}
