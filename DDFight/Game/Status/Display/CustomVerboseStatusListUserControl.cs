using DDFight.Controlers;
using DDFight.WpfExtensions;
using DnDToolsLibrary.Memory;
using DnDToolsLibrary.Status;

namespace DDFight.Game.Status.Display
{
    public class CustomVerboseStatusListUserControl : SpecializedListUserControl<CustomVerboseStatus>
    {
        public CustomVerboseStatusListUserControl()
        {
            DataContextChanged += CustomVerboseStatusListUserControl_DataContextChanged;
        }

        private GenericList<CustomVerboseStatus> data_context
        {
            get => DataContext as GenericList<CustomVerboseStatus>;
        }

        public override bool edit(object element)
        {
            if (element is CustomVerboseStatus status)
                return status.OpenEditWindow();
            return false;
        }

        private void CustomVerboseStatusListUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            this.EntityList = data_context;
        }
    }
}
