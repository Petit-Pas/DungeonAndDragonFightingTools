using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseTempHp;
using DnDToolsLibrary.Fight;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
            _character.TempHp = 10;
            LooseTempHpCommand command = new LooseTempHpCommand(_character, 5);

            _mediator.Execute(command);
            Assert.AreEqual(5, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(10, _character.TempHp);
        }

        [Test]
        public void Full()
        {
            _character.TempHp = 10;
            LooseTempHpCommand command = new LooseTempHpCommand(_character, 10);

            _mediator.Execute(command);
            Assert.AreEqual(0, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(10, _character.TempHp);
        }

        [Test]
        public void Overflow()
        {
            _character.TempHp = 10;
            LooseTempHpCommand command = new LooseTempHpCommand(_character, 15);

            Assert.Throws<InvalidOperationException>(() => _mediator.Execute(command));
        }

        [Test]
        public void WrongUndo()
        {
            _character.TempHp = 10;
            LooseTempHpCommand command = new LooseTempHpCommand(_character, 10);

            Assert.Throws<NullReferenceException>(() => _mediator.Undo(command));
        }
        
        [Test]
        public void ReturnValue()
        {
            _character.TempHp = 10;
            LooseTempHpCommand command = new LooseTempHpCommand(_character, 10);

            IMediatorCommandResponse response = _mediator.Execute(command);
            NoResponse _response = response as NoResponse;

            Assert.IsNotNull(_response);
        }
    }
}
