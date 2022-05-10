using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.AcquireConcentration;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration;
using DnDToolsLibrary.Fight;
using NUnit.Framework;

namespace CoreUnitTest.Commands.PlayableEntities.Concentration
{
    [TestFixture]
    public class AcquireConcentrationCommandTest
    {
        private IMediator _mediator;
        private IFightManager _fightManager;
        private PlayableEntity _character;

        [OneTimeSetUp]
        public void MainSetup()
        {
            _fightManager = DIContainer.GetImplementation<IFightManager>();
            _mediator = DIContainer.GetImplementation<IMediator>();
            _character = _fightManager.First();
        }

        [SetUp]
        public void Setup()
        {
            var entity = EntitiesFactory.GetWarrior();
            entity.DisplayName = "Warrior1";
            _fightManager.AddOrUpdateFighter(entity);
            entity = EntitiesFactory.GetWizard();
            entity.DisplayName = "Wizard1";
            _fightManager.AddOrUpdateFighter(entity);
        }

        [Test]
        public void ConcentrationAcquired()
        {
            PlayableEntity entity = _fightManager.GetFighterByDisplayName("Warrior1");
            entity.IsFocused = false;
            AcquireConcentrationCommand command = new AcquireConcentrationCommand(entity.DisplayName);

            _mediator.Execute(command);

            Assert.IsTrue(entity.IsFocused);
            Assert.AreEqual(0, command.InnerCommands.Count);
        }

        [Test]
        public void ConcentrationAcquired_Undo()
        {
            PlayableEntity entity = _fightManager.GetFighterByDisplayName("Warrior1");
            entity.IsFocused = false;
            AcquireConcentrationCommand command = new AcquireConcentrationCommand(entity.DisplayName);

            _mediator.Execute(command);
            _mediator.Undo(command);

            Assert.IsFalse(entity.IsFocused);
            Assert.AreEqual(0, command.InnerCommands.Count);
        }

        [Test]
        public void ConcentrationReacquired()
        {
            PlayableEntity entity = _fightManager.GetFighterByDisplayName("Warrior1");
            entity.IsFocused = true;
            AcquireConcentrationCommand command = new AcquireConcentrationCommand(entity.DisplayName);

            _mediator.Execute(command);

            Assert.IsTrue(entity.IsFocused);
            Assert.AreEqual(1, command.InnerCommands.Count);
            Assert.AreEqual(typeof(LoseConcentrationCommand), command.InnerCommands.Peek().GetType());
        }

        [Test]
        public void ConcentrationReacquired_Undo()
        {
            PlayableEntity entity = _fightManager.GetFighterByDisplayName("Warrior1");
            entity.IsFocused = true;
            AcquireConcentrationCommand command = new AcquireConcentrationCommand(entity.DisplayName);

            _mediator.Execute(command);
            _mediator.Undo(command);

            Assert.IsTrue(entity.IsFocused);
            Assert.AreEqual(0, command.InnerCommands.Count);
        }
    }
}
