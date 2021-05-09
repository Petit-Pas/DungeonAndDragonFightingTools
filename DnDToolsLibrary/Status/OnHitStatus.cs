using System.Xml.Serialization;
using System;
using BaseToolsLibrary.Tools;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Dice;
using BaseToolsLibrary.Memory;

namespace DnDToolsLibrary.Status
{
    public class OnHitStatus : CustomVerboseStatus, IEventUnregisterable, IDisposable, ICopyAssignable
    {
        /// <summary>
        ///     As event handling is moved to higher layers of the application, these actions allow us to inject Register and Unregister comportment from other layers
        /// </summary>
        public static Action<OnHitStatus> RegisterEvents { get; set; } = null;
        public static Action<OnHitStatus> UnregisterEvents { get; set; } = null;


        public OnHitStatus()
        {
        }

        #region Properties

        #region Properties_Concerned

        /// <summary>
        ///     The Entity that initiated the status, can be used when its concentration loss provokes the annulation of the status
        /// </summary>
        [XmlIgnore]
        public PlayableEntity Caster = null;

        /// <summary>
        ///     The Entity that is affected by the status, can be used to remove the status from its list of Statuses   
        /// </summary>
        [XmlIgnore]
        public PlayableEntity Affected = null;

        #endregion Properties_Concerned

        #region Properties_ApplyCondition

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

        #endregion Properties_ApplyCondition

        #region Properties_Damage

        #region Properties_Damage_OnApply

        public DamageTemplateList OnApplyDamageList
        {
            get => _onApplyDamageList;
            set
            {
                _onApplyDamageList = value;
                NotifyPropertyChanged();
            }
        }
        private DamageTemplateList _onApplyDamageList = new DamageTemplateList();

        #endregion Properties_Damage_OnApply

        #region Properties_Damage_Dot

        public DotTemplateList DotDamageList
        {
            get => _dotDamageList;
            set
            {
                _dotDamageList = value;
                NotifyPropertyChanged();
            }
        }
        private DotTemplateList _dotDamageList = new DotTemplateList();

        #endregion Properties_Damage_Dot

        #endregion Properties_Damage

        #region Properties_EndCondition

        #region Properties_EndCondition_MaximumDuration
        
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

        [XmlAttribute]
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

        [XmlAttribute]
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

        #endregion Properties_EndCondition_MaximumDuration

        #region Properties_EndCondition_SavingRemade
        
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
        ///     Only used if CanRedoSavingThrow is set to true
        ///     true ==> start of turn
        ///     false ==> end of turn
        /// </summary>
        [XmlAttribute]
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

        #endregion properties_EndCondition_SavingRemade

        #region Properties_EndConditions_Concentration

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

        #endregion Properties_EndConditions_Concentration

        #endregion Properties_EndCondition

        #endregion Properties

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

        /// <summary>
        ///     Returns a saving throw object to know if the status is applied
        /// </summary>
        /// <param name="caster"> the one that tries to apply the status </param>
        /// <param name="target"> the target of the status </param>
        /// <returns></returns>
        public SavingThrow GetSavingThrow(PlayableEntity caster, PlayableEntity target)
        {
            SavingThrow result = new SavingThrow
            {
                Characteristic = this.ApplySavingCharacteristic,
                Difficulty = this.ApplySavingDifficulty != 0 ? this.ApplySavingDifficulty : caster.SpellSave,
                Target = target,
            };
            return result;
        }

        #region ICloneable

        public override object Clone()
        {
            return new OnHitStatus(this);
        }

        protected void init_copy(OnHitStatus to_copy)
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
            OnApplyDamageList = (DamageTemplateList)to_copy.OnApplyDamageList.Clone();
            DotDamageList = (DotTemplateList)to_copy.DotDamageList.Clone();
            HasSpellSaving = to_copy.HasSpellSaving;
            SpellApplicationModifier = to_copy.SpellApplicationModifier;
            SpellSavingWasSuccessful = to_copy.SpellSavingWasSuccessful;
            Caster = to_copy.Caster;
            Affected = to_copy.Affected;
        }

        public OnHitStatus(OnHitStatus to_copy)
        {
            init_copy(to_copy);
            RegisterEvents(this);
        }

        public override void CopyAssign(object to_copy)
        {
            if (to_copy is OnHitStatus status)
            {
                UnregisterEvents(this);
                init_copy(status);
                RegisterEvents(this);
            }
        }

        ~OnHitStatus()
        {
        }

        public void Dispose()
        {
            UnregisterEvents(this);
        }

        public void Unregister()
        {
            UnregisterEvents(this);
        }

        #endregion ICloneable
    }
}
