using System;
using System.Linq;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.ReduceRemainingRoundsCommands;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.Status.EndStatusCommands
{
    [TestFixture]
    public class ReduceRemainingRoundsCommandTest
    {
        private IMediator _mediator;
        private IStatusProvider _statusProvider;
        private IFigtherProvider _fighterProvider;

        private OnHitStatus _onHitStatus;

        [OneTimeSetUp]
        public void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _statusProvider = DIContainer.GetImplementation<IStatusProvider>();
            _fighterProvider = DIContainer.GetImplementation<IFigtherProvider>();
        }

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _onHitStatus = new OnHitStatus()
            {
                RemainingRounds = 5,
                Id = Guid.NewGuid(),
                TargetName = _fighterProvider.First().DisplayName,
            };
            _fighterProvider.First().AffectingStatusList.Add(new StatusReference(_onHitStatus));
            _statusProvider.Add(_onHitStatus);
        }

        [TearDown]
        public void TearDown()
        {
            _statusProvider.Clear();
            _fighterProvider.First().AffectingStatusList.Clear();
        }

        [Test]
        public void Should_Reduce_The_Remaining_Rounds()
        {
            // Arrange
            var command = new ReduceRemainingRoundsCommand(_onHitStatus.Id);

            // Act
            _mediator.Execute(command);

            // Assert
            _onHitStatus.RemainingRounds.Should().Be(4);
        }

        [Test]
        public void Should_Return_Success()
        {
            // Arrange
            var command = new ReduceRemainingRoundsCommand(_onHitStatus.Id);

            // Act
            var result = _mediator.Execute(command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Success);
        }

        [Test]
        public void Should_Return_Canceled_When_Status_Does_Not_Exist()
        {
            // Arrange
            var command = new ReduceRemainingRoundsCommand(Guid.NewGuid());

            // Act
            var result = _mediator.Execute(command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Canceled);
        }

        [Test]
        public void Should_Remove_Status_When_RemainingRounds_Is_Zero()
        {
            // Arrange
            var command = new ReduceRemainingRoundsCommand(_onHitStatus.Id);
            _onHitStatus.RemainingRounds = 0;

            // Act
            var result = _mediator.Execute(command);

            // Assert
            command.InnerCommands.Should()
                .HaveCount(1)
                .And
                .ContainItemsAssignableTo<RemoveStatusCommand>();
        }
    }
}
