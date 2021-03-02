using DDFight;
using DDFight.Game.Status;
using DDFight.Game.Status.Display;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfSandbox.Types
{
    public class CustomVerboseStatusType : INotifyPropertyChanged, ICloneable
    {
        public CustomVerboseStatusType() { }

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

        public static CustomVerboseStatus Convert(CustomVerboseStatusType status)
        {
            CustomVerboseStatus result = new CustomVerboseStatus()
            {
                Description = status.Description,
                Header = status.Header,
                ToolTip = status.ToolTip,
            };
            return result;
        }

        private string _header = "";

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
        private string _toolTip = "";

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
        private string _description = "";

        #region ICloneable

        protected void init_copy(CustomVerboseStatusType to_copy)
        {
            Description = (string)to_copy.Description.Clone();
            Header = (string)to_copy.Header.Clone();
            ToolTip = (string)to_copy.ToolTip.Clone();
        }

        public CustomVerboseStatusType(CustomVerboseStatusType to_copy)
        {
            init_copy(to_copy);
        }

        public virtual object Clone()
        {
            return new CustomVerboseStatusType(this);
        }

        public void CopyAssign(CustomVerboseStatusType to_copy)
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
            CustomVerboseStatusType dc = (CustomVerboseStatusType)this.Clone();

            window.DataContext = dc;

            window.ShowCentered();

            if (dc.Validated)
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
