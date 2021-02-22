using DDFight.Game.Aggression;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace WpfSandbox.Types
{
    public class AAttackTemplateType
    {

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


        #region Properties

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

        #region Range

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

        #endregion

        #endregion Properties

        private void init_copy(AAttackTemplateType to_copy)
        {
            Name = (string)to_copy.Name.Clone();
            Range = to_copy.Range;
        }

        protected AAttackTemplateType(AAttackTemplateType to_copy)
        {
            init_copy(to_copy);
        }

        public object Clone()
        {
            throw new System.Exception();
        }

        #region ICopyAssignable

        public virtual void CopyAssign(object to_copy)
        {
            init_copy((AAttackTemplateType)to_copy);
        }

        #endregion ICopyAssignable
        public AAttackTemplateType()
        {

        }


    }
}