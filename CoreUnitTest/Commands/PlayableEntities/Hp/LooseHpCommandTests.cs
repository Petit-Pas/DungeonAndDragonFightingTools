using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseHp;
using DnDToolsLibrary.Fight;
using NUnit.Framework;
using System;

namespace CoreUnitTest.Commands.PlayableEntities.Hp
{
    [TestFixture]
    public class LooseHpCommandTests
    {
        private IMediator _mediator;
        private PlayableEntity _character;

        // occurs once before every test in this class
        [OneTimeSetUp]
        public void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _character = DIContainer.GetImplementation<IFightManager>().First();
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
            LooseHpCommand command = new LooseHpCommand(_character, 10);

            _mediator.Execute(command);
            Assert.AreEqual(40, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void Full()
        {
            LooseHpCommand command = new LooseHpCommand(_character, 50);

            _mediator.Execute(command);
            Assert.AreEqual(0, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void Overflow()
        {
            LooseHpCommand command = new LooseHpCommand(_character, 60);

            _mediator.Execute(command);
            Assert.AreEqual(0, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void WrongUndo()
        {
            LooseHpCommand command = new LooseHpCommand(_character, 50);

            Assert.Throws<NullReferenceException>(() => _mediator.Undo(command));
        }

        [Test]
        public void ReturnValue()
        {
            LooseHpCommand command = new LooseHpCommand(_character, 50);

            IMediatorCommandResponse response = _mediator.Execute(command);
            MediatorCommandNoResponse _response = response as MediatorCommandNoResponse;

            Assert.IsNotNull(_response);
        }
    }
}
