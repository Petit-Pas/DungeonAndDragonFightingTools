using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.RetrySavingCommands;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.Status.EndStatusCommands
{
    [TestFixture]
    public class RetrySavingCommandTest
    {
        private IMediator _mediator;
        private IStatusProvider _statusProvider;
        private IFigtherProvider _fighterProvider;

        private IMediatorHandler _savingQueryHandler;
        private bool _queryCanceled;
        private SavingThrow _success;
        private SavingThrow _failure;
        private SavingThrow _savingToReturn;

        private OnHitStatus _onHitStatus;
        private StatusReference _statusReference;

        [SetUp]
        public void SetUp()
        {
            _statusProvider = DIContainer.GetImplementation<IStatusProvider>();
            _mediator = DIContainer.GetImplementation<IMediator>();
            _fighterProvider = DIContainer.GetImplementation<IFigtherProvider>();

            _savingQueryHandler = A.Fake<IMediatorHandler>();
            _queryCanceled = false;
            _success = SavingThrowFactory.Successful(_fighterProvider.First());
            _failure = SavingThrowFactory.Failed(_fighterProvider.First());
            _savingToReturn = _success;
            A.CallTo(() => _savingQueryHandler.Execute(A<IMediatorCommand>._)).ReturnsLazily((x) => new ValidableResponse<SavingThrow>(!_queryCanceled, _savingToReturn));
            _mediator.RegisterHandler(_savingQueryHandler, typeof(SavingThrowQuery));

            _onHitStatus = StatusFactory.Slow;
            _onHitStatus.TargetName = _fighterProvider.First().DisplayName;
            _onHitStatus.CasterName = _fighterProvider.First().DisplayName;
            _statusReference = new StatusReference(_onHitStatus);
            _fighterProvider.First().AffectingStatusList.Add(_statusReference);
            _statusProvider.Add(_onHitStatus);
        }

        [TearDown]
        public void TearDown()
        {
            _statusProvider.Clear();
        }

        [Test]
        public void Should_Ask_For_A_Saving_Throw()
        {
            // Arrange
            var command = new RetrySavingCommand(_statusReference.ActualStatusReferenceId);

            // Act
            var result = _mediator.Execute(command);

            // Assert
            A.CallTo(() => _savingQueryHandler.Execute(A<IMediatorCommand>._)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Should_Return_Success_When_Succesfull()
        {
            // Arrange
            var command = new RetrySavingCommand(_statusReference.ActualStatusReferenceId);

            // Act
            var result = _mediator.Execute(command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Success);
        }

        [Test]
        public void Should_Remove_Status_When_Succesfull()
        {
            // Arrange
            var command = new RetrySavingCommand(_statusReference.ActualStatusReferenceId);

            // Act
            _mediator.Execute(command);

            // Assert
            command.InnerCommands.Should().HaveCount(1);
            command.InnerCommands.First().Should().BeAssignableTo<RemoveStatusCommand>();
        }

        [Test]
        public void Should_Return_Failed_When_Failed()
        {
            // Arrange
            var command = new RetrySavingCommand(_statusReference.ActualStatusReferenceId);
            _savingToReturn = _failure;

            // Act
            var result = _mediator.Execute(command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Failed);
        }

        [Test]
        public void Should_Return_Canceled_When_Saving_Is_Canceled()
        {
            // Arrange
            var command = new RetrySavingCommand(_statusReference.ActualStatusReferenceId);
            _queryCanceled = true;

            // Act
            var result = _mediator.Execute(command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Canceled);
        }

        [Test]
        public void Should_Return_Canceled_When_Status_Does_Not_Exist()
        {
            // Arrange
            var command = new RetrySavingCommand(new Guid());

            // Act
            var result = _mediator.Execute(command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Canceled);
        }
    }
}
