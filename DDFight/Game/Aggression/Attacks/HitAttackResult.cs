using DDFight.Game.DamageAffinity;
using DDFight.Game.Dices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Aggression.Attacks
{
    public class HitAttackResult : INotifyPropertyChanged
    {
        public PlayableEntity Target
        {
            get => _target;
            set
            {
                _target = value;
                NotifyPropertyChanged();
            }
        }
        private PlayableEntity _target = null;

        public uint HitRoll
        {
            get => _hitRoll;
            set
            {
                _hitRoll = value;
                NotifyPropertyChanged();
            }
        }
        private uint _hitRoll = 0;

        public List<DamageTemplate> DamageList
        {
            get => _damageTemplate;
            set
            {
                _damageTemplate = value;
                NotifyPropertyChanged();
            }
        }
        private List<DamageTemplate> _damageTemplate;

        public int HitBonus
        {
            get => _hitBonus;
            set
            {
                _hitBonus = value;
                NotifyPropertyChanged();
            }
        }
        private int _hitBonus;

        public SituationalHitAttackModifiers SituationalHitAttackModifiers
        {
            get => _situationalHitAttackModifiers;
            set
            {
                _situationalHitAttackModifiers = value;
                NotifyPropertyChanged();
            }
        }
        private SituationalHitAttackModifiers _situationalHitAttackModifiers;

        public SituationalAdvantageModifiers SituationalAdvantageModifiers
        {
            get => _situationalAdvantageModifiers;
            set
            {
                _situationalAdvantageModifiers = value;
                NotifyPropertyChanged();
            }
        }
        private SituationalAdvantageModifiers _situationalAdvantageModifiers;

        public int AttackIndex
        {
            get => _attackIndex;
            set
            {
                _attackIndex = value;
                NotifyPropertyChanged();
            }
        }
        private int _attackIndex;

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

    }
}
