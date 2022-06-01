using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.AttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using DnDToolsLibrary.Status.StatusCommands.TryApplyStatusCommands;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CoreUnitTest.Commands.Attacks.Spells
{
    [TestFixture]
    public class CastAttackSpellCommandTest
    {
        private IMediator _mediator;
        private IFightersProvider _fightersProvider;
        private PlayableEntity _character;

        [OneTimeSetUp]
        public void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _fightersProvider = DIContainer.GetImplementation<IFightersProvider>();
            _character = _fightersProvider.First();
        }

        [SetUp]
        public void Setup()
        {
            var entity = EntitiesFactory.GetWarrior();
            entity.DisplayName = "Warrior1";
            _fightersProvider.AddOrUpdateFighter(entity);
            entity = EntitiesFactory.GetWizard();
            entity.DisplayName = "Wizard1";
            _fightersProvider.AddOrUpdateFighter(entity);
        }

        [Test]
        public void GenerateCorrectDamageInnerCommands()
        {
            CastAttackSpellCommand command = new CastAttackSpellCommand(_character.DisplayName, new Mock<Spell>().Object, 1, new List<string>() {});
            AttackSpellResults results = new AttackSpellResults();
            results.Add(new AttackSpellResult() { 
                Target = _character,
                DamageList = new DamageResultList() {
                    // using D1 to remove random factor
                    new DamageResult("1d1+9", DamageTypeEnum.Fire),
                    new DamageResult("1d1+4", DamageTypeEnum.Cold),
                    new DamageResult("1d1+9", DamageTypeEnum.Poison),
                },
            });
            foreach (DamageResult dmg in results[0].DamageList)
                dmg.Damage.Roll();
            ValidableResponse<AttackSpellResults> response = new ValidableResponse<AttackSpellResults>(true, results);
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns((IMediatorCommandResponse)response);
            _mediator.RegisterHandler(mock.Object, typeof(AttackSpellResultsQuery));

            _mediator.Execute(command);

            Assert.AreEqual(1, command.InnerCommands.Count);
            Assert.AreEqual(25, ((ApplyDamageResultListCommand)command.PopLastInnerCommand()).DamageList.Sum(x => x.RawAmount));
        }

        [Test]
        public void GenerateCorrectTryApplyStatusCommand()
        {
            CastAttackSpellCommand command = new CastAttackSpellCommand(_character.DisplayName, new Mock<Spell>().Object, 1, new List<string>() { });
            AttackSpellResults results = new AttackSpellResults();
            Mock<IMediatorHandler> mockSaving = new Mock<IMediatorHandler>();
            OnHitStatus status = StatusFactory.InfernalWound;
            status.Caster = _character;
            status.Target = _character;

            results.Add(new AttackSpellResult()
            {
                Target = _character,
                DamageList = new DamageResultList() {
                },
                OnHitStatuses = new OnHitStatusList() { status },
                Caster = _character,
            });
            ValidableResponse<AttackSpellResults> response = new ValidableResponse<AttackSpellResults>(true, results);
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns((IMediatorCommandResponse)response);
            _mediator.RegisterHandler(mock.Object, typeof(AttackSpellResultsQuery));

            mockSaving.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns((IMediatorCommandResponse)new ValidableResponse<SavingThrow>(true, SavingThrowFactory.Successful(_character)));
            _mediator.RegisterHandler(mockSaving.Object, typeof(SavingThrowQuery));

            _mediator.Execute(command);

            Assert.AreEqual(2, command.InnerCommands.Count);
            Assert.IsTrue(status.IsEquivalentTo(((TryApplyStatusCommand)command.PopLastInnerCommand()).Status));
        }

        [Test]
        public void GenerateAttackResultsForCorrectCharacters()
        {
            CastAttackSpellCommand command = new CastAttackSpellCommand(_character.DisplayName, new Mock<Spell>().Object, 1, new List<string>() { "Warrior1", "Wizard1" });
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns<AttackSpellResultsQuery>(x => new ValidableResponse<AttackSpellResults>(true, ((AttackSpellResultsQuery)x).SpellResults));
            _mediator.RegisterHandler(mock.Object, typeof(AttackSpellResultsQuery));

            _mediator.Execute(command);

            Assert.AreEqual(2, command.InnerCommands.Count);
            Assert.AreEqual("Wizard1", ((ApplyDamageResultListCommand)command.PopLastInnerCommand()).GetEntity().DisplayName);
            Assert.AreEqual("Warrior1", ((ApplyDamageResultListCommand)command.PopLastInnerCommand()).GetEntity().DisplayName);
        }

        [Test]
        public void AttackResultQueryCanceled()
        {
            CastAttackSpellCommand command = new CastAttackSpellCommand(_character.DisplayName, new Mock<Spell>().Object, 1, new List<string>() { "Warrior1", "Wizard1" });
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns(new ValidableResponse<AttackSpellResults>(false, null));
            _mediator.RegisterHandler(mock.Object, typeof(AttackSpellResultsQuery));

            IMediatorCommandResponse response = _mediator.Execute(command);

            Assert.IsInstanceOf<MediatorCommandCanceled>(response);
        }
    }
}
