using DDFight.Windows;
using System.Windows;
using System.Xml.Serialization;

namespace DDFight.Game
{
    /// <summary>
    ///     Represents a Character for D&D (not to confound with Monsters)
    /// </summary>
    public class CharacterDataContext : PlayableEntity /*, INotifyPropertyChangedSub*/
    {
        public CharacterDataContext() : base()
        {
        }

        #region CharacterProperties

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

        public override string DisplayName
        {
            get => Name;
        }

        #endregion

        public override void OpenEditWindow()
        {
            EditCharacterWindow window = new EditCharacterWindow();
            CharacterDataContext temporary = (CharacterDataContext)this.Clone();
            window.DataContext = temporary;

            window.ShowDialog();

            if (temporary.Validated == true)
            {
                this.CopyAssign(temporary);
            }
        }

    private void init_copy(CharacterDataContext to_copy)
    {
        Level = to_copy.Level;
    }

        #region IClonable

        /// <summary>
        ///     Copy Ctor, required for the Clone method to work properly with derived classes
        /// </summary>
        /// <param name=""></param>
        protected CharacterDataContext(CharacterDataContext to_copy) : base(to_copy)
        {
            init_copy(to_copy);
        }

        public override object Clone()
        {
            return new CharacterDataContext(this);
        }

        #endregion

        public override void CopyAssign (object _to_copy)
        {
            base.CopyAssign(_to_copy);
            init_copy((CharacterDataContext)_to_copy);
        }
    }
}
