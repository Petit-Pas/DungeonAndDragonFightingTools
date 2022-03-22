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
        private IStatusProvider _statusProvider;

        [OneTimeSetUp]
        public void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
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
            PlayableEntity entity = EntitiesFactory.GetWarrior();
            entity.DisplayName = "Warrior1";
            FightersList.Instance.AddOrUpdateFighter(entity);
            entity = EntitiesFactory.GetWarrior();
            entity.DisplayName = "Warrior2";
            FightersList.Instance.AddOrUpdateFighter(entity);
            _statusProvider.Clear();
        }

        // Used in Status applied by a spell that already has a saving throw, then the saving throw of the spell is usually used for the status itself
        #region StatusProvided

        [Test]
        public void Applied()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.Slow, SavingThrowFactory.Failed(FightersList.Instance.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(1, FightersList.Instance.GetFighterByDisplayName("Warrior2").AffectingStatusList.Count);
            Assert.AreEqual(1, _statusProvider.Count);
        }

        [Test]
        public void CanceledBySaving()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.Slow, SavingThrowFactory.Successful(FightersList.Instance.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(0, FightersList.Instance.GetFighterByDisplayName("Warrior2").AffectingStatusList.Count);
            Assert.AreEqual(0, _statusProvider.Count);
        }

        [Test]
        public void StatusMaintainedWhenSavingSucceeds()
        {
            OnHitStatus status = StatusFactory.Slow;
            status.SpellApplicationModifier = ApplicationModifierEnum.Maintained;
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", status, SavingThrowFactory.Successful(FightersList.Instance.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(1, FightersList.Instance.GetFighterByDisplayName("Warrior2").AffectingStatusList.Count);
            Assert.AreEqual(1, _statusProvider.Count);
        }

        [Test]
        public void OnApplyDamageSavingFailed()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.ImmediateDamageNormal, SavingThrowFactory.Failed(FightersList.Instance.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(40, FightersList.Instance.GetFighterByDisplayName("Warrior2").Hp);
        }

        [Test]
        public void OnApplyDamageNormal()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.ImmediateDamageNormal , SavingThrowFactory.Successful(FightersList.Instance.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(40, FightersList.Instance.GetFighterByDisplayName("Warrior2").Hp);
        }

        [Test]
        public void OnApplyDamageHalved()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.ImmediateDamageHalved, SavingThrowFactory.Successful(FightersList.Instance.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(45, FightersList.Instance.GetFighterByDisplayName("Warrior2").Hp);
        }

        [Test]
        public void OnApplyDamageCanceled()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.ImmediateDamageCanceled, SavingThrowFactory.Successful(FightersList.Instance.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(50, FightersList.Instance.GetFighterByDisplayName("Warrior2").Hp);
        }

        [Test]
        public void SavingThrowPropagated()
        {
            OnHitStatus slow = StatusFactory.Slow;
            SavingThrow saving = SavingThrowFactory.Failed(FightersList.Instance.GetFighterByDisplayName("Warrior2"));
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", slow, saving);
            PlayableEntity affected = FightersList.Instance.GetFighterByDisplayName("Warrior2");

            _mediator.Execute(command);
            OnHitStatus status = _statusProvider[0] as OnHitStatus;
            
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
            PlayableEntity affected = FightersList.Instance.GetFighterByDisplayName("Warrior2");

            _mediator.Execute(command);

            Assert.AreEqual(1, _statusProvider.Count);
            Assert.AreEqual(1, affected.AffectingStatusList.Count);
        }

        [Test]
        public void SavingFail()
        {
            PlayableEntity affected = FightersList.Instance.GetFighterByDisplayName("Warrior2");
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
            PlayableEntity affected = FightersList.Instance.GetFighterByDisplayName("Warrior2");
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
            PlayableEntity affected = FightersList.Instance.GetFighterByDisplayName("Warrior2");
            affected.Hp = 50;

            _mediator.Execute(command);

            Assert.AreEqual(30, affected.Hp);
        }

        [Test]
        public void SavingCanceled()
        {
            PlayableEntity affected = FightersList.Instance.GetFighterByDisplayName("Warrior2");
            ValidableResponse<SavingThrow> response = new ValidableResponse<SavingThrow>(false, SavingThrowFactory.Failed(affected));
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns((IMediatorCommandResponse)response);
            _mediator.RegisterHandler(mock.Object, typeof(SavingThrowQuery));
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.InfernalWound);

            _mediator.Execute(command);

            mock.Verify(x => x.Execute(It.IsAny<IMediatorCommand>()), Times.Once());
            Assert.AreEqual(0, _statusProvider.Count);
            Assert.AreEqual(0, affected.AffectingStatusList.Count);
        }

        #endregion StatusNotProvided
    }
}
