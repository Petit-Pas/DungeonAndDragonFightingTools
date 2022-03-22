using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.AddStatus;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using NUnit.Framework;
using System;
using System.Linq;

namespace CoreUnitTest.Commands.PlayableEntities.Status
{
    [TestFixture]
    public class AddStatusCommandTests
    {
        private IMediator _mediator;
        private IStatusProvider _statusProvider;
        private PlayableEntity _character;

        // occurs once before every test in this class
        [OneTimeSetUp]
        public void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _statusProvider = DIContainer.GetImplementation<IStatusProvider>();
            _character = FightersList.Instance[0];
        }

        // occurs before each test
        [SetUp]
        public void Setup()
        {
            _statusProvider.Clear();
            _character.AffectingStatusList.Clear();
        }

        [Test]
        public void StatusIsWellApplied()
        {
            OnHitStatus status = StatusFactory.InfernalWound;
            status.Target = _character;
            status.Caster = _character;
            AddStatusCommand command = new AddStatusCommand(_character, status);

            _mediator.Execute(command);

            Assert.AreEqual(1, _character.AffectingStatusList.Count);
            Assert.AreEqual(1, _statusProvider.Count);
        }

        [Test]
        public void UndoWorks()
        {
            OnHitStatus status = StatusFactory.InfernalWound;
            status.Target = _character;
            status.Caster = _character;
            AddStatusCommand command = new AddStatusCommand(_character, status);

            _mediator.Execute(command);
            _mediator.Undo(command);

            Assert.AreEqual(0, _character.AffectingStatusList.Count);
            Assert.AreEqual(0, _statusProvider.Count);
        }

        [Test]
        public void StatusReferenceCorresponds()
        {
            OnHitStatus status = StatusFactory.InfernalWound;
            status.Target = _character;
            status.Caster = _character;
            AddStatusCommand command = new AddStatusCommand(_character.DisplayName, status);

            _mediator.Execute(command);

            Assert.AreEqual(_character.AffectingStatusList[0].ActualStatusReferenceId, _statusProvider[0].Id);
        }

        [Test]
        public void StatusReferenceCorresponds_IdWasPresent()
        {
            OnHitStatus status = StatusFactory.InfernalWound;
            status.Target = _character;
            status.Caster = _character;
            status.Id = new Guid();
            AddStatusCommand command = new AddStatusCommand(_character, status);

            _mediator.Execute(command);

            Assert.AreEqual(_character.AffectingStatusList.First().ActualStatusReferenceId, _statusProvider.First().Id);
        }

        [Test]
        public void UndoRemoveCorrectStatusFromMany()
        {
            OnHitStatus status = StatusFactory.InfernalWound;
            status.Target = _character;
            status.Caster = _character;
            status.Id = new Guid();
            AddStatusCommand command = new AddStatusCommand(_character, status);

            _statusProvider.Add(StatusFactory.Slow);
            _mediator.Execute(command);
            _statusProvider.Add(StatusFactory.Slow);
            _mediator.Undo(command);

            Assert.IsNull(_statusProvider.FirstOrDefault(x => x.Id == status.Id));
        }
    }
}
