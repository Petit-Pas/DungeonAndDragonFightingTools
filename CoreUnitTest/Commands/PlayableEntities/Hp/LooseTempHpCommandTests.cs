using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseTempHp;
using DnDToolsLibrary.Fight;
using NUnit.Framework;
using System;

namespace CoreUnitTest.Commands.PlayableEntities.Hp
{
    [TestFixture]
    public class LooseTempHpCommandTests
    {
        private IMediator _mediator;
        private PlayableEntity _character;

        // occurs once before every test in this class
        [OneTimeSetUp]
        public void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _character = FightersList.Instance[0];
        }

        // occurs before each test
        [SetUp]
        public void Setup()
        {
            _character.TempHp = 10;
        }

        [Test]
        public void Basic()
        {
            LooseTempHpCommand command = new LooseTempHpCommand(_character, 5);

            _mediator.Execute(command);
            Assert.AreEqual(5, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(10, _character.TempHp);
        }

        [Test]
        public void Full()
        {
            LooseTempHpCommand command = new LooseTempHpCommand(_character, 10);

            _mediator.Execute(command);
            Assert.AreEqual(0, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(10, _character.TempHp);
        }

        [Test]
        public void Overflow()
        {
            LooseTempHpCommand command = new LooseTempHpCommand(_character, 15);

            Assert.Throws<InvalidOperationException>(() => _mediator.Execute(command));
        }

        [Test]
        public void WrongUndo()
        {
            LooseTempHpCommand command = new LooseTempHpCommand(_character, 10);

            Assert.Throws<NullReferenceException>(() => _mediator.Undo(command));
        }
        
        [Test]
        public void ReturnValue()
        {
            LooseTempHpCommand command = new LooseTempHpCommand(_character, 10);

            IMediatorCommandResponse response = _mediator.Execute(command);
            MediatorCommandNoResponse _response = response as MediatorCommandNoResponse;

            Assert.IsNotNull(_response);
        }
    }
}
