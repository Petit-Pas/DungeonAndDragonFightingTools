// /***********************************************************************************
//  * ERTMS Solutions
//  ***********************************************************************************/

using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Characteristics
{
    /// <summary>
    ///     Contains all the characteristic of a character
    /// </summary>
    public class CharacteristicsDataContext : ICloneable, INotifyPropertyChanged, INotifyPropertyChangedSub
    {

        #region characteristics

        /// <summary>
        ///     Strength
        /// </summary>
        public CharacteristicDataContext Strength
        {
            get => _strength;
            set
            {
                if (_strength != value)
                {
                    _strength = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private CharacteristicDataContext _strength = new CharacteristicDataContext("Strength");

        /// <summary>
        ///     Dexterity
        /// </summary>
        public CharacteristicDataContext Dexterity
        {
            get => _dexterity;
            set
            {
                if (_dexterity != value)
                {
                    _dexterity = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private CharacteristicDataContext _dexterity = new CharacteristicDataContext("Dexterity");

        /// <summary>
        ///     Constitution
        /// </summary>
        public CharacteristicDataContext Constitution
        {
            get => _constitution;
            set
            {
                if (_constitution != value)
                {
                    _constitution = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private CharacteristicDataContext _constitution = new CharacteristicDataContext("Constitution");

        /// <summary>
        ///     Intelligence
        /// </summary>
        public CharacteristicDataContext Intelligence
        {
            get => _intelligence;
            set
            {
                if (_intelligence != value)
                {
                    _intelligence = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private CharacteristicDataContext _intelligence = new CharacteristicDataContext("Intelligence");

        /// <summary>
        ///     Wisdom
        /// </summary>
        public CharacteristicDataContext Wisdom
        {
            get => _wisdom;
            set
            {
                if (_wisdom != value)
                {
                    _wisdom = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private CharacteristicDataContext _wisdom = new CharacteristicDataContext("Wisdom");

        /// <summary>
        ///     Charisma
        /// </summary>
        public CharacteristicDataContext Charisma
        {
            get => _charisma;
            set
            {
                if (_charisma != value)
                {
                    _charisma = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private CharacteristicDataContext _charisma = new CharacteristicDataContext("Charisma");

        #endregion

        #region INotifyPropertyChangedSub

        /// <summary>
        ///     Subscribes the given event handler to this + all nested classes' PropertyChanged events
        /// </summary>
        /// <param name="handler"></param>
        public void PropertyChangedSubscript(PropertyChangedEventHandler handler)
        {
            this.PropertyChanged += handler;
            Strength.PropertyChangedSubscript(handler);
            Dexterity.PropertyChangedSubscript(handler);
            Constitution.PropertyChangedSubscript(handler);
            Intelligence.PropertyChangedSubscript(handler);
            Wisdom.PropertyChangedSubscript(handler);
            Charisma.PropertyChangedSubscript(handler);
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

        #region IClonable

        /// <summary>
        ///     Process Deep copy on the item
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new CharacteristicsDataContext
            {
                Charisma = (CharacteristicDataContext)this._charisma.Clone(),
                Constitution = (CharacteristicDataContext)this._constitution.Clone(),
                Dexterity = (CharacteristicDataContext)this._dexterity.Clone(),
                Intelligence = (CharacteristicDataContext)this._intelligence.Clone(),
                Strength = (CharacteristicDataContext)this._strength.Clone(),
                Wisdom = (CharacteristicDataContext)this._wisdom.Clone(),
            };
        }

        #endregion
    }
}
