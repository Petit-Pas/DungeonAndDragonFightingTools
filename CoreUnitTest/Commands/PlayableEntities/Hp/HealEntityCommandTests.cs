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
    public class HealEntityCommandTests
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
        public void HealFull()
        {
            _character.Hp = 50;
            HealEntityCommand command = new HealEntityCommand(_character, 50);

            _mediator.Execute(command);
            Assert.AreEqual(100, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void HealNormal()
        {
            _character.Hp = 50;
            HealEntityCommand command = new HealEntityCommand(_character, 40);

            _mediator.Execute(command);
            Assert.AreEqual(90, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void OverHeal()
        {
            _character.Hp = 50;
            HealEntityCommand command = new HealEntityCommand(_character, 60);

            _mediator.Execute(command);
            Assert.AreEqual(100, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void NoHeal()
        {
            _character.Hp = 50;
            HealEntityCommand command = new HealEntityCommand(_character, 0);

            _mediator.Execute(command);
            Assert.AreEqual(50, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void NegativeHeal()
        {
            _character.Hp = 50;
            HealEntityCommand command = new HealEntityCommand(_character, -10);

            _mediator.Execute(command);
            Assert.AreEqual(50, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void WrongUndo()
        {
            _character.Hp = 50;
            HealEntityCommand command = new HealEntityCommand(_character, 50);

            Assert.Throws<NullReferenceException>(() => _mediator.Undo(command));
        }
    }
}
