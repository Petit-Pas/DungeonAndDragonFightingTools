using DnDToolsLibrary.Memory;

namespace DnDToolsLibrary.Attacks.Damage
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
