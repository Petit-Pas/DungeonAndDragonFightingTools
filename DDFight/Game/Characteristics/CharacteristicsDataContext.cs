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
            if (!GlobalVariables.Loading)
                initCharacteristicsList();
        }

        private void initCharacteristicsList()
        {
            _characteristicsList = new List<CharacteristicDataContext>();
            _characteristicsList.Add(new CharacteristicDataContext(CharacteristicsEnum.Strength));
            _characteristicsList.Add(new CharacteristicDataContext(CharacteristicsEnum.Dexterity));
            _characteristicsList.Add(new CharacteristicDataContext(CharacteristicsEnum.Constitution));
            _characteristicsList.Add(new CharacteristicDataContext(CharacteristicsEnum.Intelligence));
            _characteristicsList.Add(new CharacteristicDataContext(CharacteristicsEnum.Wisdom));
            _characteristicsList.Add(new CharacteristicDataContext(CharacteristicsEnum.Charisma));
        }

        #region characteristics

        public List<CharacteristicDataContext> CharacteristicsList
        {
            get {
                return _characteristicsList;
            } 
            set 
            {
                Console.WriteLine("COCHON set list, " + value.Count);
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
            Console.WriteLine("COCHON copy constructor");

            /*Charisma = (CharacteristicDataContext)to_copy._charisma.Clone();
            Constitution = (CharacteristicDataContext)to_copy._constitution.Clone();
            Dexterity = (CharacteristicDataContext)to_copy._dexterity.Clone();
            Intelligence = (CharacteristicDataContext)to_copy._intelligence.Clone();
            Strength = (CharacteristicDataContext)to_copy._strength.Clone();
            Wisdom = (CharacteristicDataContext)to_copy._wisdom.Clone();*/

            MasteryBonus = to_copy.MasteryBonus;
            Console.WriteLine("COCHON: " + to_copy.CharacteristicsList.Count);
            CharacteristicsList = (List<CharacteristicDataContext>)to_copy.CharacteristicsList.Clone();
            Console.WriteLine("COCHON: " + CharacteristicsList[0].Name + " " + CharacteristicsList[0].Modifier);
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

    static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
