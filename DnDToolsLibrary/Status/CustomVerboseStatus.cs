using BaseToolsLibrary;
using BaseToolsLibrary.Memory;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Status
{
    public class CustomVerboseStatus : INotifyPropertyChanged, ICopyAssignable, INameable
    {
        public CustomVerboseStatus() { }

        [XmlAttribute]
        // Should be 1 single world
        public string Header
        {
            get => _header;
            set
            {
                _header = value;
                NotifyPropertyChanged();
            }
        }
        private string _header = "";

        [XmlElement]
        // Should be a short sentence explaining the condition
        public string ToolTip
        {
            get => _toolTip;
            set
            {
                _toolTip = value;
                NotifyPropertyChanged();
            }
        }
        private string _toolTip = "";

        [XmlElement]
        // Should be a long description explaining both condition and the way it gets removed
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChanged();
            }
        }
        private string _description = "";

        #region IListable
        [XmlIgnore]
        public string DisplayName { get => Header; set { } }
        [XmlIgnore]
        public string Name { get => Header; set { } }
        #endregion IListable

        #region ICloneable

        protected void init_copy(CustomVerboseStatus to_copy)
        {
            Description = (string)to_copy.Description.Clone();
            Header = (string)to_copy.Header.Clone();
            ToolTip = (string)to_copy.ToolTip.Clone();
        }

        public CustomVerboseStatus (CustomVerboseStatus to_copy)
        {
            init_copy(to_copy);
        }

        public virtual object Clone()
        {
            return new CustomVerboseStatus(this);
        }

        public virtual void CopyAssign(object to_copy)
        {
            if (to_copy is CustomVerboseStatus status)
            {
                init_copy(status);
            }
        }

        #endregion ICloneable

        #region DisplayOption

        #endregion DisplayOption

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
    }
}
