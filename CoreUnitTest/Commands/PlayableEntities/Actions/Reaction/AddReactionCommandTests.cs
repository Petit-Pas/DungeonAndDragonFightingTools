using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands;
using DnDToolsLibrary.Fight;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.PlayableEntities.Actions.Reaction;

[TestFixture]
public class AddActionCommandTests
{
    private PlayableEntity _character;
    private IMediator _mediator;

    [SetUp]
    public void SetUp()
    {
        _mediator = DIContainer.GetImplementation<IMediator>();
        _character = EntitiesFactory.GetWarrior();
        FightersList.Instance.AddOrUpdateFighter(_character);
    }

    [Test]
    [Order(1)]
    public void Should_Add_Reaction()
    {
        // Arrange
        var command = new AddReactionCommand(_character.DisplayName);
        _character.HasReaction = false;

        // Act
        _mediator.Execute(command);

        // Assert
        _character.HasReaction.Should().Be(true);
    }

    [Test]
    [Order(2)]
    public void Should_Return_Success_When_Successful()
    {
        // Arrange
        var command = new AddReactionCommand(_character.DisplayName);
        _character.HasReaction = false;

        // Act
        var response = _mediator.Execute(command);

        // Assert
        response.Should().Be(MediatorCommandStatii.Success);
    }

    [Test]
    [Order(3)]
    public void Undo_Should_Remove_Reaction()
    {
        // Arrange
        var command = new AddReactionCommand(_character.DisplayName);
        _character.HasReaction = false;

        // Act
        _mediator.Execute(command);
        _mediator.Undo(command);

        // Assert
        _character.HasReaction.Should().Be(false);
    }

    [Test]
    [Order(1)]
    public void Should_Not_Do_Anything_When_Reaction_Is_Already_Available()
    {
        // Arrange
        var command = new AddReactionCommand(_character.DisplayName);
        _character.HasReaction = true;

        // Act
        _mediator.Execute(command);

        // Assert
        _character.HasReaction.Should().Be(true);
    }

    [Test]
    [Order(2)]
    public void Should_Return_Canceled_When_Reaction_Is_Already_Available()
    {
        // Arrange
        var command = new AddReactionCommand(_character.DisplayName);
        _character.HasReaction = true;

        // Act
        var response = _mediator.Execute(command);

        // Assert
        response.Should().Be(MediatorCommandStatii.Canceled);
    }

    [Test]
    [Order(3)]
    public void Undo_Should_Not_Change_Anything_When_Reaction_Was_Already_Available()
    {
        // Arrange
        var command = new AddReactionCommand(_character.DisplayName);
        _character.HasReaction = true;

        // Act
        _mediator.Execute(command);
        _mediator.Undo(command);

        // Assert
        _character.HasReaction.Should().Be(true);
    }
}