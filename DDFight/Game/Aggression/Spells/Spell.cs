using DDFight.Game.Aggression.Spells.Display;
using DDFight.Game.Characteristics;
using DDFight.Game.Fight.Display;
using DDFight.Game.Status;
using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace DDFight.Game.Aggression.Spells
{
    public class Spell : AAttackTemplate, ICloneable, INameable
    {
        public Spell () : base()
        {
        }

        public string GetName() { return Name; }

        public void CastSpell(PlayableEntity Caster)
        {
            int level = 4;
            int additional_levels = level - this.BaseLevel;
            int amountTargets = this.AmountTargets;

            if (amountTargets != 0)
                for (int i = additional_levels; i > 0; i--)
                {
                    amountTargets += this.AdditionalTargetPerLevel;
                }

            FightingEntityListSelectableWindow targetWindow = new FightingEntityListSelectableWindow {
                MaximumSelected = amountTargets,
                CanSelectSameTargetTwice = this.CanSelectSameTargetTwice,
            };
            targetWindow.ShowCentered();

            if (targetWindow.Validated == true)
            {
                if (IsAnAttack)
                {
                    //TODO implement this
                }
                else
                {
                    SpellNonAttackCastWindow window = new SpellNonAttackCastWindow() { 
                        DataContext = this.GetNonAttackSpellResult(Caster, targetWindow.Selected, additional_levels) 
                    };
                    window.ShowCentered();
                }
            }
        }

        public NonAttackSpellResult GetNonAttackSpellResult(PlayableEntity caster, ObservableCollection<PlayableEntity> targets, int additional_levels)
        {
            NonAttackSpellResult template = new NonAttackSpellResult {
                HitDamage = (List<DamageTemplate>)this.HitDamage.Clone<DamageTemplate>(),
                AppliedStatusList = (OnHitStatusList)this.AppliedStatus.Clone(),
                Caster = caster,
                HasSavingThrow = this.HasSavingThrow,
                SavingCharacteristic = this.SavingCharacteristic,
                SavingDifficulty = (this.SavingDifficulty == 0 ? caster.SpellSave : this.SavingDifficulty),
                Targets = targets,
                Name = this.Name,
                Level = this.BaseLevel + additional_levels,
            };

            for (int i = additional_levels; i > 0; i--)
            {
                foreach (DamageTemplate damageTemplate in AdditionalHitDamagePerLevel)
                {
                    bool added = false;
                    foreach (DamageTemplate onHitTemplate in template.HitDamage)
                    {
                        if (onHitTemplate.IsSameKind(damageTemplate))
                        {
                            onHitTemplate.Add(damageTemplate);
                            added = true;
                            break;
                        }
                    }
                    if (added == false)
                        template.HitDamage.Add(damageTemplate);
                }
            }
            return template;
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

        public List<DamageTemplate> HitDamage
        {
            get => _hitDamage;
            set
            {
                _hitDamage = value;
                NotifyPropertyChanged();
            }
        }
        private List<DamageTemplate> _hitDamage = new List<DamageTemplate>();

        public OnHitStatusList AppliedStatus 
        {
            get => _appliedStatus;
            set
            {
                _appliedStatus = value;
                NotifyPropertyChanged();
            }
        }
        private OnHitStatusList _appliedStatus = new OnHitStatusList();

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

        public List<DamageTemplate> AdditionalHitDamagePerLevel
        {
            get => _additionalHitDamagePerLevel;
            set
            {
                _additionalHitDamagePerLevel = value;
                NotifyPropertyChanged();
            }
        }
        private List<DamageTemplate> _additionalHitDamagePerLevel = new List<DamageTemplate>();

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

        public bool OpenEditWindow()
        {
            SpellEditWindow window = new SpellEditWindow();
            Spell temporary = (Spell)this.Clone();
            window.DataContext = temporary;
            window.ShowCentered();

            if (temporary.Validated == true)
            {
                this.CopyAssign(temporary);
                return true;
            }
            return false;
        }

        #endregion EditWindow

        #region ICloneable

        private void init_copy(Spell to_copy)
        {
            this.BaseLevel = to_copy.BaseLevel;
            this.Description = (string)to_copy.Description.Clone();
            this.AmountTargets = to_copy.AmountTargets;
            this.CanSelectSameTargetTwice = to_copy.CanSelectSameTargetTwice;
            this.IsAnAttack = to_copy.IsAnAttack;
            this.HasSavingThrow = to_copy.HasSavingThrow;
            this.SavingCharacteristic = to_copy.SavingCharacteristic;
            this.SavingDifficulty = to_copy.SavingDifficulty;
            this.HitDamage = (List<DamageTemplate>)to_copy.HitDamage.Clone();
            this.AppliedStatus = (OnHitStatusList)to_copy.AppliedStatus.Clone();
            this.CanBeCastAtHigherLevel = to_copy.CanBeCastAtHigherLevel;
            this.AdditionalHitDamagePerLevel = (List<DamageTemplate>)to_copy.AdditionalHitDamagePerLevel.Clone();
            this.AdditionalTargetPerLevel = to_copy.AdditionalTargetPerLevel;
        }

        protected Spell(Spell to_copy) : base(to_copy)
        {
            init_copy(to_copy);
        }

        public new object Clone()
        {
            return new Spell(this);
        }

        #region ICopyAssignable

        public override void CopyAssign(object to_copy)
        {
            base.CopyAssign(to_copy);
            init_copy((Spell)to_copy);
        }

        #endregion ICopyAssignable
        #endregion ICloneable
    }
}
