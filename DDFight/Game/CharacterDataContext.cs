using System.Xml.Serialization;

namespace DDFight.Game
{
    public class CharacterDataContext
    {
        [XmlAttribute]
        public string Name { get => _name; set => _name = value; }
        private string _name = "Name";

        [XmlAttribute]
        public uint CA { get => _cA; set => _cA = value; }
        private uint _cA = 10;

        /*[XmlAttribute]
        public uint Initiative { get => _initiative; set => _initiative = value; }
        private uint _initiative = 0;*/

        [XmlAttribute]
        public uint MaxHp { get => _maxHp; set => _maxHp = value; }
        private uint _maxHp = 0;

        [XmlAttribute]
        public int Hp { get => _hp; set => _hp = value; }
        private int _hp = 0;

    }
}
