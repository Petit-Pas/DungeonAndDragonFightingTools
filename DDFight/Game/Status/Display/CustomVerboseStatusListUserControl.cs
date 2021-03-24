using DDFight.Controlers;
using DDFight.Tools.Save;

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

        private void CustomVerboseStatusListUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            this.EntityList = data_context.Elements;
        }
    }
}
