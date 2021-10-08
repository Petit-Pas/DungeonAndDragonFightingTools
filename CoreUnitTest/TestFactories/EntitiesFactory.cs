using DnDToolsLibrary.Attacks;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Counters;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
