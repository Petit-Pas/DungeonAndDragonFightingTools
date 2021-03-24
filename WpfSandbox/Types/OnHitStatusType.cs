using DDFight;
using DDFight.Game.Characteristics;
using DDFight.Game.Status;
using DDFight.Tools;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WpfSandbox.Types
{
    public class OnHitStatusType : CustomVerboseStatusType, IEventUnregisterable
    {

        public static OnHitStatus Convert(OnHitStatusType status)
        {
            OnHitStatus result = new OnHitStatus()
            {
                ApplySavingCharacteristic = status.ApplySavingCharacteristic,
                ApplySavingDifficulty = status.ApplySavingDifficulty,
                CanRedoSavingThrow = status.CanRedoSavingThrow,
                Description = status.Description,
                DisplayName = "",
                Caster = null,
                DotDamageList = DotTemplateType.ConvertList(status.DotDamageList),
                DurationIsBasedOnStartOfTurn = status.DurationIsBasedOnStartOfTurn,
                DurationIsCalculatedOnCasterTurn = status.DurationIsCalculatedOnCasterTurn,
                EndsOnCasterLossOfConcentration = status.EndsOnCasterLossOfConcentration,
                HasAMaximumDuration = status.HasAMaximumDuration,
                HasApplyCondition = status.HasApplyCondition,
                HasSpellSaving = status.HasSpellSaving,
                Header = status.Header,
                OnApplyDamageList = DamageTemplateType.ConvertList(status.OnApplyDamageList),
                RemainingRounds = status.RemainingRounds,
                SavingIsRemadeAtStartOfTurn = status.SavingIsRemadeAtStartOfTurn,
                SpellApplicationModifier = status.SpellApplicationModifier,
                SpellSavingWasSuccessful = status.SpellSavingWasSuccessful,
                ToolTip = status.ToolTip,
            };
            return result;
        }
        public OnHitStatusType()
        {
        }

        /// <summary>
        ///     The Entity that initiated the status, can be used when its concentration loss provokes the annulation of the status
        /// </summary>
        [XmlIgnore]
        public PlayableEntityType Caster = null;

        /// <summary>
        ///     The Entity that is affected by the status, can be used to remove the status from its list of Statuses   
        /// </summary>
        [XmlIgnore]
        public PlayableEntityType Affected = null;

        #region Spells

        /* WARNING : these (in Spells region) are only used when the status is applied by a spell
                     with a spell you can have a Saving Throw (on the spell, not the status)
                     Is is different than the Saving Throw you could set in the OnHitStatus itself
                     You could resist a Status from the Saving of the spell, not the saving of the Status itself
        */

        /// <summary>
        ///     Means it is applied by a spell that has a Saving Throw
        /// </summary>
        [XmlAttribute]
        public bool HasSpellSaving
        {
            get => _hasSpellSaving;
            set
            {
                _hasSpellSaving = value;
                NotifyPropertyChanged();
            }
        }
        private bool _hasSpellSaving = false;

        [XmlAttribute]
        public ApplicationModifierEnum SpellApplicationModifier
        {
            get => _spellApplicationModifier;
            set
            {
                _spellApplicationModifier = value;
                NotifyPropertyChanged();
            }
        }
        private ApplicationModifierEnum _spellApplicationModifier = ApplicationModifierEnum.Maintained;

        [XmlIgnore]
        public bool SpellSavingWasSuccessful
        {
            get => _spellSavingWasSuccessful;
            set
            {
                _spellSavingWasSuccessful = value;
                NotifyPropertyChanged();
            }
        }
        private bool _spellSavingWasSuccessful = false;

        #endregion Spells

        #region Apply Properties

        [XmlAttribute]
        public bool HasApplyCondition
        {
            get => _hasApplyCondition;
            set
            {
                _hasApplyCondition = value;
                NotifyPropertyChanged();
            }
        }
        private bool _hasApplyCondition = false;

        [XmlAttribute]
        public CharacteristicsEnum ApplySavingCharacteristic
        {
            get => _applySavingCharacteristic;
            set
            {
                _applySavingCharacteristic = value;
                NotifyPropertyChanged();
            }
        }
        private CharacteristicsEnum _applySavingCharacteristic = CharacteristicsEnum.Wisdom;

        // 0 for default spellcasting ability DC
        [XmlAttribute]
        public int ApplySavingDifficulty
        {
            get => _applySavingDifficulty;
            set
            {
                _applySavingDifficulty = value;
                NotifyPropertyChanged();
            }
        }
        private int _applySavingDifficulty = 0;

        #endregion Apply Properties

        #region Damage

        #region ApplyDamage

        [XmlArrayItem("DamageTemplate", typeof(DamageTemplateType))]
        public List<DamageTemplateType> OnApplyDamageList
        {
            get => _onApplyDamageList;
            set
            {
                _onApplyDamageList = value;
                NotifyPropertyChanged();
            }
        }
        private List<DamageTemplateType> _onApplyDamageList = new List<DamageTemplateType>();

        #endregion ApplyDamage

        #region DotDamage

        [XmlArrayItem("DotTemplate", typeof(DotTemplateType))]
        public List<DotTemplateType> DotDamageList
        {
            get => _dotDamageList;
            set
            {
                _dotDamageList = value;
                NotifyPropertyChanged();
            }
        }
        private List<DotTemplateType> _dotDamageList = new List<DotTemplateType>();

        #endregion DotDamage

        #endregion Damage

        #region EndConditions

        #region Maximum Duration

        [XmlAttribute]
        public bool HasAMaximumDuration
        {
            get => _hasAMaximumDuration;
            set
            {
                _hasAMaximumDuration = value;
                NotifyPropertyChanged();
            }
        }
        private bool _hasAMaximumDuration = false;

        public bool DurationIsCalculatedOnCasterTurn
        {
            get => _durationIsCalculatedOnCasterTurn;
            set
            {
                _durationIsCalculatedOnCasterTurn = value;
                NotifyPropertyChanged();
            }
        }
        private bool _durationIsCalculatedOnCasterTurn = true;

        public bool DurationIsBasedOnStartOfTurn
        {
            get => _durationIsBasedOnStartOfTurn;
            set
            {
                _durationIsBasedOnStartOfTurn = value;
                NotifyPropertyChanged();
            }
        }
        private bool _durationIsBasedOnStartOfTurn = false;

        [XmlAttribute]
        public int RemainingRounds
        {
            get => _remainingRounds;
            set
            {
                _remainingRounds = value;
                NotifyPropertyChanged();
            }
        }
        private int _remainingRounds = 0;

        /// <summary>
        ///     removes 1 turn from the Remaining rounds variable
        ///     if the status expires, the function removes it from the target of the status
        /// </summary>
        /// <returns></returns>

        #endregion Maximum Duration

        #region Saving can be remade

        [XmlAttribute]
        public bool CanRedoSavingThrow
        {
            get => _canRedoSavingThrow;
            set
            {
                _canRedoSavingThrow = value;
                NotifyPropertyChanged();
            }
        }
        private bool _canRedoSavingThrow = false;

        /// <summary>
        ///     This is only used if CanRedoSavingThrow is set to True
        ///         If this value is true ---> the saving is remade at start of turn
        ///         If this value is false --> the saving is remade at end of turn
        /// </summary>
        public bool SavingIsRemadeAtStartOfTurn
        {
            get => _savingIsRemadeAtStartOfTurn;
            set
            {
                _savingIsRemadeAtStartOfTurn = value;
                NotifyPropertyChanged();
            }
        }
        private bool _savingIsRemadeAtStartOfTurn = true;
        #endregion  Saving can be remade

        #region Concentration Loss

        [XmlAttribute]
        public bool EndsOnCasterLossOfConcentration
        {
            get => _endsOnCasterLossOfConcentration;
            set
            {
                _endsOnCasterLossOfConcentration = value;
                NotifyPropertyChanged();
            }
        }
        private bool _endsOnCasterLossOfConcentration = false;

        #endregion Concentration

        #region End status events
        #endregion End status events


        /// <summary>
        ///     In this method implements any "end of condition" such as : 
        ///     - after n turns
        ///     - after a saving throw has been successfully remade
        ///     - after 10 rounds
        ///     - etc...
        ///     
        ///     Will be used as such condition can be vanished by the end of a fight
        /// </summary>
        /// <returns> true if so, false otherwise </returns>
        public bool HasEndCondition()
        {
            if (EndsOnCasterLossOfConcentration)
                return true;
            if (CanRedoSavingThrow)
                return true;
            // if more than 50 rounds remain (5 mins), it is left on the characters, in case a second fight comes close
            if (HasAMaximumDuration && RemainingRounds < 50)
                return true;
            return false;
        }


        #region ICloneable

        public override object Clone()
        {
            return new OnHitStatusType(this);
        }

        protected void init_copy(OnHitStatusType to_copy)
        {
            base.init_copy(to_copy);
            HasApplyCondition = to_copy.HasApplyCondition;
            ApplySavingCharacteristic = to_copy.ApplySavingCharacteristic;
            ApplySavingDifficulty = to_copy.ApplySavingDifficulty;
            EndsOnCasterLossOfConcentration = to_copy.EndsOnCasterLossOfConcentration;
            CanRedoSavingThrow = to_copy.CanRedoSavingThrow;
            SavingIsRemadeAtStartOfTurn = to_copy.SavingIsRemadeAtStartOfTurn;
            RemainingRounds = to_copy.RemainingRounds;
            HasAMaximumDuration = to_copy.HasAMaximumDuration;
            DurationIsCalculatedOnCasterTurn = to_copy.DurationIsCalculatedOnCasterTurn;
            DurationIsBasedOnStartOfTurn = to_copy.DurationIsBasedOnStartOfTurn;
            OnApplyDamageList = (List<DamageTemplateType>)to_copy.OnApplyDamageList.Clone();
            DotDamageList = (List<DotTemplateType>)to_copy.DotDamageList.Clone();
            HasSpellSaving = to_copy.HasSpellSaving;
            SpellApplicationModifier = to_copy.SpellApplicationModifier;
            SpellSavingWasSuccessful = to_copy.SpellSavingWasSuccessful;
        }

        public OnHitStatusType(OnHitStatusType to_copy)
        {
            init_copy(to_copy);
        }

        public void CopyAssign(OnHitStatusType to_copy)
        {
            init_copy(to_copy);
        }

        public void UnregisterToAll()
        {
        }
        #endregion ICloneable

        #endregion ICloneable
    }
}

