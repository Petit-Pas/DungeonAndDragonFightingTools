using DDFight.Game.Status.Display;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDFight.Game.Status
{
    public class CustomVerboseStatus : INotifyPropertyChanged, ICloneable
    {
        public CustomVerboseStatus() { }

        [XmlIgnore]
        public bool Validated = false;

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
        private string _header;

        [XmlAttribute]
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
        private string _toolTip;

        [XmlAttribute]
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
        private string _description;

        #region ICloneable

        public void init_copy(CustomVerboseStatus to_copy)
        {
            Description = to_copy.Description;
            Header = to_copy.Header;
            ToolTip = to_copy.ToolTip;
            
        }

        public CustomVerboseStatus (CustomVerboseStatus to_copy)
        {
            init_copy(to_copy);
        }

        public object Clone()
        {
            return new CustomVerboseStatus(this);
        }

        public void CopyAssign(CustomVerboseStatus to_copy)
        {
            init_copy(to_copy);
        }

        #region DisplayOption

        /// <summary>
        ///     Will open a window to edit this instance
        /// </summary>
        /// <returns> True if the current instance has changed, false otherwise </returns>
        public bool OpenEditWindow()
        {
            EditCustomVerboseStatusWindow window = new EditCustomVerboseStatusWindow();

            CustomVerboseStatus dc = (CustomVerboseStatus)this.Clone();

            window.DataContext = dc;

            window.ShowDialog();

            if (dc.Validated)
            {
                this.CopyAssign(dc);
                return true;
            }
            return false;
        }

        #endregion DisplayOption

        #endregion ICloneable

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
