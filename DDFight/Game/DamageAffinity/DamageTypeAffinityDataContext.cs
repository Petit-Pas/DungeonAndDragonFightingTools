using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDFight.Game.DamageAffinity
{
    public class DamageTypeAffinityDataContext : ICloneable, INotifyPropertyChanged
    {
        public DamageTypeAffinityDataContext ()
        {
        }

        public DamageTypeAffinityDataContext(string name)
        {
            _name = name;
        }

        #region Properties

        [XmlAttribute]
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _name;

        [XmlAttribute]
        public DamageAffinityEnum Affinity
        {
            get => _affinity;
            set
            {
                if (_affinity != value)
                {
                    _affinity = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageAffinityEnum _affinity;

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

        protected DamageTypeAffinityDataContext(DamageTypeAffinityDataContext to_copy)
        {
            Name = (string)to_copy.Name.Clone();
            Affinity = to_copy.Affinity;
        }

        /// <summary>
        ///     Processes Deep copy on the item
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new DamageTypeAffinityDataContext(this);
        }

        #endregion

    }
}
