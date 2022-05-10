using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;
using DnDToolsLibrary.Fight;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.PlayableEntities.Actions.Action;

[TestFixture]
public class InvertActionAvailabilityCommandTests
{
    private PlayableEntity _character;
    private IMediator _mediator;

    [SetUp]
    public void SetUp()
    {
        _mediator = DIContainer.GetImplementation<IMediator>();
        _character = EntitiesFactory.GetWarrior();
        DIContainer.GetImplementation<IFightManager>().AddOrUpdateFighter(_character);
    }

    [Test]
    public void Should_Add_Action_As_Inner_Command_When_Unavailable()
    {
        // Arrange
        var command = new InvertActionAvailabilityCommand(_character.DisplayName);
        _character.HasAction = false;

        // Act
        _mediator.Execute(command);

        // Assert
        command.InnerCommands.Should().ContainItemsAssignableTo<AddActionCommand>();
    }

    [Test]
    public void Should_Remove_Action_As_Inner_Command_When_Available()
    {
        // Arrange
        var command = new InvertActionAvailabilityCommand(_character.DisplayName);
        _character.HasAction = true;

        // Act
        _mediator.Execute(command);

        // Assert
        command.PeekLastInnerCommand().Should().BeAssignableTo<RemoveActionCommand>();
    }
}