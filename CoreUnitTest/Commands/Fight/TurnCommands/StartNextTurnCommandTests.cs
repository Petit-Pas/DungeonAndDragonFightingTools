using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.EndTurnCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.TurnCommands.StartTurnCommands;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Fight.FightCommands.TurnCommands.IncreaseTurnCommands;
using DnDToolsLibrary.Fight.FightCommands.TurnCommands.StartNextTurnCommands;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.Fight.TurnCommands
{
    [TestFixture]
    public class StartNextTurnCommandTests
    {
        private IMediator _mediator;
        private IFightersProvider _fightersProvider;
        private ITurnManager _turnManager;

        private StartNextTurnCommand _command;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fightersProvider = DIContainer.GetImplementation<IFightersProvider>();
            _mediator = DIContainer.GetImplementation<IMediator>();
            _turnManager = DIContainer.GetImplementation<ITurnManager>();

            _fightersProvider.Clear();
            _turnManager.TurnIndex = 1;

            _fightersProvider.AddFighter(EntitiesFactory.Goblin);
            _fightersProvider.AddFighter(EntitiesFactory.Goblin);
            _fightersProvider.AddFighter(EntitiesFactory.Goblin);

            _command = new StartNextTurnCommand();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _fightersProvider.Clear();
        }


        [SetUp]
        public void SetUp()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();

            _command = new StartNextTurnCommand();
        }

        [Test]
        public void Should_End_Turn_Of_Currently_Playing_Fighter()
        {
            // Arrange
            // Act
            _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().ContainItemsAssignableTo<EndTurnCommand>();
        }

        [Test]
        public void Should_Increase_Turn()
        {
            // Arrange
            // Act
            _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().ContainItemsAssignableTo<IncreaseTurnCommand>();
        }

        [Test]
        public void Should_Start_Turn_Of_Next_Fighter()
        {
            // Arrange
            // Act
            _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().ContainItemsAssignableTo<StartTurnCommand>();
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
    }
}
