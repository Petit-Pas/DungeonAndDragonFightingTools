using DDFight.Game.Status.Display;
using DDFight.Tools.Save;
using System;
using System.Xml.Serialization;

namespace DDFight.Game.Status
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

        public void OpenEditWindow()
        {
            CustomVerboseStatusListEditWindow window = new CustomVerboseStatusListEditWindow();
            CustomVerboseStatusList dc = (CustomVerboseStatusList)this.Clone();
            dc.Validated = false;

            window.DataContext = dc;

            window.ShowCentered();

            if (dc.Validated == true)
                Elements = dc.Elements;
        }
    }
}
