using System.Xml.Serialization;

namespace DDFight.Game
{
    public class CharacterDataContext
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public uint CA { get; set; }

        [XmlAttribute]
        public uint Initiative { get; set; }

        [XmlAttribute]
        public uint MaxHp { get; set; }

        [XmlAttribute]
        public int Hp { get; set; }

    }
}
