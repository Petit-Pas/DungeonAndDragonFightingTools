using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.Heal;
using DnDToolsLibrary.Fight;
using NUnit.Framework;
using System;

namespace CoreUnitTest.Commands.PlayableEntities.Hp
{
    [TestFixture]
    public class HealCommandTests
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
            _character.Hp = 50;
        }

        [Test]
        public void HealFull()
        {
            HealCommand command = new HealCommand(_character, 50);

            _mediator.Execute(command);
            Assert.AreEqual(100, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void HealNormal()
        {
            HealCommand command = new HealCommand(_character, 40);

            _mediator.Execute(command);
            Assert.AreEqual(90, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void OverHeal()
        {
            HealCommand command = new HealCommand(_character, 60);

            _mediator.Execute(command);
            Assert.AreEqual(100, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void NoHeal()
        {
            HealCommand command = new HealCommand(_character, 0);

            _mediator.Execute(command);
            Assert.AreEqual(50, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void NegativeHeal()
        {
            HealCommand command = new HealCommand(_character, -10);

            _mediator.Execute(command);
            Assert.AreEqual(50, _character.Hp);

            _mediator.Undo(command);
            Assert.AreEqual(50, _character.Hp);
        }

        [Test]
        public void WrongUndo()
        {
            HealCommand command = new HealCommand(_character, 50);

            Assert.Throws<NullReferenceException>(() => _mediator.Undo(command));
        }

        [Test]
        public void ReturnValue()
        {
            HealCommand command = new HealCommand(_character, 50);

            IMediatorCommandResponse response = _mediator.Execute(command);
            MediatorCommandNoResponse _response = response as MediatorCommandNoResponse;

            Assert.IsNotNull(_response);
        }
    }
}
