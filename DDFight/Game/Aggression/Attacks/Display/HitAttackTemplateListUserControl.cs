using DDFight.Controlers;
using DDFight.Tools;
using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Aggression.Attacks.Display
{
    public class HitAttackTemplateListUserControl : SpecializedListUserControl<HitAttackTemplate>
    {
        public HitAttackTemplateListUserControl() : base()
        {
            DataContextChanged += HitAttackTemplateListUserControl_DataContextChanged;
        }

        private HitAttackTemplateList data_context
        {
            get => DataContext as HitAttackTemplateList;
        }

        private void HitAttackTemplateListUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (data_context != null)
                EntityList = data_context.Elements;
        }
    }
}
