using System.Linq;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Attacks;
using DnDToolsLibrary.Attacks.AttacksCommands.HitAttackCommands.ApplyHitAttackResult;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.Attacks.HitAttacks
{
    [TestFixture]
    public class ApplyHitAttackResultCommandTest
    {
        private IMediator _mediator;
        private IFightManager _fightManager;
        private PlayableEntity _target;
        private PlayableEntity _caster;
        // Defaults to a 12 to hit, with 10 fire, 5 cold and 10 poison damage
        private HitAttackResult _attackResult;

        // occurs once before every test in this class
        [OneTimeSetUp]
        public void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _fightManager = DIContainer.GetImplementation<IFightManager>();
        }

        [SetUp]
        public void Setup()
        {
            _caster = EntitiesFactory.GetWarrior();
            _target = EntitiesFactory.GetWizard();
            _target.CA = 10; // tests were designed for a target with 10 CA
            _fightManager.AddOrUpdateFighter(_target);
            _fightManager.AddOrUpdateFighter(_caster);

            _attackResult = new HitAttackResult()
            {
                DamageList = new DamageResultList()
                {
                    // using D1 to remove random factor
                    new DamageResult("1d1+9", DamageTypeEnum.Fire),
                    new DamageResult("1d1+4", DamageTypeEnum.Cold),
                    new DamageResult("1d1+9", DamageTypeEnum.Poison),
                },
                OwnerName = _caster.Name,
                TargetName = _target.Name,
                RollResult = new AttackRollResult()
                {
                    AttackRoll = 10,
                    BaseRollModifier = 2,
                    TargetName = _target.Name,
                },
                OnHitStatuses = new OnHitStatusList()
                {
                    StatusFactory.InfernalWound,
                    StatusFactory.AutomaticApplication
                }
            };
            foreach (DamageResult dmg in _attackResult.DamageList)
            {
                dmg.Damage.Roll();
            }
            _target.Hp = 100;

            var response = new ValidableResponse<SavingThrow>(true, SavingThrowFactory.Failed(_target));
            var handler = A.Fake<IMediatorHandler>();
            A.CallTo(() => handler.Execute(A<IMediatorCommand>._)).Returns(response);
            _mediator.RegisterHandler(handler, typeof(SavingThrowQuery));
        }

        [Test]
        public void BasicTest()
        {
            ApplyHitAttackResultCommand command = new ApplyHitAttackResultCommand(_attackResult);

            _mediator.Execute(command);

            // target was hit
            _target.Hp.Should().Be(75);
        }

        [Test]
        public void JustNotEnoughArmorTest()
        {
            _attackResult.RollResult.AttackRoll = 8;
            ApplyHitAttackResultCommand command = new ApplyHitAttackResultCommand(_attackResult);

            _mediator.Execute(command);

            // target was barely hit 
            _target.Hp.Should().Be(75);
        }

        [Test]
        public void EnoughArmorTest()
        {
            _attackResult.RollResult.AttackRoll = 7;
            ApplyHitAttackResultCommand command = new ApplyHitAttackResultCommand(_attackResult);

            _mediator.Execute(command);

            // target was not hit 
            _target.Hp.Should().Be(100);
        }

        [Test]
        public void Should_Hit_No_Matter_What_When_Automatically_Hits_Is_True()
        {
            _attackResult.AutomaticallyHits = true;
            _attackResult.RollResult.AttackRoll = 1;
            ApplyHitAttackResultCommand command = new ApplyHitAttackResultCommand(_attackResult);

            _mediator.Execute(command);

            // Assert
            // target was hit 
            _target.Hp.Should().Be(75);
        }

        [Test]
        public void Should_Try_Apply_OnHitStatii()
        {
            ApplyHitAttackResultCommand command = new ApplyHitAttackResultCommand(_attackResult);

            _mediator.Execute(command);

            command.InnerCommands.Should().Contain(x => x.GetType() == typeof(TryApplyStatusCommand));
        }

        [Test]
        public void Should_Try_Apply_Each_OnHitStatii()
        {
            ApplyHitAttackResultCommand command = new ApplyHitAttackResultCommand(_attackResult);

            _mediator.Execute(command);

            command.InnerCommands.Where(x => x.GetType() == typeof(TryApplyStatusCommand)).Should().HaveCount(2);
            command.InnerCommands.Should()  
                .ContainSingle(x => ((TryApplyStatusCommand)x).Status.DisplayName == "InfernalWound");
            command.InnerCommands.Should()
                .ContainSingle(x => ((TryApplyStatusCommand)x).Status.DisplayName == "AutomaticApplication");
        }

        [Test]
        public void Should_Try_Apply_Status_On_Affected()
        {
            ApplyHitAttackResultCommand command = new ApplyHitAttackResultCommand(_attackResult);

            _mediator.Execute(command);
            TryApplyStatusCommand innerCommand = command.InnerCommands.OfType<TryApplyStatusCommand>().First();

            innerCommand.Status.TargetName.Should().Be("Wizard");
        }

        [Test]
        public void Should_Try_Apply_Status_By_Caster()
        {
            ApplyHitAttackResultCommand command = new ApplyHitAttackResultCommand(_attackResult);

            _mediator.Execute(command);
            TryApplyStatusCommand innerCommand = command.InnerCommands.OfType<TryApplyStatusCommand>().First();

            innerCommand.Status.CasterName.Should().Be("Warrior");
        }
    }
}
