using System.Linq;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Extensions;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.BonusActionCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.ApplyDotCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.StartTurnCommands;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.ReduceRemainingRoundsCommands;
using DnDToolsLibrary.Status.StatusCommands.EndStatusCommands.RetrySavingCommands;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.PlayableEntities.TurnCommands
{
    [TestFixture]
    public class StartTurnCommandTests
    {
        private IMediator _mediator;
        private IFightManager _fightManager;
        private PlayableEntity _character;
        private PlayableEntity _character2;
        private StartTurnCommand _command;
        private IStatusProvider _statusProvider;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fightManager = DIContainer.GetImplementation<IFightManager>();
            _fightManager.Clear();
            _mediator = DIContainer.GetImplementation<IMediator>();
            _statusProvider = DIContainer.GetImplementation<IStatusProvider>();
            _statusProvider.Clear();

            var savingHandler = A.Fake<IMediatorHandler>();
            A.CallTo(() => savingHandler.Execute(A<IMediatorCommand>._)).Returns(A.Fake<SavingThrow>());
            _mediator.RegisterHandler(savingHandler, typeof(SavingThrowQuery));
        }

        [SetUp]
        public void SetUp()
        {
            _character = EntitiesFactory.GetWarrior();
            _character2 = EntitiesFactory.GetWizard();

            _fightManager.AddOrUpdateFighter(_character);
            _fightManager.AddOrUpdateFighter(_character2);

            _command = new StartTurnCommand(_character.DisplayName);
            InitDots();
            InitRetries();
            InitReduces();
        }

        [TearDown]
        public void TearDown()
        {
            _statusProvider.Clear();
        }

        private OnHitStatus _dotStartCaster;
        private OnHitStatus _dotEndCaster;
        private OnHitStatus _dotStartAffected;
        private OnHitStatus _dotEndAffected;

        private OnHitStatus _retryStart;
        private OnHitStatus _retryEnd;
        private OnHitStatus _noRetry;

        private OnHitStatus _reduceOnStartCaster;
        private OnHitStatus _reduceOnEndCaster;
        private OnHitStatus _reduceOnStartAffected;
        private OnHitStatus _reduceOnEndAffected;

        private void InitReduces()
        {
            _reduceOnStartCaster = StatusFactory.Bless;
            _reduceOnEndCaster = StatusFactory.Bless;
            _reduceOnStartAffected = StatusFactory.Bless;
            _reduceOnEndAffected = StatusFactory.Bless;

            _reduceOnStartCaster.DurationIsBasedOnStartOfTurn = true;
            _reduceOnStartCaster.DurationIsCalculatedOnCasterTurn = true;

            _reduceOnEndCaster.DurationIsBasedOnEndOfTurn = true;
            _reduceOnEndCaster.DurationIsCalculatedOnCasterTurn = true;

            _reduceOnStartAffected.DurationIsBasedOnStartOfTurn = true;
            _reduceOnStartAffected.DurationIsCalculatedOnTargetTurn = true;

            _reduceOnEndAffected.DurationIsBasedOnEndOfTurn = true;
            _reduceOnEndAffected.DurationIsCalculatedOnTargetTurn = true;
        }

        private void InitRetries()
        {
            _retryStart = StatusFactory.Slow;
            _retryEnd = StatusFactory.Slow;
            _noRetry = StatusFactory.Bless;

            _retryStart.SavingIsRemadeAtStartOfTurn = true;
            _retryEnd.SavingIsRemadeAtEndOfTurn = true;
        }

        private void InitDots()
        {
            _dotStartCaster = StatusFactory.InfernalWound;
            _dotEndCaster = StatusFactory.InfernalWound;
            _dotStartAffected = StatusFactory.InfernalWound;
            _dotEndAffected = StatusFactory.InfernalWound;

            _dotStartCaster.DotDamageList.First().TriggersStartOfTurn = true;
            _dotStartCaster.DotDamageList.First().TriggersOnCastersTurn = true;
            _dotStartCaster.DisplayName = "_dotStartCaster";

            _dotEndCaster.DotDamageList.First().TriggersEndOfTurn = true;
            _dotEndCaster.DotDamageList.First().TriggersOnCastersTurn = true;
            _dotEndCaster.DisplayName = "_dotEndCaster";

            _dotStartAffected.DotDamageList.First().TriggersStartOfTurn = true;
            _dotStartAffected.DotDamageList.First().TriggersOnAffectedsTurn = true;
            _dotStartAffected.DisplayName = "_dotStartAffected";

            _dotEndAffected.DotDamageList.First().TriggersEndOfTurn = true;
            _dotEndAffected.DotDamageList.First().TriggersOnAffectedsTurn = true;
            _dotEndAffected.DisplayName = "_dotEndAffected";
        }

        private void ApplyStatus(PlayableEntity target, PlayableEntity caster, OnHitStatus status)
        {
            status.TargetName = target.DisplayName;
            status.CasterName = caster.DisplayName;
            target.AffectingStatusList.Add(new StatusReference(status));
            _statusProvider.Add(status);
        }

        [Test]
        public void Should_Reset_Action()
        {
            // Arrange
            // Act
            _mediator.Execute(_command);
            
            //Assert
            _command.InnerCommands.Should().ContainItemsAssignableTo<ResetActionAvailabilityCommand>();
        }

        [Test]
        public void Should_Reset_Reaction()
        {
            // Arrange
            // Act
            _mediator.Execute(_command);

            //Assert
            _command.InnerCommands.Should().ContainItemsAssignableTo<ResetReactionAvailabilityCommand>();
        }

        [Test]
        public void Should_Reset_Bonus_Action()
        {
            // Arrange
            // Act
            _mediator.Execute(_command);

            //Assert
            _command.InnerCommands.Should().ContainItemsAssignableTo<ResetBonusActionAvailabilityCommand>();
        }

        [Test]
        public void Should_Not_Apply_Dots_That_Apply_At_End_Of_Turn()
        {
            // Arrange
            ApplyStatus(_character,_character2, _dotEndAffected);
            ApplyStatus(_character, _character2, _dotEndCaster);

            // Act
            _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().OnlyContain(x => x.GetType() != typeof(ApplyDotCommand));
        }

        [Test]
        public void Should_Apply_Casted_Dots_That_Triggers_On_Start_Of_Turn()
        {
            // Arrange
            ApplyStatus(_character2, _character, _dotStartCaster);

            // Act
            _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().ContainSingle(x => x.IsOfType(typeof(ApplyDotCommand))
                                                               && ((ApplyDotCommand)x).StatusReference == _dotStartCaster.Id);
        }

        [Test]
        public void Should_Apply_Affecting_Dots_That_Triggers_On_Start_Of_Turn()
        {
            // Arrange
            ApplyStatus(_character, _character2, _dotStartAffected);

            // Act
            _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().ContainSingle(x => x.IsOfType(typeof(ApplyDotCommand))
                                                               && ((ApplyDotCommand)x).StatusReference == _dotStartAffected.Id);
        }

        [Test]
        public void Should_Retry_Affecting_Saving_When_Based_On_Start_Of_Turn()
        {
            // Arrange
            ApplyStatus(_character, _character2, _retryStart);

            // Act
            _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().ContainItemsAssignableTo<RetrySavingCommand>();
        }

        [Test]
        public void Should_Not_Retry_Affecting_Saving_When_Based_On_End_Of_Turn()
        {
            // Arrange
            ApplyStatus(_character, _character2, _retryEnd);

            // Act
            _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().NotContain(x => x.IsOfType(typeof(RetrySavingCommand)));
        }

        [Test]
        public void Should_Not_Retry_Casted_Saving()
        {
            // Arrange
            ApplyStatus(_character, _character2, _noRetry);

            // Act
            _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().NotContain(x => x.IsOfType(typeof(RetrySavingCommand)));
        }

        [Test]
        public void Should_Not_Reduce_Remaining_Rounds_When_Based_On_End_Of_Turn()
        {
            // Arrange
            ApplyStatus(_character, _character2, _reduceOnEndAffected);
            ApplyStatus(_character, _character2, _reduceOnEndCaster);

            // Act
            _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().NotContain(x => x.IsOfType(typeof(ReduceRemainingRoundsCommand)));
        }

        [Test]
        public void Should_Reduce_Remaining_Rounds_When_Based_On_Affected_Start_Of_Turn()
        {
            // Arrange
            ApplyStatus(_character, _character2, _reduceOnStartAffected);

            // Act
            _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().ContainSingle(x => x.IsOfType(typeof(ReduceRemainingRoundsCommand)));
        }

        [Test]
        public void Should_Reduce_Remaining_Rounds_When_Based_On_Caster_Start_Of_Turn()
        {
            // Arrange
            ApplyStatus(_character2, _character, _reduceOnStartCaster);

            // Act
            _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().ContainSingle(x => x.IsOfType(typeof(ReduceRemainingRoundsCommand)));
        }

        [Test]
        public void Should_Raise_Turn_Ended()
        {
            // Arrange
            var commandArg = "";
            _character.TurnStarted += (_, args) => commandArg = args.EntityName;
            ApplyStatus(_character, _character2, _reduceOnEndCaster);

            // Act
            _mediator.Execute(_command);

            // Assert
            commandArg.Should().Be(_character.DisplayName);
        }
    }
}
