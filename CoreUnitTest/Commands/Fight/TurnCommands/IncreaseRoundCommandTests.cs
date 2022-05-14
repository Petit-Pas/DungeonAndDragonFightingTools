using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Fight.FightCommands.TurnCommands.IncreaseRoundCommands;
using DnDToolsLibrary.Fight.FightCommands.TurnCommands.IncreaseTurnCommands;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.Fight.TurnCommands
{
    [TestFixture]
    public class IncreaseRoundCommandTests
    {
        private IFightersProvider _fightersProvider;
        private ITurnManager _turnManager;
        private IMediator _mediator;


        private IncreaseRoundCommand _command;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fightersProvider = DIContainer.GetImplementation<IFightersProvider>();
            _turnManager = DIContainer.GetImplementation<ITurnManager>();
            _mediator = DIContainer.GetImplementation<IMediator>();

            _fightersProvider.Clear();

            _fightersProvider.AddFighter(EntitiesFactory.Goblin);
            _fightersProvider.AddFighter(EntitiesFactory.Goblin);
            _fightersProvider.AddFighter(EntitiesFactory.Goblin);

            _command = new IncreaseRoundCommand();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _fightersProvider.Clear();
        }

        [SetUp]
        public void SetUp()
        {
            _turnManager.TurnIndex = 3;
            _turnManager.RoundCount = 1;
        }

        [Test]
        public void Should_Return_Canceled_When_Not_End_Of_Round()
        {
            // Arrange
            _turnManager.TurnIndex = 1;

            // Act
            var result = _mediator.Execute(_command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Canceled);
        }

        [Test]
        public void Should_Reset_TurnIndex()
        {
            // Arrange
            // Act
            _mediator.Execute(_command);

            // Assert
            _turnManager.TurnIndex.Should().Be(0);
        }

        [Test]
        public void Should_Increase_Round_Count()
        {
            // Arrange
            // Act
            _mediator.Execute(_command);

            // Assert
            _turnManager.RoundCount.Should().Be(2);
        }

        [Test]
        public void Should_Return_Success()
        {
            // Arrange
            // Act
            var result = _mediator.Execute(_command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Success);
        }

        [Test]
        public void Undo_Should_Decrease_RoundCount()
        {
            // Arrange
            // Act
            _mediator.Undo(_command);

            // Assert
            _turnManager.RoundCount.Should().Be(0);
        }

        [Test]
        public void Undo_Should_Reset_TurnIndex()
        {
            // Arrange
            // Act
            _mediator.Undo(_command);

            // Assert
            _turnManager.TurnIndex.Should().Be(3);
        }
    }
}
