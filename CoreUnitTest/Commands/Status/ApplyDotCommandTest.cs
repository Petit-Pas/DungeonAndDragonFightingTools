using System;
using System.Linq;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.QueryHandlers;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Attacks.AttacksCommands.DamageCommands.DamageResultListQueries;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.ApplyDotCommands;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.Status
{
    [TestFixture]
    public class ApplyDotCommandTest
    {
        private IMediator _mediator;
        private IStatusProvider _statusProvider;
        private IFightersProvider _fightersProvider;

        private IMediatorHandler _userInputHandler;
        private ValidableResponse<GetInputDamageResultListResponse> _userInputResponse;

        private OnHitStatus _status;
        private PlayableEntity _warrior;
        private PlayableEntity _wizard;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _statusProvider = DIContainer.GetImplementation<IStatusProvider>();
            _fightersProvider = DIContainer.GetImplementation<IFightersProvider>();
        }

        [SetUp]
        public void SetUp()
        {
            _statusProvider.Clear();
            _status = StatusFactory.InfernalWound;
            _warrior = EntitiesFactory.GetWarrior();
            _wizard = EntitiesFactory.GetWizard();
            _fightersProvider.AddOrUpdateFighter(EntitiesFactory.GetWarrior());

            _status.CasterName = _wizard.DisplayName;
            _status.TargetName = _warrior.DisplayName;
            var reference = new StatusReference(_status);
            _warrior.AffectingStatusList.Add(reference);
            _statusProvider.Add(_status);

            _userInputHandler = A.Fake<IMediatorHandler>();
            A.CallTo(() => _userInputHandler.Execute(A<IMediatorCommand>._))
                .ReturnsLazily(() => _userInputResponse);
            _mediator.RegisterHandler(_userInputHandler, typeof(DamageResultListQuery));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _statusProvider.Clear();
            _mediator.RegisterHandler(new DamageResultListQueryHandler(), typeof(DamageResultListQuery));
        }

        [Test]
        public void Should_Cancel_When_Status_Does_Not_Exist()
        {
            // Arrange
            var command = new ApplyDotCommand(Guid.Empty, true, false);

            // Act
            var result = _mediator.Execute(command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Canceled);
        }

        [Test]
        public void Should_Cancel_When_User_Input_Is_Canceled()
        {
            // Arrange
            var command = new ApplyDotCommand(_status.Id, true, false);
            _userInputResponse = new ValidableResponse<GetInputDamageResultListResponse>(false, null);

            // Act
            var result = _mediator.Execute(command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Canceled);
        }

        [Test]
        public void Should_Not_Apply_Damage_For_Wrong_Turn()
        {
            // Arrange
            var command = new ApplyDotCommand(_status.Id, true, true);
            _userInputResponse = new ValidableResponse<GetInputDamageResultListResponse>(true, null);

            // Act
            var result = _mediator.Execute(command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Canceled);
            A.CallTo(() => _userInputHandler.Execute(A<DamageResultListQuery>.That.Matches(x => x.DamageList.Any()))).MustNotHaveHappened();
        }

        [Test]
        public void Should_Not_Apply_Damage_For_Wrong_Character()
        {
            // Arrange
            var command = new ApplyDotCommand(_status.Id, false, false);
            _userInputResponse = new ValidableResponse<GetInputDamageResultListResponse>(true, null);

            // Act
            var result = _mediator.Execute(command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Canceled);
            A.CallTo(() => _userInputHandler.Execute(A<DamageResultListQuery>.That.Matches(x => x.DamageList.Any()))).MustNotHaveHappened();
        }

        [Test]
        public void Should_Apply_Damage_For_Right_Turn_And_Right_Character()
        {
            // Arrange
            var command = new ApplyDotCommand(_status.Id, true, false);
            _userInputResponse = new ValidableResponse<GetInputDamageResultListResponse>(true, new GetInputDamageResultListResponse(new DamageResultList() { DamageResult.Create("1d4", DamageTypeEnum.Acid) }) );

            // Act
            var result = _mediator.Execute(command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Success);
            A.CallTo(() => _userInputHandler.Execute(A<DamageResultListQuery>._)).MustHaveHappenedOnceExactly();
        }
    }
}
