
using DnDToolsLibrary.Attacks.HitAttacks;

namespace DnDToolsLibrary.Attacks.Spells
{
    public class AttackSpellResult : HitAttackResult

    {
        public AttackSpellResult()
        {
        }

        public AttackSpellResult(AttackSpellResult toCopy) : base(toCopy)
        {
        }

        public override object Clone()
        {
            return new AttackSpellResult(this);
        }
    }
}
