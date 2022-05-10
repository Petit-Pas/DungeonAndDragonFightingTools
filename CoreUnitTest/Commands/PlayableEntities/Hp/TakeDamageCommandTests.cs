using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.TakeDamage;
using DnDToolsLibrary.Fight;
using NUnit.Framework;

namespace CoreUnitTest.Commands.PlayableEntities.Hp
{
    [TestFixture]
    public class TakeDamageCommandTests
    {
        private IMediator _mediator;
        private PlayableEntity _character;

        // occurs once before every test in this class
        [OneTimeSetUp]
        public void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _character = DIContainer.GetImplementation<IFightersProvider>().First();
        }

        // occurs before each test
        [SetUp]
        public void Setup()
        {
            _character.Hp = 50;
        }

        [Test]
        public void Basic()
        {
            _character.TempHp = 0;
            TakeDamageCommand command = new TakeDamageCommand(_character, 10);

            _mediator.Execute(command);
            Assert.AreEqual(40, _character.Hp);
            Assert.AreEqual(0, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
            Assert.AreEqual(0, _character.TempHp);
        }

        [Test]
        public void Ko()
        {
            _character.TempHp = 0;
            TakeDamageCommand command = new TakeDamageCommand(_character, 50);

            _mediator.Execute(command);
            Assert.AreEqual(0, _character.Hp);
            Assert.AreEqual(0, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
            Assert.AreEqual(0, _character.TempHp);
        }

        [Test]
        public void KoOverflow()
        {
            _character.TempHp = 0;
            TakeDamageCommand command = new TakeDamageCommand(_character, 60);

            _mediator.Execute(command);
            Assert.AreEqual(0, _character.Hp);
            Assert.AreEqual(0, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
            Assert.AreEqual(0, _character.TempHp);
        }

        [Test]
        public void BasicTemp()
        {
            _character.TempHp = 10;
            TakeDamageCommand command = new TakeDamageCommand(_character, 5);

            _mediator.Execute(command);
            Assert.AreEqual(50, _character.Hp);
            Assert.AreEqual(5, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
            Assert.AreEqual(10, _character.TempHp);
        }

        [Test]
        public void EmptyTemp()
        {
            _character.TempHp = 10;
            TakeDamageCommand command = new TakeDamageCommand(_character, 10);

            _mediator.Execute(command);
            Assert.AreEqual(50, _character.Hp);
            Assert.AreEqual(0, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
            Assert.AreEqual(10, _character.TempHp);
        }

        [Test]
        public void TempOverflow()
        {
            _character.TempHp = 10;
            TakeDamageCommand command = new TakeDamageCommand(_character, 20);

            _mediator.Execute(command);
            Assert.AreEqual(40, _character.Hp);
            Assert.AreEqual(0, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
            Assert.AreEqual(10, _character.TempHp);
        }

        [Test]
        public void FullOverflow()
        {
            _character.TempHp = 10;
            TakeDamageCommand command = new TakeDamageCommand(_character, 100);

            _mediator.Execute(command);
            Assert.AreEqual(0, _character.Hp);
            Assert.AreEqual(0, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
            Assert.AreEqual(10, _character.TempHp);
        }

        [Test]
        public void NoDamage()
        {
            _character.TempHp = 10;
            TakeDamageCommand command = new TakeDamageCommand(_character, 0);

            _mediator.Execute(command);
            Assert.AreEqual(50, _character.Hp);
            Assert.AreEqual(10, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
            Assert.AreEqual(10, _character.TempHp);
        }

        [Test]
        public void ReturnValue()
        {
            _character.Hp = 50;
            TakeDamageCommand command = new TakeDamageCommand(_character, 0);

            IMediatorCommandResponse response = _mediator.Execute(command);
            MediatorCommandNoResponse _response = response as MediatorCommandNoResponse;

            Assert.IsNotNull(_response);
        }
    }
}
