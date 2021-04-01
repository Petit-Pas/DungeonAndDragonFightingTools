using DnDToolsLibrary.Memory;
using System;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Status
{
    public class CustomVerboseStatusList : GenericList<CustomVerboseStatus>
    {
        public CustomVerboseStatusList() : base()
        {
            this.ListElementChanged += CustomVerboseStatusList_ListElementChanged;
        }

        private void CustomVerboseStatusList_ListElementChanged(object sender, ListElementChangedArgs e)
        {
            if (e.Operation == GenericListOperation.Deletion)
                if (e.Element is IDisposable disposable)
                    disposable.Dispose();
        }

        public CustomVerboseStatusList(CustomVerboseStatusList to_copy) : base(to_copy)
        {
            this.ListElementChanged += CustomVerboseStatusList_ListElementChanged;
        }

        public override object Clone()
        {
            return new CustomVerboseStatusList(this);
        }

        [XmlIgnore]
        public bool Validated = false;

    }
}
