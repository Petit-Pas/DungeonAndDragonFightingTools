
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Memory;
using System;

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

        public void RefreshDamageAffinityModifier(PlayableEntity newTarget)
        {
            if (newTarget != null)
                foreach (DamageResult result in Elements)
                {
                    result.AffinityModifier = newTarget.DamageAffinities.GetAffinity(result.DamageType).Affinity;
                }
        }
    }
}
