using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Status;

namespace CoreUnitTest.TestFactories
{
    public static class SpellFactory
    {
        public static Spell FireBolt => new Spell()
        {
            AdditionalHitDamagePerLevel = new DamageTemplateList()
            {
                new DamageTemplate("1d10", DamageTypeEnum.Fire),
            },
            IsAnAttack = true,
            AmountTargets = 1,
            BaseLevel = 0,
            CanBeCastAtHigherLevel = true,
            DisplayName = "Fire Bolt",
            HitDamage = new DamageTemplateList()
            {
                new DamageTemplate("1d10", DamageTypeEnum.Fire),
            },
            AdditionalTargetPerLevel = 0,
            HitRollBonus = 5
        };

        public static Spell FireBall => new Spell()
        {
            AdditionalHitDamagePerLevel = new DamageTemplateList()
            {
                new DamageTemplate("8d6", DamageTypeEnum.Fire),
            },
            AmountTargets = 3,
            AdditionalTargetPerLevel = 0,
            BaseLevel = 3,
            CanBeCastAtHigherLevel = true,
            DisplayName = "Fire Ball",
            HitDamage = new DamageTemplateList()
            {
                new DamageTemplate("1d6", DamageTypeEnum.Fire),
            },
            SavingCharacteristic = CharacteristicsEnum.Dexterity,
            SavingDifficulty = 0,
            HasSavingThrow = true
        };

        public static Spell Bless => new Spell()
        {
            AppliedStatus = new OnHitStatusList()
            {
                StatusFactory.Bless,
            },
            AmountTargets = 1,
            AdditionalTargetPerLevel = 1,
            BaseLevel = 1,
            CanBeCastAtHigherLevel = true,
            DisplayName = "Bless",
        };
    }
}
