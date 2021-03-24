using DDFight.Controlers;

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
