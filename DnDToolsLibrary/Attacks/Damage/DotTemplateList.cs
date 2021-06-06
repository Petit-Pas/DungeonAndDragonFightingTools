using DnDToolsLibrary.Memory;

namespace DnDToolsLibrary.Attacks.Damage
{
    public class DotTemplateList : GenericList<DotTemplate>
    {
        public DotTemplateList() : base() { }

        public DamageResultList GetResultList(bool linked_to_saving = true)
        {
            DamageResultList result = new DamageResultList();

            foreach (DamageTemplate template in Elements)
            {
                result.AddElementSilent(new DamageResult(template, linked_to_saving));
            }
            return result;
        }

        public DotTemplateList(DotTemplateList to_copy) : base(to_copy) { }

        public override object Clone()
        {
            return new DotTemplateList(this);
        }

    }
}
