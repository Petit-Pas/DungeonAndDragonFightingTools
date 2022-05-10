using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Fight;
using NUnit.Framework;

namespace CoreUnitTest.Commands.PlayableEntities.Damage
{
    [TestFixture]
    public class ApplyDamageResultListCommandTests
    {
        private IMediator _mediator;
        private PlayableEntity _character;
        private DamageResultList _damage;

        // occurs once before every test in this class
        [OneTimeSetUp]
        public void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _character = DIContainer.GetImplementation<IFightManager>().First();
        }

        [SetUp]
        public void Setup()
        {
            _character.Hp = 50;
            _damage = new DamageResultList()
            {
                new DamageResult("1d1+9", DamageTypeEnum.Fire),
                new DamageResult("1d1+4", DamageTypeEnum.Cold),
                new DamageResult("1d1+9", DamageTypeEnum.Poison),
            };
            foreach (DamageResult dmg in _damage)
            {
                dmg.Damage.Roll();
            }
        }

        [Test]
        public void Basic()
        {
            ApplyDamageResultListCommand command = new ApplyDamageResultListCommand(_character, _damage);

            _mediator.Execute(command);
            Assert.AreEqual(25, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }
        
        [Test]
        public void ReturnValue()
        {
            _damage[0].AffinityModifier = DamageAffinityEnum.Resistant;
            ApplyDamageResultListCommand command = new ApplyDamageResultListCommand(_character, _damage);

            ApplyDamageResultListResponse response = _mediator.Execute(command) as ApplyDamageResultListResponse;
            Assert.AreEqual(30, _character.Hp);
            Assert.AreEqual(20, response.Amount);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void Resistance()
        {
            _damage[0].AffinityModifier = DamageAffinityEnum.Resistant;

            ApplyDamageResultListCommand command = new ApplyDamageResultListCommand(_character, _damage);

            _mediator.Execute(command);
            Assert.AreEqual(30, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void Weakness()
        {
            _damage[0].AffinityModifier = DamageAffinityEnum.Weak;

            ApplyDamageResultListCommand command = new ApplyDamageResultListCommand(_character, _damage);

            _mediator.Execute(command);
            Assert.AreEqual(15, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void Immunity()
        {
            _damage[0].AffinityModifier = DamageAffinityEnum.Immune;

            ApplyDamageResultListCommand command = new ApplyDamageResultListCommand(_character, _damage);

            _mediator.Execute(command);
            Assert.AreEqual(35, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void Basic_WithSaving()
        {
            ApplyDamageResultListCommand command = new ApplyDamageResultListCommand(_character, _damage, true);

            _mediator.Execute(command);
            Assert.AreEqual(25, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void Halved_WithSaving()
        {
            _damage[0].SituationalDamageModifier = DamageModifierEnum.Halved;

            ApplyDamageResultListCommand command = new ApplyDamageResultListCommand(_character, _damage, true);

            _mediator.Execute(command);
            Assert.AreEqual(30, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }
        
        [Test]
        public void Cancel_WithSaving()
        {
            _damage[0].SituationalDamageModifier = DamageModifierEnum.Canceled;

            ApplyDamageResultListCommand command = new ApplyDamageResultListCommand(_character, _damage, true);

            _mediator.Execute(command);
            Assert.AreEqual(35, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void NegativeDamage()
        {
            _damage[0].Damage.LastRoll = -20;

            ApplyDamageResultListCommand command = new ApplyDamageResultListCommand(_character, _damage, true);

            _mediator.Execute(command);
            Assert.AreEqual(35, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }
    }
}
