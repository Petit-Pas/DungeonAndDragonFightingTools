
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
public class RemoveActionCommandTests
{
    private PlayableEntity _character;
    private IMediator _mediator;

    [SetUp]
    public void SetUp()
    {
        _mediator = DIContainer.GetImplementation<IMediator>();
        _character = EntitiesFactory.GetWarrior();
        DIContainer.GetImplementation<IFightersProvider>().AddOrUpdateFighter(_character);
    }

    [Test]
    [Order(1)]
    public void Should_Remove_Action()
    {
        // Arrange
        RemoveActionCommand command = new RemoveActionCommand(_character.DisplayName);
        _character.HasAction = true;

        // Act
        _mediator.Execute(command);

        // Assert
        _character.HasAction.Should().Be(false);
    }

    [Test]
    [Order(2)]
    public void Should_Return_Success_When_Successful()
    {
        // Arrange
        RemoveActionCommand command = new RemoveActionCommand(_character.DisplayName);
        _character.HasAction = true;

        // Act
        var response = _mediator.Execute(command);

        // Assert
        response.Should().Be(MediatorCommandStatii.Success);
    }

    [Test]
    [Order(3)]
    public void Undo_Should_Reset_Action()
    {
        // Arrange
        RemoveActionCommand command = new RemoveActionCommand(_character.DisplayName);
        _character.HasAction = true;

        // Act
        _mediator.Execute(command);
        _mediator.Undo(command);

        // Assert
        _character.HasAction.Should().Be(true);
    }

    [Test]
    [Order(1)]
    public void Should_Not_Do_Anything_When_Action_Is_Already_Unavailable()
    {
        // Arrange
        RemoveActionCommand command = new RemoveActionCommand(_character.DisplayName);
        _character.HasAction = false;

        // Act
        _mediator.Execute(command);

        // Assert
        _character.HasAction.Should().Be(false);
    }

    [Test]
    [Order(2)]
    public void Should_Return_Canceled_When_Action_Is_Already_Unavailable()
    {
        // Arrange
        RemoveActionCommand command = new RemoveActionCommand(_character.DisplayName);
        _character.HasAction = false;

        // Act
        var response = _mediator.Execute(command);

        // Assert
        response.Should().Be(MediatorCommandStatii.Canceled);
    }

    [Test]
    [Order(3)]
    public void Undo_Should_Not_Change_Anything_When_Action_Was_Already_Unavailable()
    {
        // Arrange
        RemoveActionCommand command = new RemoveActionCommand(_character.DisplayName);
        _character.HasAction = false;

        // Act
        _mediator.Execute(command);
        _mediator.Undo(command);

        // Assert
        _character.HasAction.Should().Be(false);
    }
}