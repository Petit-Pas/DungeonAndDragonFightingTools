using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks;
using DnDToolsLibrary.Attacks.AttacksCommands.HitAttackCommands.ApplyHitAttackResult;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CoreUnitTest.Commands.Attacks.HitAttacks
{
    [TestFixture]
    public class ApplyHitAttackResultCommandTest
    {
        private IMediator _mediator;
        private PlayableEntity _character;
        // Defaults to a 12 to hit, with 10 fire, 5 cold and 10 poison damage
        private HitAttackResult _attackResult;

        // occurs once before every test in this class
        [OneTimeSetUp]
        public void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _character = FightersList.Instance[0];
        }

        [SetUp]
        public void Setup()
        {
            _attackResult = new HitAttackResult()
            {
                DamageList = new DamageResultList()
                {
                    // using D1 to remove random factor
                    new DamageResult("1d1+9", DamageTypeEnum.Fire),
                    new DamageResult("1d1+4", DamageTypeEnum.Cold),
                    new DamageResult("1d1+9", DamageTypeEnum.Poison),
                },
                Owner = _character,
                Target = _character,
                RollResult = new AttackRollResult()
                {
                    AttackRoll = 10,
                    BaseRollModifier = 2,
                    Target = _character,
                },
            };
            foreach (DamageResult dmg in _attackResult.DamageList)
            {
                dmg.Damage.Roll();
            }
            _character.Hp = 100;
        }

        [Test]
        public void BasicTest()
        {
            ApplyHitAttackResultCommand command = new ApplyHitAttackResultCommand(_attackResult);

            _mediator.Execute(command);

            // target was hit
            Assert.AreEqual(100 - 25, _character.Hp);
        }

        [Test]
        public void JustNotEnoughArmorTest()
        {
            _attackResult.RollResult.AttackRoll = 8;
            ApplyHitAttackResultCommand command = new ApplyHitAttackResultCommand(_attackResult);

            _mediator.Execute(command);

            // target was barely hit 
            Assert.AreEqual(75, _character.Hp);
        }

        [Test]
        public void EnoughArmorTest()
        {
            _attackResult.RollResult.AttackRoll = 7;
            ApplyHitAttackResultCommand command = new ApplyHitAttackResultCommand(_attackResult);

            _mediator.Execute(command);

            // target was not hit 
            Assert.AreEqual(100, _character.Hp);
        }

    }
}
