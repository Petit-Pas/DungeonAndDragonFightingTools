
using DnDToolsLibrary.Memory;

namespace DnDToolsLibrary.Attacks.Damage
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
