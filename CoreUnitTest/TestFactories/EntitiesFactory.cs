using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Entities;

namespace CoreUnitTest.TestFactories
{
    public static class EntitiesFactory
    {
        public static PlayableEntity GetWarrior()
        {
            Character warrior = new Character()
            {
                ActionDescription = "Action Description",
                CA = 18,
                Characteristics = new CharacteristicList(4, 2, 2, 0, -1, 1),
                DamageAffinities = DamageTypeAffinityList.Default(),
                DisplayName = "Warrior",
                HitAttacks = new HitAttackTemplateList() {
                    HitAttackTemplateFactory.LongSword,
                },
                Hp = 50,
                HpString = "50",
                Level = 5,
                MaxHp = 100,
                Name = "Warrior",
                SpecialAbilities = "Special Abilities",
                SpellHitModifier = 0,
                Spells = new SpellList(),
                SpellSave = 10,
            };
            return warrior;
        }

        public static PlayableEntity GetWizard()
        {
            Character wizard = new Character()
            {
                ActionDescription = "Action Description",
                CA = 15,
                Characteristics = new CharacteristicList(-1, 2, 2, 4, 2, 1),
                DamageAffinities = DamageTypeAffinityList.Default(),
                DisplayName = "Wizard",
                HitAttacks = new HitAttackTemplateList() {
                    HitAttackTemplateFactory.Staff,
                },
                Hp = 30,
                HpString = "30",
                Level = 5,
                MaxHp = 30,
                Name = "Wizard",
                SpecialAbilities = "Special Abilities",
                SpellHitModifier = 5,
                Spells = new SpellList() { },
                SpellSave = 15,
            };
            return wizard;
        }

        public static PlayableEntity Goblin => new Monster()
        {
            ActionDescription = "Action Description",
            CA = 12,
            Characteristics = new CharacteristicList(4, 2, 2, 0, -1, 1),
            DamageAffinities = DamageTypeAffinityList.Default(),
            DisplayName = "Goblin - 1",
            HitAttacks = new HitAttackTemplateList() {
                HitAttackTemplateFactory.LongSword,
            },
            Hp = 50,
            HpString = "50",
            Level = 5,
            MaxHp = 100,
            Name = "Goblin",
            SpecialAbilities = "Special Abilities",
            SpellHitModifier = 0,
            Spells = new SpellList(),
            SpellSave = 10,
        };
    }
}
