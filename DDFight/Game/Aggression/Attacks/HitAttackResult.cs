using DDFight.Game.Entities;
using DDFight.Game.Status;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DDFight.Game.Aggression.Attacks
{
    public class HitAttackResult : INotifyPropertyChanged
    {
        public PlayableEntity Owner
        {
            get => _owner;
            set
            {
                _owner = value;
                this.RollResult.Caster = value;
                NotifyPropertyChanged();
            }
        }
        private PlayableEntity _owner;

        public PlayableEntity Target
        {
            get => RollResult.Target;
            set => RollResult.Target = value;
        }

        public DamageResultList DamageList
        {
            get => _damageList;
            set
            {
                _damageList = value;
                NotifyPropertyChanged();
            }
        }
        private DamageResultList _damageList = new DamageResultList();

        public OnHitStatusList OnHitStatuses
        {
            get => _onHitStatuses;
            set
            {
                _onHitStatuses = value;
                NotifyPropertyChanged();
            }
        }
        private OnHitStatusList _onHitStatuses = new OnHitStatusList();

        public AttackRollResult RollResult
        {
            get => _rollResult;
            set
            {
                _rollResult = value;
                NotifyPropertyChanged();
            }
        }
        private AttackRollResult _rollResult = new AttackRollResult();


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

        public void Reset()
        {
            foreach (DamageResult dmg in DamageList.Elements)
            {
                dmg.Reset();
            }
            this.RollResult.Reset();
        }
        #endregion

    }
}
