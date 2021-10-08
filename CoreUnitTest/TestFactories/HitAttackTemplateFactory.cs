using DnDToolsLibrary.Attacks;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Text;

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
            Name = "sword",
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
            Name = "bow",
            OnHitStatuses = new OnHitStatusList(),
            Owner = null,
            Range = AttackRangeEnum.LongRange
        };
    }
}
