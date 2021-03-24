using DDFight.Tools.Save;

namespace DDFight.Game.Aggression.Attacks
{
    public class HitAttackTemplateList : GenericList<HitAttackTemplate>
    {
        public HitAttackTemplateList() : base ()
        {
        }

        public HitAttackTemplateList(HitAttackTemplateList to_copy) : base(to_copy)
        {
        }

        public override object Clone()
        {
            return new HitAttackTemplateList(this);
        }
    }
}
