using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands;
using DnDToolsLibrary.Fight;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreUnitTest.Commands.PlayableEntities.Hp
{
    [TestFixture]
    public class TempHealEntityCommandTests
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
        public void NoTempHp()
        {
            _character.TempHp = 0;
            TempHealEntityCommand command = new TempHealEntityCommand(_character, 10);

            _mediator.Execute(command);
            Assert.AreEqual(10, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(0, _character.TempHp);
        }

        [Test]
        public void LowTempHp()
        {
            _character.TempHp = 5;
            TempHealEntityCommand command = new TempHealEntityCommand(_character, 10);

            _mediator.Execute(command);
            Assert.AreEqual(10, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(5, _character.TempHp);

        }

        [Test]
        public void HighTempHp()
        {
            _character.TempHp = 15;
            TempHealEntityCommand command = new TempHealEntityCommand(_character, 10);

            _mediator.Execute(command);
            Assert.AreEqual(15, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(15, _character.TempHp);

        }

        [Test]
        public void NoHealWithTemp()
        {
            _character.TempHp = 15;
            TempHealEntityCommand command = new TempHealEntityCommand(_character, 0);

            _mediator.Execute(command);
            Assert.AreEqual(15, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(15, _character.TempHp);

        }

        [Test]
        public void NoHealWithoutTemp()
        {
            _character.TempHp = 0;
            TempHealEntityCommand command = new TempHealEntityCommand(_character, 0);

            _mediator.Execute(command);
            Assert.AreEqual(0, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(0, _character.TempHp);

        }

        [Test]
        public void NegativeHealTemp()
        {
            _character.TempHp = 0;
            TempHealEntityCommand command = new TempHealEntityCommand(_character, -10);

            _mediator.Execute(command);
            Assert.AreEqual(0, _character.TempHp);

            _mediator.Undo(command);
            Assert.AreEqual(0, _character.TempHp);

        }

        [Test]
        public void WrongUndo()
        {
            _character.Hp = 50;
            TempHealEntityCommand command = new TempHealEntityCommand(_character, 50);

            Assert.Throws<NullReferenceException>(() => _mediator.Undo(command));

        }
    }
}
