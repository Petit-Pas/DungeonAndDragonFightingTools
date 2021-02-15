using DDFight.Game.Entities.Display;
using DDFight.Game.Status;
using System.Linq;
using System.Xml.Serialization;

namespace DDFight.Game.Entities
{
    /// <summary>
    ///     Represents a Character for D&D (not to confound with Monsters)
    /// </summary>
    public class Character : PlayableEntity
    {
        public Character() : base()
        {
        }

        #region CharacterProperties

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

        #endregion

        public void GetOutOfFight()
        {
            if (IsTransformed)
                TransformBack();
            InitiativeRoll = 0;
            for (int i = 0; i != CustomVerboseStatusList.List.Count; i++)
            {
                CustomVerboseStatus status = CustomVerboseStatusList.List.ElementAt(i);
                if (status.GetType() == typeof(OnHitStatus))
                {
                    OnHitStatus onHit = (OnHitStatus)status;
                    if (onHit.HasEndCondition())
                    {
                        onHit.UnregisterToAll();
                        CustomVerboseStatusList.List.Remove(status);
                        i--;
                    }
                }
            }
        }

        public override bool Edit()
        {
            PlayableEntityEditWindow window = new PlayableEntityEditWindow();
            Character temporary = (Character)this.Clone();
            window.DataContext = temporary;
            window.ShowCentered();

            if (temporary.Validated == true)
            {
                this.CopyAssign(temporary);
                return true;
            }
            return false;
        }

    private void init_copy(Character to_copy)
    {
        Level = to_copy.Level;
        HasInspiration = to_copy.HasInspiration;
    }

        #region IClonable

        /// <summary>
        ///     Copy Ctor, required for the Clone method to work properly with derived classes
        /// </summary>
        /// <param name=""></param>
        protected Character(Character to_copy) : base(to_copy)
        {
            init_copy(to_copy);
        }

        public override object Clone()
        {
            return new Character(this);
        }

        #endregion

        public override void CopyAssign (object _to_copy)
        {
            base.CopyAssign(_to_copy);
            if (_to_copy.GetType() == this.GetType())
                init_copy((Character)_to_copy);
        }
    }
}
