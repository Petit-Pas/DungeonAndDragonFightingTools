using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ActionCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.ReactionCommands;
using DnDToolsLibrary.Fight;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.PlayableEntities.Actions.Reaction;

[TestFixture]
public class InvertReactionAvailabilityCommandTests
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
    public void Should_Add_Reaction_As_Inner_Command_When_Unavailable()
    {
        // Arrange
        var command = new InvertReactionAvailabilityCommand(_character.DisplayName);
        _character.HasReaction = false;

        // Act
        _mediator.Execute(command);

        // Assert
        command.InnerCommands.Should().ContainItemsAssignableTo<AddReactionCommand>();
    }

    [Test]
    public void Should_Remove_Reaction_As_Inner_Command_When_Available()
    {
        // Arrange
        var command = new InvertReactionAvailabilityCommand(_character.DisplayName);
        _character.HasReaction = true;

        // Act
        _mediator.Execute(command);

        // Assert
        command.PeekLastInnerCommand().Should().BeAssignableTo<RemoveReactionCommand>();
    }
}