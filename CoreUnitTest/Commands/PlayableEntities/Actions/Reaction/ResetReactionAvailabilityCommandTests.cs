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
public class ResetReactionAvailabilityCommandTests
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
        var command = new ResetReactionAvailabilityCommand(_character.DisplayName);
        _character.HasReaction = false;

        // Act
        _mediator.Execute(command);

        // Arrange
        _character.HasReaction.Should().Be(true);
    }

    [Test]
    public void Should_Return_Success_When_Successful()
    {
        // Arrange
        var command = new ResetReactionAvailabilityCommand(_character.DisplayName);
        _character.HasReaction = false;

        // Act
        var response = _mediator.Execute(command);

        // Arrange
        response.Should().Be(MediatorCommandStatii.Success);
    }

    [Test]
    public void Should_Not_Do_Anything_When_Action_Was_Available()
    {
        // Arrange
        var command = new ResetReactionAvailabilityCommand(_character.DisplayName);
        _character.HasReaction = true;

        // Act
        _mediator.Execute(command);

        // Arrange
        _character.HasReaction.Should().Be(true);
    }

    [Test]
    public void Should_Return_Canceled_When_Successful()
    {
        // Arrange
        var command = new ResetReactionAvailabilityCommand(_character.DisplayName);
        _character.HasReaction = true;

        // Act
        var response = _mediator.Execute(command);

        // Arrange
        response.Should().Be(MediatorCommandStatii.Canceled);
    }
}