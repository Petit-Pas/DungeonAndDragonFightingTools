using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.InspirationCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.InspirationCommands.AcquireInspiration;
using DnDToolsLibrary.Entities.EntitiesCommands.InspirationCommands.InvertInspiration;
using DnDToolsLibrary.Entities.EntitiesCommands.InspirationCommands.LoseInspiration;
using DnDToolsLibrary.Fight;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.PlayableEntities.Inspirations
{
    [TestFixture]
    public class InvertInspirationCommandTests
    {
        private Character _character;
        private IMediator _mediator;
        private InvertInspirationCommand _command;

        [SetUp]
        public void SetUp()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _character = EntitiesFactory.GetWarrior() as Character;
            FightersList.Instance.AddOrUpdateFighter(_character);
            _command = new InvertInspirationCommand(_character.DisplayName);
        }

        [Test]
        public void Should_Lose_Concentration_When_He_Had()
        {
            // Arrange
            _character.HasInspiration = true;

            // Act
            var result = _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().ContainItemsAssignableTo<LoseInspirationCommand>();
            result.Should().Be(MediatorCommandStatii.NoResponse);
        }

        [Test]
        public void Should_Acquire_Concentration_When_Had_Not()
        {
            // Arrange
            _character.HasInspiration = false;

            // Act
            var result = _mediator.Execute(_command);

            // Assert
            _command.PeekLastInnerCommand().Should().BeAssignableTo<AcquireInspirationCommand>();
            result.Should().Be(MediatorCommandStatii.NoResponse);
        }
    }
}
