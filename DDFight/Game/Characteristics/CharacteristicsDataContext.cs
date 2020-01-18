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
using System.Xml.Serialization;

namespace DDFight.Game.Characteristics
{
    /// <summary>
    ///     Contains all the characteristic of a character
    /// </summary>
    public class CharacteristicsDataContext : ICloneable, INotifyPropertyChanged  /*, INotifyPropertyChangedSub*/
    {

        public CharacteristicsDataContext()
        {
            if (!Global.Loading)
                initCharacteristicsList();
        }

        private void initCharacteristicsList()
        {
            _characteristicsList = new List<CharacteristicDataContext>
            {
                new CharacteristicDataContext(CharacteristicsEnum.Strength),
                new CharacteristicDataContext(CharacteristicsEnum.Dexterity),
                new CharacteristicDataContext(CharacteristicsEnum.Constitution),
                new CharacteristicDataContext(CharacteristicsEnum.Intelligence),
                new CharacteristicDataContext(CharacteristicsEnum.Wisdom),
                new CharacteristicDataContext(CharacteristicsEnum.Charisma)
            };
        }

        #region characteristics

        public int GetCharacteristicModifier(CharacteristicsEnum type)
        {
            return CharacteristicsList.First(x => x.Name == CharacteristicsEnum.Dexterity).Modifier;
        }

        public List<CharacteristicDataContext> CharacteristicsList
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
        private List<CharacteristicDataContext> _characteristicsList = null;

        [XmlAttribute]
        /// <summary>
        ///     The amount to add to mastered characteristics
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


        public CharacteristicsDataContext(CharacteristicsDataContext to_copy)
        {
            MasteryBonus = to_copy.MasteryBonus;
            CharacteristicsList = (List<CharacteristicDataContext>)to_copy.CharacteristicsList.Clone();
        }

        /// <summary>
        ///     Process Deep copy on the item
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new CharacteristicsDataContext(this);
        }

        #endregion
    }
}
