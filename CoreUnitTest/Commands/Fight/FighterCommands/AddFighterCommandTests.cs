using System.Security.RightsManagement;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Fight.FightCommands.FighterCommands.AddFighterCommands;
using DnDToolsLibrary.Fight.FightCommands.FighterCommands.RemoveFighterCommands;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace CoreUnitTest.Commands.Fight.FighterCommands
{
    [TestFixture]
    public class AddFighterCommandTests
    {
        private IFighterProvider _fighterProvider;
        private IMediator _mediator;

        private AddFighterCommand _command;
        private PlayableEntity _entity;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fighterProvider = DIContainer.GetImplementation<IFighterProvider>();
            _fighterProvider.Clear();
            _mediator = DIContainer.GetImplementation<IMediator>();
            _entity = EntitiesFactory.GetWarrior();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _fighterProvider.Clear();
        }

        [SetUp]
        public virtual void SetUp()
        {
            _fighterProvider.Clear();
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
                _fighterProvider.Should().HaveCount(1);
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
                // Act
                // Assert
            }

            [Test]
            public void Should_Return_Canceled_When_A_Fighter_Was_Already_Fighting()
            {
                // Arrange 
                // Act
                // Assert
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
                // Assert
            }

            [Test]
            public void Should_Give_Name_With_Zero_When_First_Instance()
            {
                // Arrange
                // Act
                // Assert
            }

            [Test]
            public void Should_Add_Monster_When_Second_Instance()
            {
                // Arrange
                // Act
                // Assert
            }

            [Test]
            public void Should_Give_Name_With_Increasing_Index_When_Second_Instance()
            {
                // Arrange
                // Act
                // Assert
            }

            [Test]
            public void Should_Fill_Missing_Index_When_More_Than_First_Instance()
            {
                // Arrange
                // Act
                // Assert
            }

            [Test]
            public void Should_Return_Success()
            {
                // Arrange
                // Act
                // Assert
            }

        }

        [TestFixture]
        public class UndoTests : AddFighterCommandTests
        {
            [Test]
            public void Should_Remove_Added_Fighter()
            {
                // Arrange 
                // Act
                // Assert
            }
        }
    }
}
