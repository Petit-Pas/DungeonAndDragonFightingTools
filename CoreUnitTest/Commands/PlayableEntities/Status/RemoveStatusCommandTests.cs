using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreUnitTest.Commands.PlayableEntities.Status
{
    [TestFixture]
    public class RemoveStatusCommandTests
    {
        private IMediator _mediator;
        private IStatusProvider _statusProvider;
        private PlayableEntity _character;

        // occurs once before every test in this class
        [OneTimeSetUp]
        public virtual void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _statusProvider = DIContainer.GetImplementation<IStatusProvider>();
            _character = FightersList.Instance[0];
        }

        // occurs before each test
        [SetUp]
        public virtual void Setup()
        {
            _statusProvider.Clear();
            _character.AffectingStatusList.Clear();
        }

        [TestFixture]
        public class RemoveStatusCommandTests_StatusRemoved : RemoveStatusCommandTests
        {
            protected OnHitStatus status;
            protected StatusReference statusReference;

            [SetUp]
            public override void Setup()
            {
            }

            [OneTimeSetUp]
            public override void MainSetup()
            {
                base.MainSetup();
                base.Setup();

                // arrange
                status = StatusFactory.Slow;
                statusReference = new StatusReference(status);
                _statusProvider.Add(status);
                _character.AffectingStatusList.Add(statusReference);
                RemoveStatusCommand command = new RemoveStatusCommand(status.Id, _character.DisplayName);

                // act
                _mediator.Execute(command);
            }

            [Test]
            public void StatusRemovedFromCharacter()
            {
                // assert
                Assert.AreEqual(0, _character.AffectingStatusList.Count);
            }

            [Test]
            public void StatusRemovedFromStatusProvider()
            {
                // assert
                Assert.AreEqual(0, _statusProvider.Count);
            }
        }

        [TestFixture]
        public class RemoveStatusCommandTests_StatusAddedOnUndo : RemoveStatusCommandTests
        {
            protected OnHitStatus status;
            protected StatusReference statusReference;

            [SetUp]
            public override void Setup()
            {
            }

            [OneTimeSetUp]
            public override void MainSetup()
            {
                base.MainSetup();
                base.Setup();

                // arrange
                status = StatusFactory.Slow;
                statusReference = new StatusReference(status);
                _statusProvider.Add(status);
                _character.AffectingStatusList.Add(statusReference);
                RemoveStatusCommand command = new RemoveStatusCommand(status.Id, _character.DisplayName);

                // act
                _mediator.Execute(command);
                _mediator.Undo(command);
            }

            [Test]
            public void StatusAddedBackToCharacter()
            {
                // assert
                Assert.AreEqual(1, _character.AffectingStatusList.Count);
                Assert.AreEqual(status.Id, _character.AffectingStatusList[0].ActualStatusReferenceId);
            }

            [Test]
            public void StatusAddedBackToStatusProvider()
            {
                // assert
                Assert.AreEqual(1, _statusProvider.Count);
                Assert.AreEqual(status.Id, _statusProvider[0].Id);
            }
        }
    }
}
