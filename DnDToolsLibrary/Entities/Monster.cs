using BaseToolsLibrary;
using System.Xml.Serialization;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration;

namespace DnDToolsLibrary.Entities
{
    public class Monster : PlayableEntity, IEquivalentComparable<Monster>
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

        private void init_copy(Monster to_copy)
        {
            Level = to_copy.Level;
        }

        protected Monster(Monster to_copy) : base(to_copy)
        {
            init_copy(to_copy);
        }

        /// <summary>
        ///     Method to clone a character (Deep Copy)
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new Monster(this);
        }

        public override void CopyAssign(object to_copy)
        {
            base.CopyAssign(to_copy);
            init_copy(to_copy as Monster);
        }

        /// <summary>
        ///     Should be called after any fight to reset Character
        /// </summary>
        public void GetOutOfFight()
        {
            if (IsFocused)
            {
                var loseConcentrationCommand = new LoseConcentrationCommand(this.DisplayName);
                Mediator.Execute(loseConcentrationCommand);
            }
        }

        ~Monster()
        {
            ;
        }

        #endregion

        public bool IsEquivalentTo(Monster toCompare)
        {
            if (!base.IsEquivalentTo(toCompare))
                return false;
            if (Level != toCompare.Level)
                return false;
            return true;
        }

    }
}
