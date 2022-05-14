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
    public class IncreaseTurnCommandTests
    {
        private IFightersProvider _fightersProvider;
        private ITurnManager _turnManager;
        private IMediator _mediator;


        private IncreaseTurnCommand _command;

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

            _command = new IncreaseTurnCommand();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _fightersProvider.Clear();
        }

        [SetUp]
        public void SetUp()
        {
            _turnManager.TurnIndex = 1;
        }

        [Test]
        public void Should_Increase_Turn_Index()
        {
            // Arrange
            // Act
            _mediator.Execute(_command);
            
            // Assert
            _turnManager.TurnIndex.Should().Be(2);
        }

        [Test]
        public void Should_Return_NoResponse()
        {
            // Arrange
            // Act
            var result = _mediator.Execute(_command);

            // Assert
            result.Should().Be(MediatorCommandStatii.NoResponse);
        }

        [Test]
        public void Should_Increase_Round_When_Final_Turn()
        {
            // Arrange
            _turnManager.TurnIndex = 2;
            // Act
            _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().ContainItemsAssignableTo<IncreaseRoundCommand>();
        }

        [Test]
        public void Undo_Should_Reduce_Turn_Index()
        {
            // Arrange
            // Act
            _mediator.Undo(_command);

            // Assert
            _turnManager.TurnIndex.Should().Be(0);
        }
    }
}
