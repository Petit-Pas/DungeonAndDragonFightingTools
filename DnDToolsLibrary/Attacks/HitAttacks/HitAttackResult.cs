using BaseToolsLibrary.Memory;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DnDToolsLibrary.Attacks.HitAttacks
{
    public class HitAttackResult : INotifyPropertyChanged, ICopyAssignable
    {
        public HitAttackResult() { }

        public string Name 
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        private string _name;

        public PlayableEntity Owner
        {
            get => _owner;
            set
            {
                _owner = value;
                if (this.RollResult != null)
                    this.RollResult.Caster = value;
                NotifyPropertyChanged();
            }
        }
        private PlayableEntity _owner;

        public PlayableEntity Target
        {
            get => RollResult.Target;
            set 
            {
                RollResult.Target = value;
                this.RollResult.Target = value;
                NotifyPropertyChanged();
            }
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

        #endregion

        public void Reset()
        {
            foreach (DamageResult dmg in DamageList.Elements)
            {
                dmg.Reset();
            }
            this.RollResult.Reset();
        }

        private void init_copy(HitAttackResult to_copy)
        {
            this.DamageList = (DamageResultList)to_copy.DamageList.Clone();
            this.RollResult = (AttackRollResult)to_copy.RollResult.Clone();
            this.OnHitStatuses = (OnHitStatusList)OnHitStatuses.Clone();
            this.Name = to_copy.Name;
            this.Target = to_copy.Target;
            this.Owner = to_copy.Owner;
        }

        public HitAttackResult(HitAttackResult to_copy)
        {
            init_copy(to_copy);
        }

        public void CopyAssign(object to_copy)
        {
            init_copy((HitAttackResult)to_copy);
        }

        public object Clone()
        {
            return new HitAttackResult(this);
        }

    }
}
