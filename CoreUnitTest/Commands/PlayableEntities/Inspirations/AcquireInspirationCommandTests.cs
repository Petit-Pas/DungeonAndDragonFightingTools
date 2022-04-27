using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.InspirationCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.InspirationCommands.AcquireInspiration;
using DnDToolsLibrary.Fight;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.PlayableEntities.Inspirations
{
    [TestFixture]
    public class AcquireInspirationCommandTests
    {
        private PlayableEntity _monster;
        private PlayableEntity _character;

        private IFighterProvider _fighterProvider;
        private IMediator _mediator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _fighterProvider = DIContainer.GetImplementation<IFighterProvider>();
            _fighterProvider.Clear();
        }

        [SetUp]
        public void SetUp()
        {
            _character = EntitiesFactory.GetWarrior();
            _monster = EntitiesFactory.Goblin;

            _fighterProvider.AddOrUpdateFighter(_character);
            _fighterProvider.AddOrUpdateFighter(_monster);
        }

        [TearDown]
        public void TearDown()
        {
            _fighterProvider.Clear();
        }

        [Test]
        public void Should_Cancel_When_Entity_Is_A_Monster()
        {
            // Arrange
            var command = new AcquireInspirationCommand(_monster.DisplayName);
            
            // Act
            var result = _mediator.Execute(command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Canceled);
        }

        [Test]
        [Order(1)]
        public void Should_Cancel_When_Entity_Already_Has_An_Inspiration()
        {
            // Arrange
            var command = new AcquireInspirationCommand(_character.DisplayName);
            ((Character) _character).HasInspiration = true;

            // Act
            var result = _mediator.Execute(command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Canceled);
        }

        [Test]
        [Order(1)]
        public void Should_Grant_Inspiration()
        {
            // Arrange
            var command = new AcquireInspirationCommand(_character.DisplayName);
            var character = (Character)_character;

            character.HasInspiration = false;

            // Act
            _mediator.Execute(command);

            // Assert
            character.HasInspiration.Should().Be(true);
        }


        [Test]
        [Order(2)]
        public void Should_Return_Success_When_Granting_Inspiration()
        {
            // Arrange
            var command = new AcquireInspirationCommand(_character.DisplayName);
            var character = (Character)_character;

            character.HasInspiration = false;

            // Act
            var result = _mediator.Execute(command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Success);
        }

        [Test]
        [Order(2)]
        public void Undo_Should_Remove_Granted_Inspiration()
        {
            // Arrange
            var command = new AcquireInspirationCommand(_character.DisplayName);
            var character = (Character)_character;

            character.HasInspiration = false;
            _mediator.Execute(command);

            // Act
            _mediator.Undo(command);

            // Assert
            character.HasInspiration.Should().Be(false);
        }

        [Test]
        [Order(2)]
        public void Undo_Should_Do_Nothing_When_Command_Was_Canceled()
        {
            // Arrange
            var command = new AcquireInspirationCommand(_character.DisplayName);
            var character = (Character)_character;

            character.HasInspiration = true;
            _mediator.Execute(command);
            // ensures that if the command is undone, the inspiration remains as is since it was previously cancelled
            character.HasInspiration = false;

            // Act
            _mediator.Undo(command);

            // Assert
            character.HasInspiration.Should().Be(false);
        }
    }
}
