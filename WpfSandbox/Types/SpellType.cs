using DDFight;
using DDFight.Game.Aggression;
using DDFight.Game.Aggression.Spells;
using DDFight.Game.Characteristics;
using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfSandbox.Types
{
    [XmlRoot(ElementName = "Spell")]
    public class SpellType : AAttackTemplateType, ICloneable, INameable, IWindowEditable
    {

        public static Spell Convert(SpellType spell)
        {
            Spell result = new Spell()
            {
                AdditionalHitDamagePerLevel = (DamageTemplateList) DamageTemplateType.ConvertList(spell.AdditionalHitDamagePerLevel),
                AdditionalTargetPerLevel = spell.AdditionalTargetPerLevel,
                AmountTargets = spell.AmountTargets,
                AppliedStatus = OnHitStatusListType.Convert(spell.AppliedStatus),
                AutomaticalyHits = spell.AutomaticalyHits,
                BaseLevel = spell.BaseLevel,
                CanBeCastAtHigherLevel = spell.CanBeCastAtHigherLevel,
                CanSelectSameTargetTwice = spell.CanSelectSameTargetTwice,
                Description = spell.Description,
                DisplayName = spell.DisplayName,
                HasSavingThrow = spell.HasSavingThrow,
                HitDamage = DamageTemplateType.ConvertList(spell.HitDamage),
                HitRollBonus = spell.HitRollBonus,
                IsAnAttack = spell.IsAnAttack,
                IsCloseRanged = spell.IsCloseRanged,
                IsLongRanged = spell.IsLongRanged,
                Name = spell.Name,
                Range = spell.Range,
                SavingCharacteristic = spell.SavingCharacteristic,
                SavingDifficulty = spell.SavingDifficulty,
            };
            return result;
        }


        public SpellType() : base()
        {
        }

        public string GetName() { return Name; }

        public void CastSpell(PlayableEntityType caster)
        {
        }


        #region Properties

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChanged();
            }
        }
        private string _description = "";

        [XmlArrayItem("DamageTemplate", typeof(DamageTemplateType))]
        public List<DamageTemplateType> HitDamage
        {
            get => _hitDamage;
            set
            {
                _hitDamage = value;
                NotifyPropertyChanged();
            }
        }
        private List<DamageTemplateType> _hitDamage = new List<DamageTemplateType>();

        public OnHitStatusListType AppliedStatus
        {
            get => _appliedStatus;
            set
            {
                _appliedStatus = value;
                NotifyPropertyChanged();
            }
        }
        private OnHitStatusListType _appliedStatus = new OnHitStatusListType();

        #region Level

        public bool CanBeCastAtHigherLevel
        {
            get => _canBeCastAtHigherLevel;
            set
            {
                if (_canBeCastAtHigherLevel != value)
                {
                    _canBeCastAtHigherLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _canBeCastAtHigherLevel = false;

        public int AdditionalTargetPerLevel
        {
            get => _additionalTargetPerLevel;
            set
            {
                if (_additionalTargetPerLevel != value)
                {
                    _additionalTargetPerLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _additionalTargetPerLevel = 0;

        [XmlArrayItem("DamageTemplate", typeof(DamageTemplateType))]
        public List<DamageTemplateType> AdditionalHitDamagePerLevel
        {
            get => _additionalHitDamagePerLevel;
            set
            {
                _additionalHitDamagePerLevel = value;
                NotifyPropertyChanged();
            }
        }
        private List<DamageTemplateType> _additionalHitDamagePerLevel = new List<DamageTemplateType>();

        public int BaseLevel
        {
            get => _baseLevel;
            set
            {
                _baseLevel = value;
                NotifyPropertyChanged();
            }
        }
        private int _baseLevel = 0;

        #endregion Level

        /// <summary>
        ///     0 means uncapped
        /// </summary>
        public int AmountTargets
        {
            get => _amountTargets;
            set
            {
                _amountTargets = value;
                NotifyPropertyChanged();
            }
        }
        private int _amountTargets = 0;

        public bool CanSelectSameTargetTwice
        {
            get => _canSelectSameTargetTwice;
            set
            {
                _canSelectSameTargetTwice = value;
                NotifyPropertyChanged();
            }
        }
        private bool _canSelectSameTargetTwice = false;

        /// <summary>
        ///     tells wether or not there is an attack roll (opposed to saving throw OR automatic success)
        /// </summary>
        public bool IsAnAttack
        {
            get => _isAnAttack;
            set
            {
                _isAnAttack = value;
                NotifyPropertyChanged();
            }
        }
        private bool _isAnAttack = false;

        #region Attack_Spell
        public int HitRollBonus
        {
            get => _hitRollBonus;
            set
            {
                if (_hitRollBonus != value)
                {
                    _hitRollBonus = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _hitRollBonus = 0;

        /// <summary>
        ///     this is just there for magic missile :(
        /// </summary>
        public bool AutomaticalyHits
        {
            get => _automaticalyHits;
            set
            {
                if (_automaticalyHits != value)
                {
                    _automaticalyHits = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _automaticalyHits = false;

        #endregion Attack_Spell

        #region SavingThrow

        public bool HasSavingThrow
        {
            get => _hasSavingThrow;
            set
            {
                _hasSavingThrow = value;
                NotifyPropertyChanged();
            }
        }
        private bool _hasSavingThrow = false;

        public CharacteristicsEnum SavingCharacteristic
        {
            get => _savingCharacteristic;
            set
            {
                _savingCharacteristic = value;
                NotifyPropertyChanged();
            }
        }
        private CharacteristicsEnum _savingCharacteristic = CharacteristicsEnum.Dexterity;

        public int SavingDifficulty
        {
            get => _savingDifficulty;
            set
            {
                _savingDifficulty = value;
                NotifyPropertyChanged();
            }
        }
        private int _savingDifficulty = 0;

        #endregion SavingThrow

        #endregion Properties

        #region EditWindow

        [XmlIgnore]
        public bool Validated = false;


        #endregion EditWindow

        #region ICloneable

        private void init_copy(SpellType to_copy)
        {
            this.BaseLevel = to_copy.BaseLevel;
            this.Description = (string)to_copy.Description.Clone();
            this.AmountTargets = to_copy.AmountTargets;
            this.CanSelectSameTargetTwice = to_copy.CanSelectSameTargetTwice;
            this.IsAnAttack = to_copy.IsAnAttack;
            this.HasSavingThrow = to_copy.HasSavingThrow;
            this.SavingCharacteristic = to_copy.SavingCharacteristic;
            this.SavingDifficulty = to_copy.SavingDifficulty;
            this.HitDamage = (List<DamageTemplateType>)to_copy.HitDamage.Clone();
            this.AppliedStatus = (OnHitStatusListType)to_copy.AppliedStatus.Clone();
            this.CanBeCastAtHigherLevel = to_copy.CanBeCastAtHigherLevel;
            this.AdditionalHitDamagePerLevel = (List<DamageTemplateType>)to_copy.AdditionalHitDamagePerLevel.Clone();
            this.AdditionalTargetPerLevel = to_copy.AdditionalTargetPerLevel;
            this.HitRollBonus = to_copy.HitRollBonus;
            this.AutomaticalyHits = to_copy.AutomaticalyHits;
        }

        protected SpellType(SpellType to_copy) : base(to_copy)
        {
            init_copy(to_copy);
        }

        public new object Clone()
        {
            return new SpellType(this);
        }

        public bool OpenEditWindow()
        {
            return false;
        }

                #region ICopyAssignable

        #endregion ICopyAssignable
        #endregion ICloneable
    }
}
