using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;
using Moq;
using NUnit.Framework;

namespace CoreUnitTest.Commands.Status
{
    [TestFixture]
    public class TryApplyStatusCommandTest
    {

        private IMediator _mediator;
        private IFightManager _fightManager;
        private IStatusProvider _statusProvider;

        [OneTimeSetUp]
        public void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _fightManager = DIContainer.GetImplementation<IFightManager>(); 
            _statusProvider = DIContainer.GetImplementation<IStatusProvider>();
        }

        [OneTimeTearDown]
        public void MainTearDown()
        {
            _statusProvider.Clear();
        }

        [SetUp]
        public void Setup()
        {
            var entity = EntitiesFactory.GetWarrior();
            entity.DisplayName = "Warrior1";
            _fightManager.AddOrUpdateFighter(entity);
            entity = EntitiesFactory.GetWarrior();
            entity.DisplayName = "Warrior2";
            _fightManager.AddOrUpdateFighter(entity);
            _statusProvider.Clear();
        }

        // Used in Status applied by a spell that already has a saving throw, then the saving throw of the spell is usually used for the status itself
        #region StatusProvided

        [Test]
        public void Applied()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.Slow, SavingThrowFactory.Failed(_fightManager.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(1, _fightManager.GetFighterByDisplayName("Warrior2").AffectingStatusList.Count);
            Assert.AreEqual(1, _statusProvider.Count);
        }

        [Test]
        public void CanceledBySaving()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.Slow, SavingThrowFactory.Successful(_fightManager.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(0, _fightManager.GetFighterByDisplayName("Warrior2").AffectingStatusList.Count);
            Assert.AreEqual(0, _statusProvider.Count);
        }

        [Test]
        public void StatusMaintainedWhenSavingSucceeds()
        {
            var status = StatusFactory.Slow;
            status.SpellApplicationModifier = ApplicationModifierEnum.Maintained;
            var command = new TryApplyStatusCommand("Warrior1", "Warrior2", status, SavingThrowFactory.Successful(_fightManager.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(1, _fightManager.GetFighterByDisplayName("Warrior2").AffectingStatusList.Count);
            Assert.AreEqual(1, _statusProvider.Count);
        }

        [Test]
        public void OnApplyDamageSavingFailed()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.ImmediateDamageNormal, SavingThrowFactory.Failed(_fightManager.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(40, _fightManager.GetFighterByDisplayName("Warrior2").Hp);
        }

        [Test]
        public void OnApplyDamageNormal()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.ImmediateDamageNormal , SavingThrowFactory.Successful(_fightManager.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(40, _fightManager.GetFighterByDisplayName("Warrior2").Hp);
        }

        [Test]
        public void OnApplyDamageHalved()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.ImmediateDamageHalved, SavingThrowFactory.Successful(_fightManager.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(45, _fightManager.GetFighterByDisplayName("Warrior2").Hp);
        }

        [Test]
        public void OnApplyDamageCanceled()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.ImmediateDamageCanceled, SavingThrowFactory.Successful(_fightManager.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(50, _fightManager.GetFighterByDisplayName("Warrior2").Hp);
        }

        [Test]
        public void SavingThrowPropagated()
        {
            var slow = StatusFactory.Slow;
            var saving = SavingThrowFactory.Failed(_fightManager.GetFighterByDisplayName("Warrior2"));
            var command = new TryApplyStatusCommand("Warrior1", "Warrior2", slow, saving);
            var affected = _fightManager.GetFighterByDisplayName("Warrior2");

            _mediator.Execute(command);
            var status = _statusProvider[0] as OnHitStatus;
            
            Assert.AreEqual(saving.Difficulty, status.ApplySavingDifficulty);
            Assert.AreEqual(saving.Characteristic, status.ApplySavingCharacteristic);
        }

        #endregion StatusProvided

        // Used when the status has its own Saving throw (or none) that are not linked to the attack/spell that applies it
        #region StatusNotProvided

        [Test]
        public void NoSavingNeeded()
        {
            OnHitStatus automatic = StatusFactory.AutomaticApplication;
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", automatic);
            PlayableEntity affected = _fightManager.GetFighterByDisplayName("Warrior2");

            _mediator.Execute(command);

            Assert.AreEqual(1, _statusProvider.Count);
            Assert.AreEqual(1, affected.AffectingStatusList.Count);
        }

        [Test]
        public void SavingFail()
        {
            PlayableEntity affected = _fightManager.GetFighterByDisplayName("Warrior2");
            ValidableResponse<SavingThrow> response = new ValidableResponse<SavingThrow>(true, SavingThrowFactory.Failed(affected));
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns((IMediatorCommandResponse)response);
            _mediator.RegisterHandler(mock.Object, typeof(SavingThrowQuery));
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.InfernalWound);

            _mediator.Execute(command);

            mock.Verify(x => x.Execute(It.IsAny<IMediatorCommand>()), Times.Once());
            Assert.AreEqual(1, _statusProvider.Count);
            Assert.AreEqual(1, affected.AffectingStatusList.Count);
        }

        [Test]
        public void SavingSuccess()
        {
            PlayableEntity affected = _fightManager.GetFighterByDisplayName("Warrior2");
            ValidableResponse<SavingThrow> response = new ValidableResponse<SavingThrow>(true, SavingThrowFactory.Successful(affected));
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns((IMediatorCommandResponse)response);
            _mediator.RegisterHandler(mock.Object, typeof(SavingThrowQuery));
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.InfernalWound);

            _mediator.Execute(command);

            mock.Verify(x => x.Execute(It.IsAny<IMediatorCommand>()), Times.Once());
            Assert.AreEqual(0, _statusProvider.Count);
            Assert.AreEqual(0, affected.AffectingStatusList.Count);
        }

        [Test]
        public void NoSavingNeeded_DamageUnImpacted()
        {
            OnHitStatus automatic = StatusFactory.AutomaticApplication;
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", automatic);
            command.Status.OnApplyDamageList.AddElementSilent(new DamageTemplate("10", DamageTypeEnum.Force));
            command.Status.OnApplyDamageList.AddElementSilent(new DamageTemplate("10", DamageTypeEnum.Force));
            command.Status.OnApplyDamageList[0].SituationalDamageModifier = DamageModifierEnum.Halved;
            command.Status.OnApplyDamageList[1].SituationalDamageModifier = DamageModifierEnum.Normal;
            PlayableEntity affected = _fightManager.GetFighterByDisplayName("Warrior2");
            affected.Hp = 50;

            _mediator.Execute(command);

            Assert.AreEqual(30, affected.Hp);
        }

        [Test]
        public void SavingCanceled()
        {
            var affected = _fightManager.GetFighterByDisplayName("Warrior2");
            var response = new ValidableResponse<SavingThrow>(false, SavingThrowFactory.Failed(affected));
            var mock = new Mock<IMediatorHandler>();
            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns((IMediatorCommandResponse)response);
            _mediator.RegisterHandler(mock.Object, typeof(SavingThrowQuery));
            var command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.InfernalWound);

            _mediator.Execute(command);

            mock.Verify(x => x.Execute(It.IsAny<IMediatorCommand>()), Times.Once());
            Assert.AreEqual(0, _statusProvider.Count);
            Assert.AreEqual(0, affected.AffectingStatusList.Count);
        }

        #endregion StatusNotProvided
    }
}
