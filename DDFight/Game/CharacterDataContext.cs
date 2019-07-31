using DDFight.Game.Characteristics;
using DDFight.Tools;
using System.Xml.Serialization;

namespace DDFight.Game
{
    /// <summary>
    ///     Represents a Character for D&D (not to confound with Monsters)
    /// </summary>
    public class CharacterDataContext : IClonable
    {
        /// <summary>
        ///     This is used when the CharacterDataContext is used as a DataContext for an edit window. If the user Cancelled the operation, it is set to false.
        /// </summary>
        public bool Validated = false;

        /// <summary>
        ///     Name of the Character
        /// </summary>
        [XmlAttribute]
        public string Name { get => _name; set => _name = value; }
        private string _name = "Name";

        /// <summary>
        ///     Armor Class
        /// </summary>
        [XmlAttribute]
        public uint CA { get => _cA; set => _cA = value; }
        private uint _cA = 10;

        /// <summary>
        ///     Max Hps that this character can have (Temporary Hps not taken into account)
        /// </summary>
        [XmlAttribute]
        public uint MaxHp { get => _maxHp; set => _maxHp = value; }
        private uint _maxHp = 0;

        /// <summary>
        ///     Current Hps (Temporary Hps not taken into account)
        /// </summary>
        [XmlAttribute]
        public int Hp { get => _hp; set => _hp = value; }
        private int _hp = 0;

        /// <summary>
        ///     The mastery Bonus to apply on mastered Characteristics
        /// </summary>
        [XmlAttribute]
        public uint MasteryBonus { get => _masteryBonus; set => _masteryBonus = value; }
        private uint _masteryBonus = 0;

        /// <summary>
        ///     The characteristics of the character
        /// </summary>
        public CharacteristicsDataContext Characteristics { get => _characteristics; set => _characteristics = value; }
        private CharacteristicsDataContext _characteristics = new CharacteristicsDataContext();

        /// <summary>
        ///     Method to clone a character (Deep Copy)
        /// </summary>
        /// <returns></returns>
        public IClonable Clone()
        {
            return new CharacterDataContext
            {
                Name = (string)this.Name.Clone(),
                CA = this.CA,
                Hp = this.Hp,
                MaxHp = this.MaxHp,
                Characteristics = (CharacteristicsDataContext)this.Characteristics.Clone(),
            };
        }
    }
}
