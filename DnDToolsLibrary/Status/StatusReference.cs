using BaseToolsLibrary;
using BaseToolsLibrary.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Status
{
    public class StatusReference : INameable, INotifyPropertyChanged, ICopyAssignable, IEquivalentComparable<StatusReference>
    {
        public StatusReference()
        {
        }

        // WARNING, this creates the ID reference on the status if it doesn't exist
        public StatusReference(CustomVerboseStatus status)
        {
            if (status.Id == default)
                status.Id = Guid.NewGuid();
            ActualStatusReferenceId = status.Id;
            Header = status.Header;
            ToolTip = status.ToolTip;
        }

        [XmlAttribute]
        public Guid ActualStatusReferenceId 
        {
            get => _actualStatusReference;
            set
            {
                _actualStatusReference = value;
                NotifyPropertyChanged();
            }
        }
        private Guid _actualStatusReference;

        [XmlAttribute]
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

        [XmlElement]
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

        #region IListable
        [XmlIgnore]
        public string DisplayName { get => Header; set { } }
        [XmlIgnore]
        public string Name { get => Header; set { } }
        #endregion IListable

        public bool IsEquivalentTo(StatusReference toCompare)
        {
            if (ActualStatusReferenceId != toCompare.ActualStatusReferenceId)
                return false;
            if (Header != toCompare.Header)
                return false;
            if (ToolTip != toCompare.ToolTip)
                return false;
            return true;
        }


        #region ICloneable

        protected void init_copy(StatusReference to_copy)
        {
            ActualStatusReferenceId = to_copy.ActualStatusReferenceId;
            Header = (string)to_copy.Header.Clone();
            ToolTip = (string)to_copy.ToolTip.Clone();
        }

        public StatusReference(StatusReference to_copy)
        {
            init_copy(to_copy);
        }

        public virtual object Clone()
        {
            return new StatusReference(this);
        }

        public virtual void CopyAssign(object to_copy)
        {
            if (to_copy is StatusReference status)
            {
                init_copy(status);
            }
        }

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
