using DDFight.Game.Entities.Display;
using System.Xml.Serialization;

namespace DDFight.Game.Entities
{
    public class Monster : PlayableEntity
    {
        public Monster()
        {
        }

        #region MonsterProperties

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

        #endregion MonsterProperties

        #region ICloneable

        protected virtual void InitCopy(Monster to_copy)
        {
            base.InitCopy(to_copy);
            Level = to_copy.Level;
        }

        protected Monster(Monster to_copy) : base(to_copy)
        {
            InitCopy(to_copy);
        }

        /// <summary>
        ///     Method to clone a character (Deep Copy)
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new Monster(this);
        }

        #endregion
    }
}
