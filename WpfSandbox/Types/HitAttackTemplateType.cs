using DDFight;
using DDFight.Game.Aggression.Attacks;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WpfSandbox.Types
{
    [XmlRoot(elementName: "HitAttackTemplate")]
    public class HitAttackTemplateType : AAttackTemplateType
    {
        public static HitAttackTemplateList ConvertList(IEnumerable<HitAttackTemplateType> list)
        {
            HitAttackTemplateList result = new HitAttackTemplateList();
            foreach (HitAttackTemplateType atk in list)
                result.AddElementSilent(Convert(atk));
            return result;
        }

        public static HitAttackTemplate Convert(HitAttackTemplateType attack)
        {
            HitAttackTemplate result = new HitAttackTemplate()
            {
                DamageList = DamageTemplateType.ConvertList(attack.DamageList),
                DisplayName = attack.DisplayName,
                HitBonus = attack.HitBonus,
                IsCloseRanged = attack.IsCloseRanged,
                IsLongRanged = attack.IsLongRanged,
                Name = attack.Name,
                OnHitStatuses = OnHitStatusListType.Convert(attack.OnHitStatuses),
            };

            return result;
        }
        public HitAttackTemplateType() { }

        #region Properties

        public OnHitStatusListType OnHitStatuses
        {
            get => _onHitStatuses;
            set
            {
                _onHitStatuses = value;
                NotifyPropertyChanged();
            }
        }
        private OnHitStatusListType _onHitStatuses = new OnHitStatusListType();

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
        [XmlArrayItem("DamageTemplate", typeof(DamageTemplateType))]
        public List<DamageTemplateType> DamageList
        {
            get => _damage;
            set
            {
                _damage = value;
                NotifyPropertyChanged();
            }
        }
        private List<DamageTemplateType> _damage = new List<DamageTemplateType>();

        #endregion Properties

        private void init_copy(HitAttackTemplateType to_copy)
        {
            HitBonus = to_copy.HitBonus;
            DamageList = (List<DamageTemplateType>)to_copy.DamageList.Clone();
            OnHitStatuses = (OnHitStatusListType)to_copy.OnHitStatuses.Clone();
        }

        protected HitAttackTemplateType(HitAttackTemplateType to_copy) : base(to_copy)
        {
            init_copy(to_copy);
        }

        public new object Clone()
        {
            return new HitAttackTemplateType(this);
        }

        #region ICopyAssignable

        public override void CopyAssign(object to_copy)
        {
            base.CopyAssign(to_copy);
            init_copy((HitAttackTemplateType)to_copy);
        }

        #endregion ICopyAssignable

    }
}
