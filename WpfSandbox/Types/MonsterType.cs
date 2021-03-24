using System.Xml.Serialization;

namespace WpfSandbox.Types
{
    [XmlRoot(elementName: "Monster")]
    public class MonsterType : PlayableEntityType
    {
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
    }
}
