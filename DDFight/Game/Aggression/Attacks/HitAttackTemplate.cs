using DDFight.Game.DamageAffinity;
using DDFight.Game.Dices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDFight.Game.Aggression.Attacks
{
    /// <summary>
    ///     Represents a possible attack, example: inflamed two handed sword, +7 to Hit, 2d6+3 of Slashing Damage + 1d4 of Fire Damage 
    /// </summary>
    public class HitAttackTemplate : AAttackTemplate, ICloneable, INotifyPropertyChanged
    {
        public HitAttackTemplate() : base()
        {
        }

        [XmlIgnore]
        public bool Validated;

        [XmlAttribute]
        /// <summary>
        ///     Amount of hit (1 target per hit, a target can be focused more than once)
        /// </summary>
        public int HitAmount
        {
            get => _hitAmount;
            set
            {
                _hitAmount = value;
                NotifyPropertyChanged();
            }
        }
        private int _hitAmount;

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

        public List<HitAttackResult> GetResultTemplate()
        {
            List<HitAttackResult> result = new List<HitAttackResult>();

            for (int i = 0; i != HitAmount; i++)
            {
                result.Add(new HitAttackResult()
                {
                    DamageRoll = 0,
                    DamageList = (List<DamageTemplate>)DamageList.Clone(),
                    HitRoll = 0,
                    HitBonus = HitBonus,
                    Target = null,
                    AttackIndex = i,
                });
            }
            return result;
        }

        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region ICloneable

        protected HitAttackTemplate(HitAttackTemplate to_copy) : base(to_copy)
        {
            HitBonus = to_copy.HitBonus;
            HitAmount = to_copy.HitAmount;
            DamageList = (List<DamageTemplate>)to_copy.DamageList.Clone();
        }

        public new object Clone()
        {
            return new HitAttackTemplate(this);
        }

        #endregion
    }
}
