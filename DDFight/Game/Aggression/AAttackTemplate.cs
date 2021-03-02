using DDFight.Tools;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DDFight.Game.Aggression
{
    public abstract class AAttackTemplate : INotifyPropertyChanged, ICopyAssignable, INameable, IWindowEditable
    {
        public AAttackTemplate()
        {
        }

        public virtual bool OpenEditWindow()
        {
            throw new NotImplementedException("Edit method should be overriden");
        }

        #region Properties

        #region Properties_Name

        [XmlIgnore]
        public string DisplayName
        {
            get => _name;
            set { }
        }

        [XmlAttribute]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("DisplayName");
            }
        }
        private string _name = "Name";

        #endregion Properties_Name

        #region Properties_Range

        [XmlAttribute]
        public AttackRangeEnum Range
        {
            get => _range;
            set
            {
                if (_range != value)
                {
                    _range = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private AttackRangeEnum _range = AttackRangeEnum.CloseRange;

        [XmlIgnore]
        public bool IsLongRanged
        {
            get
            {
                return Range == AttackRangeEnum.LongRange;
            }
            set
            {
                if (value == true)
                {
                    Range = AttackRangeEnum.LongRange;
                    IsCloseRanged = false;
                }
                NotifyPropertyChanged();
            }
        }

        [XmlIgnore]
        public bool IsCloseRanged
        {
            get
            {
                return Range == AttackRangeEnum.CloseRange;
            }
            set
            {
                if (value == true)
                {
                    Range = AttackRangeEnum.CloseRange;
                    IsLongRanged = false;
                }
                NotifyPropertyChanged();
            }
        }

        #endregion Range_Properties

        #endregion Properties

        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion INotifyPropertyChanged

        #region ICloneable

        private void init_copy(AAttackTemplate to_copy)
        {
            Name = (string)to_copy.Name.Clone();
            Range = to_copy.Range;
        }

        protected AAttackTemplate(AAttackTemplate to_copy)
        {
            init_copy(to_copy);
        }

        public object Clone()
        {
            throw new MissingMethodException("Cannot clone an instance of AAttackTemplate");
        }

        #region ICopyAssignable

        public virtual void CopyAssign(object to_copy)
        {
            init_copy((AAttackTemplate)to_copy);
        }

        #endregion ICopyAssignable

        #endregion IConeable

    }
}
