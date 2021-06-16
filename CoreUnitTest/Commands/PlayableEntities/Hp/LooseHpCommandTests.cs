using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseHp;
using DnDToolsLibrary.Fight;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
            _character = FightersList.Instance.Elements[0];
        }

        // occurs before each test
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Basic()
        {
            _character.Hp = 50;
            LooseHpCommand command = new LooseHpCommand(_character, 10);

            _mediator.Execute(command);
            Assert.AreEqual(40, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void Full()
        {
            _character.Hp = 50;
            LooseHpCommand command = new LooseHpCommand(_character, 50);

            _mediator.Execute(command);
            Assert.AreEqual(0, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void Overflow()
        {
            _character.Hp = 50;
            LooseHpCommand command = new LooseHpCommand(_character, 60);

            _mediator.Execute(command);
            Assert.AreEqual(0, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void WrongUndo()
        {
            _character.Hp = 50;
            LooseHpCommand command = new LooseHpCommand(_character, 50);

            Assert.Throws<NullReferenceException>(() => _mediator.Undo(command));
        }

        [Test]
        public void ReturnValue()
        {
            _character.Hp = 50;
            LooseHpCommand command = new LooseHpCommand(_character, 50);

            IMediatorCommandResponse response = _mediator.Execute(command);
            NoResponse _response = response as NoResponse;

            Assert.IsNotNull(_response);
        }
    }
}
