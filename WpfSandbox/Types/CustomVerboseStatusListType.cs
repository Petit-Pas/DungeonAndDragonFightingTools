using DDFight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfSandbox.Types
{
    public class CustomVerboseStatusListType : INotifyPropertyChanged, ICloneable
    {
        public CustomVerboseStatusListType() { }

        [XmlIgnore]
        public bool Validated = false;

        [XmlArrayItem("CustomVerboseStatus", typeof(CustomVerboseStatusType))]
        public ObservableCollection<CustomVerboseStatusType> List
        {
            get => _list;
            set
            {
                _list = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<CustomVerboseStatusType> _list = new ObservableCollection<CustomVerboseStatusType>();

        public void OpenEditWindow()
        {
        }

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

        #endregion

        public void init_copy(CustomVerboseStatusListType to_copy)
        {
            List = to_copy.List.Clone();
        }

        public CustomVerboseStatusListType(CustomVerboseStatusListType to_copy)
        {
            init_copy(to_copy);
        }

        public object Clone()
        {
            return new CustomVerboseStatusListType(this);
        }

        public virtual void CopyAssign(object _to_copy)
        {
            CustomVerboseStatusListType to_copy = (CustomVerboseStatusListType)_to_copy;
            init_copy(to_copy);
        }

    }
}
