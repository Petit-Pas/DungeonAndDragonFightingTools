using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.DamageAffinity
{
    public class DamageTypeAffinitiesDataContext : ICloneable, INotifyPropertyChanged
    {
        public DamageTypeAffinitiesDataContext() 
        {
        }

        #region affinities

        public DamageTypeAffinityDataContext Acid 
        {
            get => _acid;
            set
            {
                if (value != _acid)
                {
                    _acid = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageTypeAffinityDataContext _acid = new DamageTypeAffinityDataContext(DamageTypeEnum.Acid);


        public DamageTypeAffinityDataContext Bludgeoning
        {
            get => _bludgeoning;
            set
            {
                if (value != _bludgeoning)
                {
                    _bludgeoning = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageTypeAffinityDataContext _bludgeoning = new DamageTypeAffinityDataContext(DamageTypeEnum.Bludgeoning);


        public DamageTypeAffinityDataContext Cold
        {
            get => _cold;
            set
            {
                if (value != _cold)
                {
                    _cold = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageTypeAffinityDataContext _cold = new DamageTypeAffinityDataContext(DamageTypeEnum.Cold);


        public DamageTypeAffinityDataContext Fire
        {
            get => _fire;
            set
            {
                if (value != _fire)
                {
                    _fire = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageTypeAffinityDataContext _fire = new DamageTypeAffinityDataContext(DamageTypeEnum.Fire);


        public DamageTypeAffinityDataContext Force
        {
            get => _force;
            set
            {
                if (value != _force)
                {
                    _force = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageTypeAffinityDataContext _force = new DamageTypeAffinityDataContext(DamageTypeEnum.Force);


        public DamageTypeAffinityDataContext Lightning
        {
            get => _lightning;
            set
            {
                if (value != _lightning)
                {
                    _lightning = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageTypeAffinityDataContext _lightning = new DamageTypeAffinityDataContext(DamageTypeEnum.Lightning);


        public DamageTypeAffinityDataContext Necrotic
        {
            get => _necrotic;
            set
            {
                if (value != _necrotic)
                {
                    _necrotic = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageTypeAffinityDataContext _necrotic = new DamageTypeAffinityDataContext(DamageTypeEnum.Necrotic);


        public DamageTypeAffinityDataContext Piercing
        {
            get => _piercing;
            set
            {
                if (value != _piercing)
                {
                    _piercing = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageTypeAffinityDataContext _piercing = new DamageTypeAffinityDataContext(DamageTypeEnum.Piercing);


        public DamageTypeAffinityDataContext Poison
        {
            get => _poison;
            set
            {
                if (value != _poison)
                {
                    _poison = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageTypeAffinityDataContext _poison = new DamageTypeAffinityDataContext(DamageTypeEnum.Poison);


        public DamageTypeAffinityDataContext Psychic
        {
            get => _psychic;
            set
            {
                if (value != _psychic)
                {
                    _psychic = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageTypeAffinityDataContext _psychic = new DamageTypeAffinityDataContext(DamageTypeEnum.Psychic);


        public DamageTypeAffinityDataContext Radiant
        {
            get => _radiant;
            set
            {
                if (value != _radiant)
                {
                    _radiant = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageTypeAffinityDataContext _radiant = new DamageTypeAffinityDataContext(DamageTypeEnum.Radiant);


        public DamageTypeAffinityDataContext Slashing
        {
            get => _slashing;
            set
            {
                if (value != _slashing)
                {
                    _slashing = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageTypeAffinityDataContext _slashing = new DamageTypeAffinityDataContext(DamageTypeEnum.Slashing);


        public DamageTypeAffinityDataContext Thunder
        {
            get => _thunder;
            set
            {
                if (value != _thunder)
                {
                    _thunder = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageTypeAffinityDataContext _thunder = new DamageTypeAffinityDataContext(DamageTypeEnum.Thunder);


        #endregion


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

        protected DamageTypeAffinitiesDataContext(DamageTypeAffinitiesDataContext to_copy)
        {
            Acid = (DamageTypeAffinityDataContext)to_copy.Acid.Clone();
            Bludgeoning = (DamageTypeAffinityDataContext)to_copy.Bludgeoning.Clone();
            Cold = (DamageTypeAffinityDataContext)to_copy.Cold.Clone();
            Fire = (DamageTypeAffinityDataContext)to_copy.Fire.Clone();
            Force = (DamageTypeAffinityDataContext)to_copy.Force.Clone();
            Lightning = (DamageTypeAffinityDataContext)to_copy.Lightning.Clone();
            Necrotic = (DamageTypeAffinityDataContext)to_copy.Necrotic.Clone();
            Piercing = (DamageTypeAffinityDataContext)to_copy.Piercing.Clone();
            Poison = (DamageTypeAffinityDataContext)to_copy.Poison.Clone();
            Psychic = (DamageTypeAffinityDataContext)to_copy.Psychic.Clone();
            Radiant = (DamageTypeAffinityDataContext)to_copy.Radiant.Clone();
            Slashing = (DamageTypeAffinityDataContext)to_copy.Slashing.Clone();
            Thunder = (DamageTypeAffinityDataContext)to_copy.Thunder.Clone();
        }

        /// <summary>
        ///     Processes Deep copy on the item
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new DamageTypeAffinitiesDataContext(this);
        }

        #endregion
    }
}
