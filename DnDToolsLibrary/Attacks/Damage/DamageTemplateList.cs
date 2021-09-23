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

        public DamageResultList GetResultList(bool linked_to_saving = true)
        {
            DamageResultList result = new DamageResultList();

            foreach (DamageTemplate template in this)
            {
                result.AddElementSilent(new DamageResult(template, linked_to_saving));
            }
            return result;
        }
    }
}
