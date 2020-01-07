﻿using System;
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

        public DamageTypeAffinityDataContext(DamageTypeEnum type)
        {
            _type = type;
        }

        #region Properties

        [XmlAttribute]
        public DamageTypeEnum Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DamageTypeEnum _type;

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

        [XmlIgnore]
        public bool IsWeak
        {
            get => Affinity == DamageAffinityEnum.Weak;
            set {
                if (value == true)
                {
                    Affinity = DamageAffinityEnum.Weak;
                    IsNeutral = false;
                    IsResistant = false;
                    IsImmune = false;
                }
                NotifyPropertyChanged();
            }
        }

        [XmlIgnore]
        public bool IsNeutral
        {
            get => Affinity == DamageAffinityEnum.Neutral;
            set
            {
                if (value == true)
                {
                    Affinity = DamageAffinityEnum.Neutral;
                    IsWeak = false;
                    IsResistant = false;
                    IsImmune = false;
                }
                NotifyPropertyChanged();
            }

        }

        [XmlIgnore]
        public bool IsResistant
        {
            get => Affinity == DamageAffinityEnum.Resistant;
            set
            {
                if (value == true)
                {
                    Affinity = DamageAffinityEnum.Resistant;
                    IsWeak = false;
                    IsNeutral = false;
                    IsImmune = false;
                }
                NotifyPropertyChanged();
            }

        }

        [XmlIgnore]
        public bool IsImmune
        {
            get => Affinity == DamageAffinityEnum.Immune;
            set
            {
                if (value == true)
                {
                    Affinity = DamageAffinityEnum.Immune;
                    IsWeak = false;
                    IsNeutral = false;
                    IsResistant = false;
                }
                NotifyPropertyChanged();
            }

        }

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
            Type = to_copy.Type;
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