using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Aggression
{
    public class DamageTemplateList : GenericList<DamageTemplate>
    {
        public DamageTemplateList() : base()
        {
        }

        public DamageTemplateList(DamageTemplateList to_copy) : base(to_copy)
        {
        }

        public override object Clone()
        {
            return new DamageTemplateList(this);
        }
    }
}
