// /***********************************************************************************
//  * ERTMS Solutions
//  ***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Characteristics
{
    /// <summary>
    ///     Contains all the characteristic of a character
    /// </summary>
    public class CharacteristicsDataContext : ICloneable
    {
        /// <summary>
        ///     Strength
        /// </summary>
        public CharacteristicDataContext Strength { get => _strength; set => _strength = value; }
        private CharacteristicDataContext _strength = new CharacteristicDataContext("Strength");

        /// <summary>
        ///     Dexterity
        /// </summary>
        public CharacteristicDataContext Dexterity { get => _dexterity; set => _dexterity = value; }
        private CharacteristicDataContext _dexterity = new CharacteristicDataContext("Dexterity");

        /// <summary>
        ///     Constitution
        /// </summary>
        public CharacteristicDataContext Constitution { get => _constitution; set => _constitution = value; }
        private CharacteristicDataContext _constitution = new CharacteristicDataContext("Constitution");

        /// <summary>
        ///     Intelligence
        /// </summary>
        public CharacteristicDataContext Intelligence { get => _intelligence; set => _intelligence = value; }
        private CharacteristicDataContext _intelligence = new CharacteristicDataContext("Intelligence");

        /// <summary>
        ///     Wisdom
        /// </summary>
        public CharacteristicDataContext Wisdom { get => _wisdom; set => _wisdom = value; }
        private CharacteristicDataContext _wisdom = new CharacteristicDataContext("Wisdom");

        /// <summary>
        ///     Charisma
        /// </summary>
        public CharacteristicDataContext Charisma { get => _charisma; set => _charisma = value; }
        private CharacteristicDataContext _charisma = new CharacteristicDataContext("Charisma");

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
    }
}
