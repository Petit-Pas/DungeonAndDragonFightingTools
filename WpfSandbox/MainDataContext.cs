using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfSandbox
{
    public enum AttackRangeEnum
    {
        CloseRange,
        LongRange,
    }

    public class MainDataContext : INotifyPropertyChanged
    {
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
            Console.WriteLine("property {0} in mainDataContext changed", propertyName);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
