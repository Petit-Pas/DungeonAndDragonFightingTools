using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
