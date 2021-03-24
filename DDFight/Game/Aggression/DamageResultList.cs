using DDFight.Tools.Save;

namespace DDFight.Game.Aggression
{
    public class DamageResultList : GenericList<DamageResult>
    {
        public DamageResultList() : base()
        {
        }

        public DamageResultList(DamageResultList to_copy) : base(to_copy)
        {
        }

        public override object Clone()
        {
            return new DamageResultList(this);
        }

    }
}
