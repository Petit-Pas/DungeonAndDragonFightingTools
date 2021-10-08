using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
        public void OnHitDamageSavingFailed()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.ImmediateDamageNormal, SavingThrowFactory.Failed(FightersList.Instance.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(40, FightersList.Instance.GetFighterByDisplayName("Warrior2").Hp);
        }

        [Test]
        public void OnHitDamageNormal()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.ImmediateDamageNormal , SavingThrowFactory.Successful(FightersList.Instance.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(40, FightersList.Instance.GetFighterByDisplayName("Warrior2").Hp);
        }

        [Test]
        public void OnHitDamageHalved()
        {
            TryApplyStatusCommand command = new TryApplyStatusCommand("Warrior1", "Warrior2", StatusFactory.ImmediateDamageHalved, SavingThrowFactory.Successful(FightersList.Instance.GetFighterByDisplayName("Warrior2")));

            _mediator.Execute(command);

            Assert.AreEqual(45, FightersList.Instance.GetFighterByDisplayName("Warrior2").Hp);
        }

        [Test]
        public void OnHitDamageCanceled()
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
    }
}
