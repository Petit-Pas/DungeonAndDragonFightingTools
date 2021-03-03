using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
