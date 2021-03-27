using DDFight.Game.Status.Display;
using DDFight.Tools.Save;
using System.Xml.Serialization;

namespace DDFight.Game.Status
{
    public class CustomVerboseStatusList : GenericList<CustomVerboseStatus>
    {
        public CustomVerboseStatusList() : base()
        {
        }

        public CustomVerboseStatusList(CustomVerboseStatusList to_copy) : base(to_copy)
        {
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
