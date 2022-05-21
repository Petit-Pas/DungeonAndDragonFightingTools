using System;
using BaseToolsLibrary;
using DnDToolsLibrary.Status;
using System.Linq;
using System.Net.NetworkInformation;
using System.Xml.Serialization;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;

namespace DnDToolsLibrary.Entities
{
    /// <summary>
    ///     Represents a Character for D&D (not to confound with Monsters)
    /// </summary>
    public class Character : PlayableEntity, IEquivalentComparable<Character>
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
            {
                TransformBack();
            }
            if (IsFocused)
            {
                var loseConcentrationCommand = new LoseConcentrationCommand(this.DisplayName);
                _mediator.Execute(loseConcentrationCommand);
            }
            InitiativeRoll = 0;
            var affectingStatus = _statusProvider.GetOnHitStatusesAppliedOn(DisplayName).ToArray();
            foreach (var status in affectingStatus)
            {
                if (status.HasEndCondition())
                {
                    var removeStatusCommand = new RemoveStatusCommand(status.Id, DisplayName);
                    _mediator.Execute(removeStatusCommand);
                }
            }
        }

        #region IClonable

        private void init_copy(Character to_copy)
        {
            Level = to_copy.Level;
            HasInspiration = to_copy.HasInspiration;
        }

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

        public override void CopyAssign(object to_copy)
        {
            base.CopyAssign(to_copy);
            init_copy(to_copy as Character);
        }

        public bool IsEquivalentTo(Character toCompare)
        {
            if (!base.IsEquivalentTo(toCompare))
                return false;
            if (Level != toCompare.Level)
                return false;
            if (HasInspiration != toCompare.HasInspiration)
                return false;
            return true;
        }
        #endregion IClonable

        ~Character()
        {
        }

    }
}
