using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.NonAttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;
using BaseToolsLibrary.Mediator.CommandStatii;
using DnDToolsLibrary.Dice;

namespace CoreUnitTest.Commands.Attacks.Spells
{
    [TestFixture]
    public class CastNonAttackSpellCommandTest
    {
        private IMediator _mediator;
        private PlayableEntity _character;

        [OneTimeSetUp]
        public void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _character = FightersList.Instance[0];
        }

        [OneTimeTearDown]
        public void MainTearDown()
        {
            var statusProvider = DIContainer.GetImplementation<IStatusProvider>();
            statusProvider.Clear();
        }

        [SetUp]
        public void Setup()
        {
            PlayableEntity entity = EntitiesFactory.GetWarrior();
            entity.DisplayName = "Warrior1";
            FightersList.Instance.AddOrUpdateFighter(entity);
            entity = EntitiesFactory.GetWizard();
            entity.DisplayName = "Wizard1";
            FightersList.Instance.AddOrUpdateFighter(entity);
        }

        [Test]
        public void GenerateCorrectDamageInnerCommands()
        {
            CastNonAttackSpellCommand command = new CastNonAttackSpellCommand(_character.DisplayName, new Mock<Spell>().Object, 1, new List<string>() { "Warrior1", "Wizard1" });
            NonAttackSpellResults results = new NonAttackSpellResults();
            results.Add(new NewNonAttackSpellResult() { 
                HitDamage = new DamageResultList()
                {
                    new DamageResult("1d1+9", DamageTypeEnum.Fire),
                    new DamageResult("1d1+9", DamageTypeEnum.Cold),
                },
                TargetName = "Warrior1",
                CasterName = "Warrior1",
            });
            results.Add(new NewNonAttackSpellResult()
            {
                HitDamage = new DamageResultList()
                {
                    new DamageResult("1d1+9", DamageTypeEnum.Fire),
                    new DamageResult("1d1+9", DamageTypeEnum.Cold),
                },
                TargetName = "Wizard1",
                CasterName = "Warrior1",
            });
            results.ForEach(x => x.HitDamage.ToList().ForEach(x => x.Damage.Roll()));
            
            ValidableResponse<NonAttackSpellResults> response = new ValidableResponse<NonAttackSpellResults>(true, results);
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns(response);
            _mediator.RegisterHandler(mock.Object, typeof(NonAttackSpellResultsQuery));

            _mediator.Execute(command);

            Assert.AreEqual(2, command.InnerCommands.Count);
            ApplyDamageResultListCommand damageCommand = (ApplyDamageResultListCommand)command.PopLastInnerCommand();
            Assert.AreEqual(20, damageCommand.DamageList.Sum(x => x.RawAmount));
            Assert.AreEqual("Wizard1", damageCommand.GetEntity().DisplayName);
            damageCommand = (ApplyDamageResultListCommand)command.PopLastInnerCommand();
            Assert.AreEqual(20, damageCommand.DamageList.Sum(x => x.RawAmount));
            Assert.AreEqual("Warrior1", damageCommand.GetEntity().DisplayName);
        }

        [Test]
        public void GenerateCorrectInnerTryApplyStatusCommand_SavingSuccess()
        {
            CastNonAttackSpellCommand command = new CastNonAttackSpellCommand(_character.DisplayName, new Mock<Spell>().Object, 1, new List<string>() { "Warrior1", "Wizard1" });
            NonAttackSpellResults results = new NonAttackSpellResults();
            PlayableEntity warrior = FightersList.Instance.GetFighterByDisplayName("Warrior1");
            PlayableEntity wizard = FightersList.Instance.GetFighterByDisplayName("Wizard1");
            results.Add(new NewNonAttackSpellResult()
            {
                TargetName = "Warrior1",
                CasterName = "Wizard1",
                AppliedStatusList = new OnHitStatusList()
                {
                    StatusFactory.Slow
                },
                Saving = SavingThrowFactory.Successful(warrior),
            });
            results[0].AppliedStatusList[0].Caster = wizard;
            results[0].AppliedStatusList[0].Target = warrior;


            IMediatorCommandResponse response = new ValidableResponse<NonAttackSpellResults>(true, results);
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns(response);
            _mediator.RegisterHandler(mock.Object, typeof(NonAttackSpellResultsQuery));

            _mediator.Execute(command);

            TryApplyStatusCommand statusCommand = command.PopLastInnerCommand() as TryApplyStatusCommand;
            Assert.IsTrue(SavingThrowFactory.Successful(FightersList.Instance.GetFighterByDisplayName("Warrior1")).IsEquivalentTo(statusCommand.Saving));
            Assert.IsTrue(results[0].AppliedStatusList[0].IsEquivalentTo(statusCommand.Status));
        }

        [Test]
        public void GenerateCorrectInnerTryApplyStatusCommand_SavingFails()
        {
            CastNonAttackSpellCommand command = new CastNonAttackSpellCommand(_character.DisplayName, new Mock<Spell>().Object, 1, new List<string>() { "Warrior1", "Wizard1" });
            NonAttackSpellResults results = new NonAttackSpellResults();
            SavingThrow failingSaving = SavingThrowFactory.Failed(FightersList.Instance.GetFighterByDisplayName("Warrior1"));
            PlayableEntity warrior = FightersList.Instance.GetFighterByDisplayName("Warrior1");
            PlayableEntity wizard = FightersList.Instance.GetFighterByDisplayName("Wizard1");

            results.Add(new NewNonAttackSpellResult()
            {
                TargetName = "Warrior1",
                CasterName = "Wizard1",
                AppliedStatusList = new OnHitStatusList()
                {
                    StatusFactory.Slow
                },
                Saving = failingSaving,
            });
            results[0].AppliedStatusList[0].Caster = wizard;
            results[0].AppliedStatusList[0].Target = warrior;
            results[0].AppliedStatusList[0].ApplySavingDifficulty = failingSaving.Difficulty;
            results[0].AppliedStatusList[0].ApplySavingCharacteristic = failingSaving.Characteristic;



            ValidableResponse <NonAttackSpellResults> response = new ValidableResponse<NonAttackSpellResults>(true, results);
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns(response);
            _mediator.RegisterHandler(mock.Object, typeof(NonAttackSpellResultsQuery));

            _mediator.Execute(command);

            TryApplyStatusCommand statusCommand = command.PopLastInnerCommand() as TryApplyStatusCommand;
            Assert.IsTrue(SavingThrowFactory.Failed(warrior).IsEquivalentTo(statusCommand.Saving));
            Assert.IsTrue(results[0].AppliedStatusList[0].IsEquivalentTo(statusCommand.Status));
        }

        [Test]
        public void NonAttackSpellResultsQuery_Canceled()
        {
            CastNonAttackSpellCommand command = new CastNonAttackSpellCommand(_character.DisplayName, new Mock<Spell>().Object, 1, new List<string>() { "Warrior1", "Wizard1" });

            ValidableResponse<NonAttackSpellResults> response = new ValidableResponse<NonAttackSpellResults>(false, null);
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns(response);
            _mediator.RegisterHandler(mock.Object, typeof(NonAttackSpellResultsQuery));

            IMediatorCommandResponse commandResponse = _mediator.Execute(command);

            Assert.AreEqual(0, command.InnerCommands.Count);
            Assert.IsInstanceOf<MediatorCommandCanceled>(commandResponse);
        }
    }
}
