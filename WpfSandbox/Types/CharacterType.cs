using System.Xml.Serialization;

namespace WpfSandbox.Types
{
    [XmlRoot(elementName: "Character")]
    public class CharacterType : PlayableEntityType
    {

        [XmlAttribute]
        public bool HasInspiration
        {
            get => _hasInpiration;
            set
            {
                _hasInpiration = value;
                NotifyPropertyChanged();
            }
        }
        private bool _hasInpiration = false;

        /// <summary>
        ///     Level of the Character
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
