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

        #endregion CharacterProperties

        /// <summary>
        ///     Should be called after any fight to reset Character
        /// </summary>
        public void GetOutOfFight()
        {
            if (IsTransformed)
                TransformBack();
            InitiativeRoll = 0;
            for (int i = 0; i != CustomVerboseStatusList.Elements.Count; i++)
            {
                CustomVerboseStatus status = CustomVerboseStatusList.Elements.ElementAt(i);
                if (status.GetType() == typeof(OnHitStatus))
                {
                    OnHitStatus onHit = (OnHitStatus)status;
                    if (onHit.HasEndCondition())
                    {
                        onHit.UnregisterToAll();
                        CustomVerboseStatusList.RemoveElement(status);
                        i--;
                    }
                }
            }
        }

        #region IClonable

        protected override void InitCopy(PlayableEntity to_copy)
        {
            Character copy_character = to_copy as Character;

            base.InitCopy(copy_character);
            Level = copy_character.Level;
            HasInspiration = copy_character.HasInspiration;
        }

        /// <summary>
        ///     Copy Ctor, required for the Clone method to work properly with derived classes
        /// </summary>
        /// <param name=""></param>
        protected Character(Character to_copy) : base(to_copy)
        {
            InitCopy(to_copy);
        }

        public override object Clone()
        {
            return new Character(this);
        }

        #endregion IClonable

    }
}
