﻿using BaseToolsLibrary;
using BaseToolsLibrary.Extensions;
using BaseToolsLibrary.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Characteristics
{
    /// <summary>
    ///     Contains all the characteristic of a character
    /// </summary>
    public class CharacteristicList : ICloneable, INotifyPropertyChanged, IEquivalentComparable<CharacteristicList>
    {

        public CharacteristicList()
        {
            if (!Global.Loading)
                initCharacteristicsList();
        }

        public CharacteristicList(int str, int dex, int con, int _int, int wis, int cha)
        {
            _characteristicsList = new List<Characteristic>
            {
                new Characteristic(CharacteristicsEnum.Strength, str),
                new Characteristic(CharacteristicsEnum.Dexterity, dex),
                new Characteristic(CharacteristicsEnum.Constitution, con),
                new Characteristic(CharacteristicsEnum.Intelligence, _int),
                new Characteristic(CharacteristicsEnum.Wisdom, wis),
                new Characteristic(CharacteristicsEnum.Charisma, cha)
            };
        }

        private void initCharacteristicsList()
        {
            _characteristicsList = new List<Characteristic>
            {
                new Characteristic(CharacteristicsEnum.Strength),
                new Characteristic(CharacteristicsEnum.Dexterity),
                new Characteristic(CharacteristicsEnum.Constitution),
                new Characteristic(CharacteristicsEnum.Intelligence),
                new Characteristic(CharacteristicsEnum.Wisdom),
                new Characteristic(CharacteristicsEnum.Charisma)
            };
        }

        #region characteristics

        public Characteristic GetCharacteristic(CharacteristicsEnum type)
        {
            return CharacteristicsList.First(x => x.Name == type);
        }

        public int GetSavingModifier(CharacteristicsEnum type)
        {
            Characteristic charac = CharacteristicsList.First(x => x.Name == type);
            int reuslt = charac.Modifier;

            if (charac.Mastery == true)
                reuslt += (int)MasteryBonus;
            return reuslt;
        }

        public int GetCharacteristicModifier(CharacteristicsEnum type)
        {
            return CharacteristicsList.First(x => x.Name == type).Modifier;
        }

        public List<Characteristic> CharacteristicsList
        {
            get {
                return _characteristicsList;
            } 
            set 
            {
                _characteristicsList = value;
                NotifyPropertyChanged();
            }
        }
        private List<Characteristic> _characteristicsList = null;

        [XmlAttribute]
        /// <summary>
        ///     The 
        ///     to add to mastered characteristics
        /// </summary>
        public uint MasteryBonus
        {
            get => _masteryBonus;
            set
            {
                if (_masteryBonus != value)
                {
                    _masteryBonus = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private uint _masteryBonus = 0;

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
            Strength.PropertyChangedSubscript(handler);
            Dexterity.PropertyChangedSubscript(handler);
            Constitution.PropertyChangedSubscript(handler);
            Intelligence.PropertyChangedSubscript(handler);
            Wisdom.PropertyChangedSubscript(handler);
            Charisma.PropertyChangedSubscript(handler);
        }
        */
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


        public CharacteristicList(CharacteristicList to_copy)
        {
            MasteryBonus = to_copy.MasteryBonus;
            CharacteristicsList = (List<Characteristic>)to_copy.CharacteristicsList.Clone();
        }

        /// <summary>
        ///     Process Deep copy on the item
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new CharacteristicList(this);
        }

        #endregion

        public bool IsEquivalentTo(CharacteristicList toCompare)
        {
            foreach (Tuple<Characteristic, Characteristic> characteristics in CharacteristicsList.Zip<Characteristic, Characteristic, Tuple<Characteristic, Characteristic>>(toCompare.CharacteristicsList, (x, y) => new Tuple<Characteristic, Characteristic>(x, y)))
            {
                if (!characteristics.Item1.IsEquivalentTo(characteristics.Item2))
                    return false;
            }
            return true;
        }

    }
}
