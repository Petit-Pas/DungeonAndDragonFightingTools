using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;
using WpfToolsLibrary.Display;

namespace DnDToolsLibrary.Attacks.Spells
{
    public class Spell : AAttackTemplate, ICloneable, INameable, INotifyPropertyChanged
    {
        public Spell () : base()
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

        #region Properties_Level

        [XmlAttribute]
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

        [XmlAttribute]
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

        public DamageTemplateList AdditionalHitDamagePerLevel
        {
            get => _additionalHitDamagePerLevel;
            set
            {
                _additionalHitDamagePerLevel = value;
                NotifyPropertyChanged();
            }
        }
        private DamageTemplateList _additionalHitDamagePerLevel = new DamageTemplateList();

        [XmlAttribute]
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

        #endregion Properties_Level

        #region Properties_Targets

        /// <summary>
        ///     0 means uncapped
        /// </summary>
        [XmlAttribute]
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

        [XmlAttribute]
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

        #endregion Properties_Targets

        #region Properties_SavingThrow

        [XmlAttribute]
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

        [XmlAttribute]
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

        [XmlAttribute]
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

        #endregion Properties_SavingThrow

        #region Properties_Attack

        /// <summary>
        ///     tells wether or not there is an attack roll (opposed to saving throw OR automatic success)
        /// </summary>
        [XmlAttribute]
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

        [XmlAttribute]
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
        [XmlAttribute]
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

        #endregion Properties_Attack

        #region Properties_SpellEffect
        
        public DamageTemplateList HitDamage
        {
            get => _hitDamage;
            set
            {
                _hitDamage = value;
                NotifyPropertyChanged();
            }
        }
        private DamageTemplateList _hitDamage = new DamageTemplateList();

        /// <summary>
        ///     If the spell is an attack, the status will have their own saving throw
        ///     If the spell has a saving, the status will share the same saving throw
        /// </summary>
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

        #endregion Properties_SpellEffect

        #endregion Properties

        public void CastSpell(PlayableEntity caster)
        {
            Console.Write("CRITICAL");
            /*
            AskPositiveIntWindow levelWindow = new AskPositiveIntWindow();
            levelWindow.DescriptionTextBoxControl.Text = "at which level do you wish to cast this spell?";
            levelWindow.Number = this.BaseLevel;
            levelWindow.ShowCentered();

            if (levelWindow.Validated == false)
                return;

            int level = levelWindow.Number;
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
                    SpellAttackCastWindow window = new SpellAttackCastWindow()
                    {
                        DataContext = this.GetAttackSpellResult(caster, targetWindow.Selected, additional_levels)
                    };
                    window.ShowCentered();
                }
                else
                {
                    SpellNonAttackCastWindow window = new SpellNonAttackCastWindow() { 
                        DataContext = this.GetNonAttackSpellResult(caster, targetWindow.Selected, additional_levels) 
                    };
                    window.ShowCentered();
                }
            }
            */
        }

        public AttackSpellResult GetAttackSpellResult(PlayableEntity caster, ObservableCollection<PlayableEntity> targets, int additional_levels)
        {
            AttackSpellResult template = new AttackSpellResult
            {
                HitDamage = this.HitDamage.GetResultList(),
                AppliedStatusList = this.AppliedStatus,
                Caster = caster,
                Targets = targets,
                Name = this.Name,
                Level = this.BaseLevel + additional_levels,
                AutomaticalyHits = this.AutomaticalyHits,
                ToHitBonus = (this.HitRollBonus == 0 ? caster.SpellHitModifier : this.HitRollBonus),
            };

            for (int i = additional_levels; i > 0; i -= 1)
            {
                foreach (DamageTemplate damageTemplate in AdditionalHitDamagePerLevel.Elements)
                {
                    bool added = false;
                    foreach (DamageResult onHitTemplate in template.HitDamage.Elements)
                    {
                        if (onHitTemplate.IsSameKind(damageTemplate))
                        {
                            onHitTemplate.Add(damageTemplate);
                            added = true;
                            break;
                        }
                    }
                    if (added == false)
                        template.HitDamage.AddElementSilent(new DamageResult(damageTemplate));
                }
            }
            return template;
        }

        public NonAttackSpellResult GetNonAttackSpellResult(PlayableEntity caster, ObservableCollection<PlayableEntity> targets, int additional_levels)
        {
            NonAttackSpellResult template = new NonAttackSpellResult {
                HitDamage = this.HitDamage.GetResultList(),
                AppliedStatusList = this.AppliedStatus,
                Caster = caster,
                HasSavingThrow = this.HasSavingThrow,
                SavingCharacteristic = this.SavingCharacteristic,
                SavingDifficulty = (this.SavingDifficulty == 0 ? caster.SpellSave : this.SavingDifficulty),
                Targets = targets,
                Name = this.Name,
                Level = this.BaseLevel + additional_levels,
            };

            for (int i = additional_levels; i > 0; i -= 1)
            {
                foreach (DamageTemplate damageTemplate in AdditionalHitDamagePerLevel.Elements)
                {
                    bool added = false;
                    foreach (DamageResult onHitTemplate in template.HitDamage.Elements)
                    {
                        if (onHitTemplate.IsSameKind(damageTemplate))
                        {
                            onHitTemplate.Add(damageTemplate);
                            added = true;
                            break;
                        }
                    }
                    if (added == false)
                        template.HitDamage.AddElementSilent(new DamageResult(damageTemplate));
                }
            }
            return template;
        }

        #region EditWindow

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
            this.HitDamage = (DamageTemplateList)to_copy.HitDamage.Clone();
            this.AppliedStatus = (OnHitStatusList)to_copy.AppliedStatus.Clone();
            this.CanBeCastAtHigherLevel = to_copy.CanBeCastAtHigherLevel;
            this.AdditionalHitDamagePerLevel = (DamageTemplateList)to_copy.AdditionalHitDamagePerLevel.Clone();
            this.AdditionalTargetPerLevel = to_copy.AdditionalTargetPerLevel;
            this.HitRollBonus = to_copy.HitRollBonus;
            this.AutomaticalyHits = to_copy.AutomaticalyHits;
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
