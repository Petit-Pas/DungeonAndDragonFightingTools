using DnDToolsLibrary.Attacks;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Status;

namespace CoreUnitTest.TestFactories
{
    public static class HitAttackTemplateFactory
    {
        public static HitAttackTemplate LongSword => new HitAttackTemplate()
        {
            DamageList = new DamageTemplateList()
                        {
                            new DamageTemplate("1d10+4", DamageTypeEnum.Slashing)
                        },
            DisplayName = "Sword",
            HitBonus = 7,
            IsCloseRanged = true,
            IsLongRanged = false,
            Name = "Sword",
            OnHitStatuses = new OnHitStatusList(),
            Owner = null,
            Range = AttackRangeEnum.CloseRange
        };

        public static HitAttackTemplate Bow => new HitAttackTemplate()
        {
            DamageList = new DamageTemplateList()
                        {
                            new DamageTemplate("1d6+4", DamageTypeEnum.Piercing)
                        },
            DisplayName = "Bow",
            HitBonus = 7,
            Name = "Bow",
            OnHitStatuses = new OnHitStatusList(),
            Owner = null,
            Range = AttackRangeEnum.LongRange
        };

        public static HitAttackTemplate Staff => new HitAttackTemplate()
        {
            DamageList = new DamageTemplateList()
                        {
                            new DamageTemplate("1d6+1", DamageTypeEnum.Piercing)
                        },
            DisplayName = "Staff",
            HitBonus = 3,
            Name = "Staff",
            OnHitStatuses = new OnHitStatusList(),
            Owner = null,
            Range = AttackRangeEnum.CloseRange
        };
    }
}
