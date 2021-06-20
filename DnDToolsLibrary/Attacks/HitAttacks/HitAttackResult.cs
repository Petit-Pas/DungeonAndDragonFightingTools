using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Memory;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

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

        private static IFigtherProvider fighterProvider = DIContainer.GetImplementation<IFigtherProvider>();

        [XmlIgnore]
        public PlayableEntity Owner
        {
            get
            {
                return fighterProvider.GetFighterByDisplayName(OwnerName);
            }
            set
            {
                if (this.RollResult != null)
                {
                    this.RollResult.Caster = value;
                }

                if (value != null)
                    OwnerName = value.DisplayName;
                else
                    OwnerName = null;
                
                NotifyPropertyChanged();
            }
        }
        [XmlAttribute]
        public string OwnerName
        {
            get
            {
                return _ownerName;
            }
            set
            {
                _ownerName = value;
                NotifyPropertyChanged();
            }
        }
        private string _ownerName = null;

        [XmlIgnore]
        public PlayableEntity Target
        {
            get
            {
                return fighterProvider.GetFighterByDisplayName(TargetName);
            }
            set
            {
                if (this.RollResult != null)
                {
                    refresh_damage_affinity_modifiers(value);
                    this.RollResult.Target = value;
                }

                if (value != null)
                    TargetName = value.DisplayName;
                else
                    TargetName = null;
                
                NotifyPropertyChanged();
            }
        }
        [XmlAttribute]
        public string TargetName
        {
            get
            {
                return _targetName;
            }
            set
            {
                _targetName = value;
                NotifyPropertyChanged();
            }
        }
        private string _targetName = null;

        public DamageResultList DamageList
        {
            get => _damageList;
            set
            {
                _damageList = value;
                if (Target != null)
                    refresh_damage_affinity_modifiers(Target);
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

        private void refresh_damage_affinity_modifiers(PlayableEntity newTarget)
        {
            DamageList.RefreshDamageAffinityModifier(newTarget);
        }

        private void init_copy(HitAttackResult to_copy)
        {
            this.DamageList = (DamageResultList)to_copy.DamageList.Clone();
            this.RollResult = (AttackRollResult)to_copy.RollResult.Clone();
            this.OnHitStatuses = (OnHitStatusList)to_copy.OnHitStatuses.Clone();
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
