using DDFight.Windows;
using System.Xml.Serialization;

namespace DDFight.Game
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

        #endregion

        public override bool OpenEditWindow()
        {
            EditMonsterWindow window = new EditMonsterWindow();
            Monster temporary = (Monster)this.Clone();
            window.DataContext = temporary;

            window.ShowDialog();

            if (temporary.Validated == true)
            {
                this.CopyAssign(temporary);
                return true;
            }
            return false;
        }

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
            init_copy((Monster)to_copy);
        }

        #endregion
    }
}
