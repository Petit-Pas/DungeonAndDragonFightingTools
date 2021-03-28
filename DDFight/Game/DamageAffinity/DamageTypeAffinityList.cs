using BaseToolsLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DDFight.Game.DamageAffinity
{
    public class DamageTypeAffinityList : ICloneable, INotifyPropertyChanged
    {
        public DamageTypeAffinityList()
        {
            if (!Global.Loading)
                initAffinities();
        }

        private void initAffinities()
        {
            _damageTypeAffinityList = new List<DamageTypeAffinity>();
            foreach (DamageTypeEnum type in (DamageTypeEnum[]) Enum.GetValues(typeof(DamageTypeEnum)))
            {
                _damageTypeAffinityList.Add(new DamageTypeAffinity(type));
            }
        }

        public DamageTypeAffinity GetAffinity(DamageTypeEnum type)
        {
            return AffinityList.First(x => x.Type == type);
        }

        #region affinities

        public List<DamageTypeAffinity> AffinityList {
            get {
                return _damageTypeAffinityList;
            }
            set {
                _damageTypeAffinityList = value;
                NotifyPropertyChanged();
            }
        }
        private List<DamageTypeAffinity> _damageTypeAffinityList = null;

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

        protected DamageTypeAffinityList(DamageTypeAffinityList to_copy)
        {
            AffinityList = (List<DamageTypeAffinity>)to_copy.AffinityList.Clone();
        }

        /// <summary>
        ///     Processes Deep copy on the item
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new DamageTypeAffinityList(this);
        }

        #endregion
    }
}
