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
        public HitAttackResult()
        {
        }

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

        private static readonly IFightersProvider _fightersProvider = DIContainer.GetImplementation<IFightersProvider>();

        [XmlIgnore]
        public PlayableEntity Caster
        {
            get
            {
                return _fightersProvider.GetFighterByDisplayName(CasterName);
            }
            set
            {
                if (this.RollResult != null)
                {
                    this.RollResult.Caster = value;
                }

                if (value != null)
                    CasterName = value.DisplayName;
                else
                    CasterName = null;
                
                NotifyPropertyChanged();
            }
        }
        [XmlAttribute]
        public string CasterName
        {
            get
            {
                return _casterName;
            }
            set
            {
                _casterName = value;
                NotifyPropertyChanged();
            }
        }
        private string _casterName = null;

        [XmlIgnore]
        public PlayableEntity Target
        {
            get
            {
                return _fightersProvider.GetFighterByDisplayName(TargetName);
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
        private DamageResultList _damageList = new ();

        public OnHitStatusList OnHitStatuses
        {
            get => _onHitStatuses;
            set
            {
                _onHitStatuses = value;
                NotifyPropertyChanged();
            }
        }
        private OnHitStatusList _onHitStatuses = new ();

        public AttackRollResult RollResult
        {
            get => _rollResult;
            set
            {
                _rollResult = value;
                NotifyPropertyChanged();
            }
        }
        private AttackRollResult _rollResult = new ();

        public bool AutomaticallyHits {
            get => _automaticallyHits;
            set
            {
                _automaticallyHits = value;
                NotifyPropertyChanged();
            }
        }
        private bool _automaticallyHits = false;


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
            foreach (DamageResult dmg in DamageList)
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
            this.DamageList = to_copy.DamageList.Clone() as DamageResultList;
            this.RollResult = to_copy.RollResult.Clone() as AttackRollResult;
            this.OnHitStatuses = to_copy.OnHitStatuses.Clone() as OnHitStatusList;
            this.Name = to_copy.Name;
            this.TargetName = to_copy.TargetName;
            this.CasterName = to_copy.CasterName;
            this.AutomaticallyHits = to_copy.AutomaticallyHits;
        }

        public HitAttackResult(HitAttackResult to_copy)
        {
            init_copy(to_copy);
        }

        public void CopyAssign(object to_copy)
        {
            init_copy((HitAttackResult)to_copy);
        }

        public virtual object Clone()
        {
            return new HitAttackResult(this);
        }

    }
}
