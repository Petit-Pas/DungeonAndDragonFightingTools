using DDFight.Game.Status;
using DDFight.Windows;
using DDFight.Windows.FightWindows;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DDFight.Game.Aggression.Attacks
{
    /// <summary>
    ///     Represents a possible attack, example: inflamed two handed sword, +7 to Hit, 2d6+3 of Slashing Damage + 1d4 of Fire Damage 
    /// </summary>
    public class HitAttackTemplate : AAttackTemplate, ICloneable
    {
        public HitAttackTemplate() : base()
        {
        }

        [XmlIgnore]
        public PlayableEntity Owner;

        [XmlIgnore]
        public bool Validated;

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
        public List<DamageTemplate> DamageList
        {
            get => _damage;
            set {
                _damage = value;
                NotifyPropertyChanged();
            }
        }
        private List<DamageTemplate> _damage = new List<DamageTemplate> ();

        #endregion Properties
        
        public HitAttackResult GetResultTemplate()
        {
            return new HitAttackResult()
            {
                DamageList = (List<DamageTemplate>)DamageList.Clone(),
                HitRoll = 0,
                HitBonus = HitBonus,
                Target = null,
                SituationalHitAttackModifiers = new SituationalHitAttackModifiers(),
                SituationalAdvantageModifiers = new SituationalAdvantageModifiers(),
                Owner = this.Owner,
                OnHitStatuses = (OnHitStatusList)this.OnHitStatuses.Clone(),
            };
        }

        public void ExecuteAttack()
        {
            ExecuteHitAttackWindow window = new ExecuteHitAttackWindow()
            {
                DataContext = this,
            };
            window.ShowCentered();
        }

        public bool OpenEditWindow()
        {
            HitAttackTemplateEditWindow window = new HitAttackTemplateEditWindow();
            HitAttackTemplate temporary = (HitAttackTemplate)this.Clone();
            window.DataContext = temporary;
            window.ShowCentered();

            if (temporary.Validated == true)
            {
                this.CopyAssign(temporary);
                return true;
            }
            return false;
        }

        #region ICloneable

        private void init_copy(HitAttackTemplate to_copy)
        {
            HitBonus = to_copy.HitBonus;
            DamageList = (List<DamageTemplate>)to_copy.DamageList.Clone();
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

        #endregion
    }
}
