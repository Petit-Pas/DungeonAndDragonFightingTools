// /***********************************************************************************
//  * ERTMS Solutions
//  ***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDFight.Game.Characteristics
{
    /// <summary>
    ///     Respresents 1 characteristic
    /// </summary>
    public class CharacteristicDataContext : ICloneable
    {
        public CharacteristicDataContext() { }

        public CharacteristicDataContext(string given_name)
        {
            _name = given_name;
        }

        /// <summary>
        ///     Ex: strength, wisdow, etc...
        /// </summary>
        [XmlAttribute]
        public string Name { get => _name; set => _name = value; }
        private string _name;

        /// <summary>
        ///     Says if we need to add the mastery bonus to the Modifier
        /// </summary>
        [XmlAttribute]
        public bool Mastery { get => _mastery; set => _mastery = value; }
        private bool _mastery = false;

        /// <summary>
        ///     the value to add to results of this characteristic
        /// </summary>
        [XmlAttribute]
        public int Modifier { get => _modifier; set => _modifier = value; }
        private int _modifier = 0;

        public object Clone()
        {
            return new CharacteristicDataContext
            {
                Mastery = this.Mastery,
                Modifier = this.Modifier,
                Name = (string)this.Name.Clone(),
            };
        }
    }
}
