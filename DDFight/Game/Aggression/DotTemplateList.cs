using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Aggression
{
    public class DotTemplateList : GenericList<DotTemplate>
    {
        public DotTemplateList() : base() { }

        public DotTemplateList(DotTemplateList to_copy) : base(to_copy) { }

        public override object Clone()
        {
            return new DotTemplateList(this);
        }

    }
}
