using DnDToolsLibrary.Memory;

namespace DnDToolsLibrary.Attacks.Damage
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

        public DamageResultList GetResultList()
        {
            DamageResultList result = new DamageResultList();

            foreach (DamageTemplate template in Elements)
            {
                result.AddElementSilent(new DamageResult(template));
            }
            return result;
        }
    }
}
