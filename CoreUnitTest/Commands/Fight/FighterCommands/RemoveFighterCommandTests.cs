using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Fight.FightCommands.FighterCommands.RemoveFighterCommands;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.Fight.FighterCommands
{
    [TestFixture]
    public class RemoveFighterCommandTests
    {
        private IFightManager _fightManager;
        private IMediator _mediator;

        private RemoveFighterCommand _command;
        private PlayableEntity _character;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fightManager = DIContainer.GetImplementation<IFightManager>();
            _fightManager.Clear();
            _mediator = DIContainer.GetImplementation<IMediator>();
            _character = EntitiesFactory.GetWarrior();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _fightManager.Clear();
        }

        [SetUp]
        public void SetUp()
        {
            _fightManager.AddOrUpdateFighter(_character);
            _command = new RemoveFighterCommand(_character);
        }

        [Test]
        public void Should_Return_An_Error_When_Entity_Was_Not_Fighting()
        {
            // Arrange
            _fightManager.RemoveFighter(_character);

            // Act
            var result = _mediator.Execute(_command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Error);
        }

        [Test]
        public void Should_Return_Success_When_Successfull()
        {
            // Arrange
            // Act
            var result = _mediator.Execute(_command);

            // Assert
            result.Should().Be(MediatorCommandStatii.Success);
        }
        
        [Test]
        [Order(1)]
        public void Should_Remove_From_Fight()
        {
            // Arrange
            // Act
            _mediator.Execute(_command);

            // Assert
            _fightManager.GetAllFighters().Should().BeEmpty();
        }

        [Test]
        [Order(2)]
        public void Undo_Should_Add_Entity_Back_To_Fight()
        {
            // Arrange
            _mediator.Execute(_command);

            // Act
            _mediator.Undo(_command);

            // Assert
            _fightManager.GetAllFighters().Should().Contain(x => x.DisplayName == _character.DisplayName);
        }
    }
}
