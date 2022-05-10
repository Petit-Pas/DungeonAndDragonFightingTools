
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
public class ResetActionAvailabilityCommandTests
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
        var command = new ResetActionAvailabilityCommand(_character.DisplayName);
        _character.HasAction = false;

        // Act
        _mediator.Execute(command);

        // Arrange
        _character.HasAction.Should().Be(true);
    }

    [Test]
    public void Should_Return_Success_When_Successful()
    {
        // Arrange
        var command = new ResetActionAvailabilityCommand(_character.DisplayName);
        _character.HasAction = false;

        // Act
        var response = _mediator.Execute(command);

        // Arrange
        response.Should().Be(MediatorCommandStatii.Success);
    }

    [Test]
    public void Should_Not_Do_Anything_When_Action_Was_Available()
    {
        // Arrange
        var command = new ResetActionAvailabilityCommand(_character.DisplayName);
        _character.HasAction = true;

        // Act
        _mediator.Execute(command);

        // Arrange
        _character.HasAction.Should().Be(true);
    }

    [Test]
    public void Should_Return_Canceled_When_Successful()
    {
        // Arrange
        var command = new ResetActionAvailabilityCommand(_character.DisplayName);
        _character.HasAction = true;

        // Act
        var response = _mediator.Execute(command);

        // Arrange
        response.Should().Be(MediatorCommandStatii.Canceled);
    }
}