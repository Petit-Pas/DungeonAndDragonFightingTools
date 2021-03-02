using DDFight.Game.Status.Display;
using DDFight.Tools;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DDFight.Game.Status
{
    public class CustomVerboseStatus : INotifyPropertyChanged, ICloneable, IWindowEditable, INameable
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

        public void CopyAssign(CustomVerboseStatus to_copy)
        {
            init_copy(to_copy);
        }

        #endregion ICloneable

        #region DisplayOption

        /// <summary>
        ///     Will open a window to edit this instance
        /// </summary>
        /// <returns> True if the current instance has changed, false otherwise </returns>
        public virtual bool OpenEditWindow()
        {
            CustomVerboseStatusEditWindow window = new CustomVerboseStatusEditWindow();
            CustomVerboseStatus dc = (CustomVerboseStatus)this.Clone();

            window.DataContext = dc;

            window.ShowCentered();

            if (window.Validated)
            {
                this.CopyAssign(dc);
                return true;
            }
            return false;
        }

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
