using DDFight.Controlers;
using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
