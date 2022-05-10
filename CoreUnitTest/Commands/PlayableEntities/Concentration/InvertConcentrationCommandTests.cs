using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.AcquireConcentration;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.InvertConcentration;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration;
using DnDToolsLibrary.Fight;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.PlayableEntities.Concentration
{
    [TestFixture]
    public class InvertConcentrationCommandTests
    {
        private PlayableEntity _character;
        private IMediator _mediator;
        private InvertConcentrationCommand _command;

        [SetUp]
        public void SetUp()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _character = EntitiesFactory.GetWarrior();
            DIContainer.GetImplementation<IFightersProvider>().AddOrUpdateFighter(_character);
            _command = new InvertConcentrationCommand(_character.DisplayName);
        }

        [Test]
        public void Should_Lose_Concentration_When_He_Had()
        {
            // Arrange
            _character.IsFocused = true;

            // Act
            var result = _mediator.Execute(_command);

            // Assert
            _command.InnerCommands.Should().ContainItemsAssignableTo<LoseConcentrationCommand>();
            result.Should().Be(MediatorCommandStatii.NoResponse);
        }

        [Test]
        public void Should_Acquire_Concentration_When_Had_Not()
        {
            // Arrange
            _character.IsFocused = false;

            // Act
            var result = _mediator.Execute(_command);

            // Assert
            _command.PeekLastInnerCommand().Should().BeAssignableTo<AcquireConcentrationCommand>();
            result.Should().Be(MediatorCommandStatii.NoResponse);
        }
    }
}
