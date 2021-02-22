using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Characteristics;
using DDFight.Game.Counters;
using DDFight.Game.DamageAffinity;
using DDFight.Game.Status;
using DDFight.Tools;
using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
