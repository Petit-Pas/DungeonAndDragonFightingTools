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
            if (!Global.Loading)
                initAffinities();
        }

        private void initAffinities()
        {
            _damageTypeAffinityList = new List<DamageTypeAffinityDataContext>
            {
                new DamageTypeAffinityDataContext(DamageTypeEnum.Acid),
                new DamageTypeAffinityDataContext(DamageTypeEnum.Bludgeoning),
                new DamageTypeAffinityDataContext(DamageTypeEnum.Cold),
                new DamageTypeAffinityDataContext(DamageTypeEnum.Fire),
                new DamageTypeAffinityDataContext(DamageTypeEnum.Force),
                new DamageTypeAffinityDataContext(DamageTypeEnum.Lightning),
                new DamageTypeAffinityDataContext(DamageTypeEnum.Necrotic),
                new DamageTypeAffinityDataContext(DamageTypeEnum.Piercing),
                new DamageTypeAffinityDataContext(DamageTypeEnum.Poison),
                new DamageTypeAffinityDataContext(DamageTypeEnum.Psychic),
                new DamageTypeAffinityDataContext(DamageTypeEnum.Radiant),
                new DamageTypeAffinityDataContext(DamageTypeEnum.Slashing),
                new DamageTypeAffinityDataContext(DamageTypeEnum.Thunder)
            };
        }

        public DamageTypeAffinityDataContext GetAffinity(DamageTypeEnum type)
        {
            return DamageTypeAffinityList.First(x => x.Type == type);
        }

        #region affinities

        public List<DamageTypeAffinityDataContext> DamageTypeAffinityList {
            get {
                return _damageTypeAffinityList;
            }
            set {
                _damageTypeAffinityList = value;
                NotifyPropertyChanged();
            }
        }
        private List<DamageTypeAffinityDataContext> _damageTypeAffinityList = null;

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
            ;
            DamageTypeAffinityList = (List<DamageTypeAffinityDataContext>)to_copy.DamageTypeAffinityList.Clone();
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
