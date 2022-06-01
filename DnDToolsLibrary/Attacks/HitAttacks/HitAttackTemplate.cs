using BaseToolsLibrary;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Attacks.HitAttacks
{
    /// <summary>
    ///     Represents a possible attack, example: inflamed two handed sword, +7 to Hit, 2d6+3 of Slashing Damage + 1d4 of Fire Damage 
    /// </summary>
    public class HitAttackTemplate : AAttackTemplate, ICloneable, IEquivalentComparable<HitAttackTemplate>
    {
        public HitAttackTemplate() : base()
        {
        }

        [XmlIgnore]
        public PlayableEntity Owner;

        #region Properties

        public OnHitStatusList OnHitStatuses {
            get => _onHitStatuses;
            set
            {
                _onHitStatuses = value;
                NotifyPropertyChanged();
            }
        }
        private OnHitStatusList _onHitStatuses = new OnHitStatusList();

        [XmlAttribute]
        /// <summary>
        ///     Bonus to hit on the d20 throw to know wehter the attack hits or not
        /// </summary>
        public int HitBonus
        {
            get => _hitBonus;
            set
            {
                _hitBonus = value;
                NotifyPropertyChanged();
            }
        }
        private int _hitBonus = 0;

        /// <summary>
        ///     The list of damage that the attack will inflict if it lands
        /// </summary>
        public DamageTemplateList DamageList
        {
            get => _damage;
            set {
                _damage = value;
                NotifyPropertyChanged();
            }
        }
        private DamageTemplateList _damage = new DamageTemplateList();

        #endregion Properties
        
        public HitAttackResult GetResultTemplate()
        {
            return new HitAttackResult()
            {
                Name = this.Name,
                DamageList = DamageList.GetResultList(),
                RollResult = new AttackRollResult
                {
                    BaseRollModifier = HitBonus,
                    Caster = this.Owner,
                    HitModifiers = new SituationalAttackRollModifiers(),
                    AdvantageModifiers = new SituationalAdvantageModifiers(),
                },
                Caster = this.Owner,
                OnHitStatuses = this.OnHitStatuses,
            };
        }

        #region ICloneable

        private void init_copy(HitAttackTemplate to_copy)
        {
            HitBonus = to_copy.HitBonus;
            DamageList = (DamageTemplateList)to_copy.DamageList.Clone();
            OnHitStatuses = (OnHitStatusList)to_copy.OnHitStatuses.Clone();
        }

        protected HitAttackTemplate(HitAttackTemplate to_copy) : base(to_copy)
        {
            init_copy(to_copy);
        }

        public new object Clone()
        {
            return new HitAttackTemplate(this);
        }

        #region ICopyAssignable

        public override void CopyAssign(object to_copy)
        {
            base.CopyAssign(to_copy);
            init_copy((HitAttackTemplate)to_copy);
        }

        #endregion ICopyAssignable

        public bool IsEquivalentTo(HitAttackTemplate toCompare)
        {
            if (!base.IsEquivalentTo(toCompare))
                return false;
            if (HitBonus != toCompare.HitBonus)
                return false;
            if (!DamageList.IsEquivalentTo(toCompare.DamageList))
                return false;
            if (!OnHitStatuses.IsEquivalentTo(toCompare.OnHitStatuses))
                return false;
            return true;
        }

        #endregion
    }
}
