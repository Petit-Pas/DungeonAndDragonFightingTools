using System.Linq;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Fight.FightCommands.FighterCommands.AddFighterCommands;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.Fight.FighterCommands
{
    [TestFixture]
    public class AddFighterCommandTests
    {
        private IFightManager _fightManager;
        private IMediator _mediator;

        private AddFighterCommand _command;
        private PlayableEntity _entity;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fightManager = DIContainer.GetImplementation<IFightManager>();
            _fightManager.Clear();
            _mediator = DIContainer.GetImplementation<IMediator>();
            _entity = EntitiesFactory.GetWarrior();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _fightManager.Clear();
        }

        [SetUp]
        public virtual void SetUp()
        {
            _fightManager.Clear();
            _command = new AddFighterCommand(_entity);
        }

        [TestFixture]
        public class AddOtherTests : AddFighterCommandTests
        {
            public override void SetUp()
            {
                _entity = A.Fake<PlayableEntity>();
                base.SetUp();
            }

            [Test]
            public void Should_Return_Error_When_Trying_To_Add_Something_That_Is_Neither_Character_Nor_Monster()
            {
                // Arrange 
                // Act
                var result = _mediator.Execute(_command);

                // Assert
                result.Should().Be(MediatorCommandStatii.Error);
            }
        }

        [TestFixture]
        public class AddCharacterTests : AddFighterCommandTests
        {
            [SetUp]
            public override void SetUp()
            {
                _entity = EntitiesFactory.GetWarrior();
                base.SetUp();
            }

            [Test]
            public void Should_Add_Fighter()
            {
                // Arrange 
                // Act
                _mediator.Execute(_command);

                // Assert
                _fightManager.FighterCount.Should().Be(1);
            }

            [Test]
            public void Should_Return_Success_When_Everything_Went_Fine()
            {
                // Arrange 
                // Act
                var result = _mediator.Execute(_command);

                // Assert
                result.Should().Be(MediatorCommandStatii.Success);
            }


            [Test]
            public void Should_Not_Add_Fighter_When_Fighter_Is_Already_Fighting()
            {
                // Arrange 
                _mediator.Execute(_command);
                var previousCount = _fightManager.FighterCount;
                // Act

                _mediator.Execute(_command);
                // Assert
                _fightManager.FighterCount.Should().Be(previousCount);
            }

            [Test]
            public void Should_Return_Canceled_When_A_Fighter_Was_Already_Fighting()
            {
                // Arrange 
                _mediator.Execute(_command);
                // Act
                var result = _mediator.Execute(_command);
                // Assert
                result.Should().Be(MediatorCommandStatii.Canceled);
            }

        }

        [TestFixture]
        public class AddMonsterTests : AddFighterCommandTests
        {
            [SetUp]
            public override void SetUp()
            {
                _entity = EntitiesFactory.Goblin;
                base.SetUp();
            }

            [Test]
            public void Should_Add_Monster_When_First_Instance()
            {
                // Arrange
                // Act
                _mediator.Execute(_command);
                // Assert
                _fightManager.GetAllFighters().Should().Contain(x => x.Name == _entity.Name);
            }

            [Test]
            public void Should_Give_Name_With_Zero_When_First_Instance()
            {
                // Arrange
                // Act
                _mediator.Execute(_command);
                // Assert
                _fightManager.First().DisplayName.Should().EndWith("- 0");
            }

            [Test]
            public void Should_Add_Monster_When_Second_Instance()
            {
                // Arrange
                _mediator.Execute(_command);
                // Act
                _mediator.Execute(_command);
                // Assert
                _fightManager.FighterCount.Should().Be(2);
            }

            [Test]
            [Order(1)]
            public void Should_Give_Name_With_Increasing_Index_When_Second_Instance()
            {
                // Arrange
                _mediator.Execute(_command);
                // Act
                _mediator.Execute(_command);
                // Assert
                _fightManager.GetAllFighters().Should().Contain(x => x.DisplayName.EndsWith("- 1"));
            }

            [Test]
            [Order(2)]
            public void Should_Fill_Missing_Index_When_More_Than_First_Instance()
            {
                // Arrange
                _mediator.Execute(_command);
                _mediator.Execute(_command);
                _mediator.Execute(_command);
                _fightManager.RemoveFighter(_fightManager.GetAllFighters().First(x => x.DisplayName.Contains("1")));
                // Act
                _mediator.Execute(_command);
                // Assert
                _fightManager.GetAllFighters().Should().Contain(x => x.DisplayName.EndsWith("- 1"));
            }

            [Test]
            public void Should_Return_Success()
            {
                // Arrange
                // Act
                var result = _mediator.Execute(_command);
                // Assert
                result.Should().Be(MediatorCommandStatii.Success);
            }

        }

        [TestFixture]
        public class UndoTests : AddFighterCommandTests
        {
            [Test]
            public void Should_Remove_Added_Fighter()
            {
                // Arrange 
                _mediator.Execute(_command);
                // Act
                _mediator.Undo(_command);
                // Assert
                _fightManager.FighterCount.Should().Be(0);
            }
        }
    }
}
