using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.ActionsCommands.BonusActionCommands;
using DnDToolsLibrary.Fight;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.PlayableEntities.Actions.BonusAction;

[TestFixture]
public class AddBonusActionCommandTests
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
    public void Should_Add_Bonus_Action()
    {
        // Arrange
        var command = new AddBonusActionCommand(_character.DisplayName);
        _character.HasBonusAction = false;

        // Act
        _mediator.Execute(command);

        // Assert
        _character.HasBonusAction.Should().Be(true);
    }

    [Test]
    [Order(2)]
    public void Should_Return_Success_When_Successful()
    {
        // Arrange
        var command = new AddBonusActionCommand(_character.DisplayName);
        _character.HasBonusAction = false;

        // Act
        var response = _mediator.Execute(command);

        // Assert
        response.Should().Be(MediatorCommandStatii.Success);
    }

    [Test]
    [Order(3)]
    public void Undo_Should_Remove_Bonus_Action()
    {
        // Arrange
        var command = new AddBonusActionCommand(_character.DisplayName);
        _character.HasBonusAction = false;

        // Act
        _mediator.Execute(command);
        _mediator.Undo(command);

        // Assert
        _character.HasBonusAction.Should().Be(false);
    }

    [Test]
    [Order(1)]
    public void Should_Not_Do_Anything_When_Bonus_Action_Is_Already_Available()
    {
        // Arrange
        var command = new AddBonusActionCommand(_character.DisplayName);
        _character.HasBonusAction = true;

        // Act
        _mediator.Execute(command);

        // Assert
        _character.HasBonusAction.Should().Be(true);
    }

    [Test]
    [Order(2)]
    public void Should_Return_Canceled_When_Bonus_Action_Is_Already_Available()
    {
        // Arrange
        var command = new AddBonusActionCommand(_character.DisplayName);
        _character.HasBonusAction = true;

        // Act
        var response = _mediator.Execute(command);

        // Assert
        response.Should().Be(MediatorCommandStatii.Canceled);
    }

    [Test]
    [Order(3)]
    public void Undo_Should_Not_Change_Anything_When_Bonus_Action_Was_Already_Available()
    {
        // Arrange
        var command = new AddBonusActionCommand(_character.DisplayName);
        _character.HasBonusAction = true;

        // Act
        _mediator.Execute(command);
        _mediator.Undo(command);

        // Assert
        _character.HasBonusAction.Should().Be(true);
    }
}