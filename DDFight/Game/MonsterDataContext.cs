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
    public class MonsterDataContext : IClonable
    {
        /// <summary>
        ///     This is used when the MonsterDataContext is used as a DataContext for an edit window. If the user Cancelled the operation, it is set to false.
        /// </summary>
        public bool Validated = false;

        #region MonsterProperties

        /// <summary>
        ///     Name of the Character
        /// </summary>
        [XmlAttribute]
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _name = "Name";

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

        /// <summary>
        ///     Armor Class
        /// </summary>
        [XmlAttribute]
        public uint CA
        {
            get => _cA;
            set
            {
                if (value != _cA)
                {
                    _cA = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private uint _cA = 10;

        /// <summary>
        ///     Max Hps that this character can have (Temporary Hps not taken into account)
        /// </summary>
        [XmlAttribute]
        public uint MaxHp
        {
            get => _maxHp;
            set
            {
                if (value != _maxHp)
                {
                    _maxHp = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private uint _maxHp = 0;

        /// <summary>
        ///     Current Hps (Temporary Hps not taken into account)
        /// </summary>
        [XmlAttribute]
        public int Hp
        {
            get => _hp;
            set
            {
                if (value != _hp)
                {
                    _hp = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _hp = 0;

        /// <summary>
        ///     The mastery Bonus to apply on mastered Characteristics
        /// </summary>
        [XmlAttribute]
        public uint MasteryBonus
        {
            get => _masteryBonus;
            set
            {
                if (value != _masteryBonus)
                {
                    _masteryBonus = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private uint _masteryBonus = 0;

        /// <summary>
        ///     The characteristics of the character
        /// </summary>
        public CharacteristicsDataContext Characteristics
        {
            get => _characteristics;
            set
            {
                _characteristics = value;
                NotifyPropertyChanged();
            }
        }
        private CharacteristicsDataContext _characteristics = new CharacteristicsDataContext();

        #endregion

        #region INotifyPropertyChangedSub

        /// <summary>
        ///     Subscribes the given event handler to this + all nested classes' PropertyChanged events
        /// </summary>
        /// <param name="handler"></param>
        public void PropertyChangedSubscript(PropertyChangedEventHandler handler)
        {
            this.PropertyChanged += handler;
            Characteristics.PropertyChangedSubscript(handler);
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
        ///     Method to clone a character (Deep Copy)
        /// </summary>
        /// <returns></returns>
        public IClonable Clone()
        {
            return new MonsterDataContext
            {
                Name = (string)this.Name.Clone(),
                Level = this.Level,
                CA = this.CA,
                Hp = this.Hp,
                MaxHp = this.MaxHp,
                Characteristics = (CharacteristicsDataContext)this.Characteristics.Clone(),
            };
        }

        #endregion
    }
}
