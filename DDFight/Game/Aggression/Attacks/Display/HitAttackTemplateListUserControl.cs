using DDFight.Controlers;
using DDFight.WpfExtensions;
using DnDToolsLibrary.Attacks.HitAttacks;

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

        public override bool edit(object obj)
        {
            if (obj is HitAttackTemplate hitAttackTemplate)
                return hitAttackTemplate.OpenEditWindow();
            return false;
        }

        private void HitAttackTemplateListUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (data_context != null)
                EntityList = data_context.Elements;
        }
    }
}
