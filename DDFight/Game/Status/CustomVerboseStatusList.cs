using DDFight.Game.Status.Display;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DDFight.Game.Status
{
    public class CustomVerboseStatusList : INotifyPropertyChanged, ICloneable
    {
        public CustomVerboseStatusList() { }

        [XmlIgnore]
        public bool Validated = false;

        public ObservableCollection<CustomVerboseStatus> List
        {
            get => _list;
            set
            {
                _list = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<CustomVerboseStatus> _list = new ObservableCollection<CustomVerboseStatus>();

        public void OpenEditWindow()
        {
            CustomVerboseStatusListEditWindow window = new CustomVerboseStatusListEditWindow();
            CustomVerboseStatusList dc = (CustomVerboseStatusList)this.Clone();
            dc.Validated = false;

            window.DataContext = dc;

            window.ShowCentered();

            if (dc.Validated == true)
                List = dc.List;
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

        public void init_copy(CustomVerboseStatusList to_copy)
        {
            List = to_copy.List.Clone();
        }

        public CustomVerboseStatusList(CustomVerboseStatusList to_copy)
        {
            init_copy(to_copy);
        }

        public object Clone()
        {
            return new CustomVerboseStatusList(this);
        }

        public virtual void CopyAssign(object _to_copy)
        {
            CustomVerboseStatusList to_copy = (CustomVerboseStatusList)_to_copy;
            init_copy(to_copy);
        }

    }
}
