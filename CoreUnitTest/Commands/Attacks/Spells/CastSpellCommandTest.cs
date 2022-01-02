using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using CoreUnitTest.Extensions;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellLevelQueries;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellTargetsQueries;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CoreUnitTest.Commands.Attacks.Spells
{
    [TestFixture]
    public class CastSpellCommandTest
    {
        private PlayableEntity _character;

        private IMediator _mediator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
        }

        [SetUp]
        public void Setup()
        {
            PlayableEntity entity = EntitiesFactory.GetWarrior();
            entity.DisplayName = "Warrior1";
            _character = entity;
            FightersList.Instance.AddOrUpdateFighter(entity);
        }

        [Test]
        public void SelectLevel_NotCantrip()
        {
            Spell spell = SpellFactory.FireBall;
            CastSpellCommand command = new CastSpellCommand(_character, spell);
            _mediator.ConfigureCommandHandler<NormalSpellLevelQuery>(new ValidableResponse<SpellLevel>(true, new SpellLevel(spell.BaseLevel) { Value = 4 }));
            Mock<IMediatorHandler> targetQueryHandler = _mediator.ConfigureCommandHandler<SpellTargetQuery>(new ValidableResponse<SpellTargets>(false, new SpellTargets(new List<string> { _character.DisplayName })));

            _mediator.Execute(command);

            Assert.AreEqual(4, command.CastLevel);
            targetQueryHandler.Verify(mock => mock.Execute(It.IsAny<IMediatorCommand>()), Times.Once());
        }

        [Test]
        public void LevelNotSelected_NotCantrip()
        {
            Spell spell = SpellFactory.FireBall;
            CastSpellCommand command = new CastSpellCommand(_character, spell);
            _mediator.ConfigureCommandHandler<NormalSpellLevelQuery>(new ValidableResponse<SpellLevel>(false, new SpellLevel(spell.BaseLevel) { Value = 4 }));
            Mock<IMediatorHandler> targetQueryHandler = _mediator.ConfigureCommandHandler<SpellTargetQuery>(new ValidableResponse<SpellTargets>(false, new SpellTargets(new List<string> { _character.DisplayName })));

            ValidableResponse<MediatorCommandNoResponse> response = _mediator.Execute(command) as ValidableResponse<MediatorCommandNoResponse>;

            Assert.IsFalse(response.IsValid);
            targetQueryHandler.Verify(mock => mock.Execute(It.IsAny<IMediatorCommand>()), Times.Never());
        }


        [Test]
        public void SelectLevel_Cantrip()
        {
            Spell spell = SpellFactory.FireBolt;
            CastSpellCommand command = new CastSpellCommand(_character, spell);
            _mediator.ConfigureCommandHandler<CantripLevelQuery>(new ValidableResponse<SpellLevel>(true, new SpellLevel(spell.BaseLevel) { Value = 3 }));
            Mock<IMediatorHandler> targetQueryHandler = _mediator.ConfigureCommandHandler<SpellTargetQuery>(new ValidableResponse<SpellTargets>(false, new SpellTargets(new List<string>{ _character.DisplayName })));

            _mediator.Execute(command);

            Assert.AreEqual(3, command.CastLevel);
            targetQueryHandler.Verify(mock => mock.Execute(It.IsAny<IMediatorCommand>()), Times.Once());
        }

        [Test]
        public void LevelNotSelected_Cantrip()
        {
            Spell spell = SpellFactory.FireBolt;
            CastSpellCommand command = new CastSpellCommand(_character, spell);
            _mediator.ConfigureCommandHandler<CantripLevelQuery>(new ValidableResponse<SpellLevel>(false, new SpellLevel(spell.BaseLevel) { Value = 3 }));
            Mock<IMediatorHandler> targetQueryHandler = _mediator.ConfigureCommandHandler<SpellTargetQuery>(new ValidableResponse<SpellTargets>(false, new SpellTargets(new List<string> { _character.DisplayName })));

            ValidableResponse<MediatorCommandNoResponse> response = _mediator.Execute(command) as ValidableResponse<MediatorCommandNoResponse>;

            Assert.IsFalse(response.IsValid);
            targetQueryHandler.Verify(mock => mock.Execute(It.IsAny<IMediatorCommand>()), Times.Never());
        }


        [Test]
        public void SelectTarget()
        {
            Spell spell = SpellFactory.FireBall;
            CastSpellCommand command = new CastSpellCommand(_character, spell);
            _mediator.ConfigureCommandHandler<NormalSpellLevelQuery>(new ValidableResponse<SpellLevel>(true, new SpellLevel(spell.BaseLevel) { Value = 4 }));
            _mediator.ConfigureCommandHandler<SpellTargetQuery>(new ValidableResponse<SpellTargets>(true, new SpellTargets(new List<string> { "TestCharacter" })));
            Mock<IMediatorHandler> castNonAttackSpellCommandHandler = _mediator.ConfigureCommandHandler<CastNonAttackSpellCommand>(new ValidableResponse<MediatorCommandNoResponse>(true, MediatorCommandStatii.NoResponse));
            
            ValidableResponse<MediatorCommandNoResponse> response = _mediator.Execute(command) as ValidableResponse<MediatorCommandNoResponse>;

            Assert.AreEqual("TestCharacter", command.TargetNames[0]);
            Assert.IsTrue(response.IsValid);
        }

        [Test]
        public void TargetNotSelected()
        {
            Spell spell = SpellFactory.FireBall;
            CastSpellCommand command = new CastSpellCommand(_character, spell);
            _mediator.ConfigureCommandHandler<NormalSpellLevelQuery>(new ValidableResponse<SpellLevel>(true, new SpellLevel(spell.BaseLevel) { Value = 4 }));
            _mediator.ConfigureCommandHandler<SpellTargetQuery>(new ValidableResponse<SpellTargets>(false, new SpellTargets(new List<string> { "TestCharacter" })));
            Mock<IMediatorHandler> castNonAttackSpellCommandHandler = _mediator.ConfigureCommandHandler<CastNonAttackSpellCommand>(new ValidableResponse<MediatorCommandNoResponse>(true, MediatorCommandStatii.NoResponse));

            ValidableResponse<MediatorCommandNoResponse> response = _mediator.Execute(command) as ValidableResponse<MediatorCommandNoResponse>;

            Assert.AreEqual(0, command.TargetNames.Count);
            Assert.IsFalse(response.IsValid);
            castNonAttackSpellCommandHandler.Verify(mock => mock.Execute(It.IsAny<IMediatorCommand>()), Times.Never());
        }

        [Test]
        public void CastAttackSpell()
        {
            Spell spell = SpellFactory.FireBolt;
            CastSpellCommand command = new CastSpellCommand(_character, spell);
            _mediator.ConfigureCommandHandler<CantripLevelQuery>(new ValidableResponse<SpellLevel>(true, new SpellLevel(spell.BaseLevel) { Value = 1 }));
            _mediator.ConfigureCommandHandler<SpellTargetQuery>(new ValidableResponse<SpellTargets>(true, new SpellTargets(new List<string> { "TestCharacter" })));
            Mock<IMediatorHandler> castAttackSpellCommandHandler = _mediator.ConfigureCommandHandler<CastAttackSpellCommand>(new ValidableResponse<MediatorCommandNoResponse>(true, MediatorCommandStatii.NoResponse));

            ValidableResponse<MediatorCommandNoResponse> response = _mediator.Execute(command) as ValidableResponse<MediatorCommandNoResponse>;

            Assert.IsTrue(response.IsValid);
            castAttackSpellCommandHandler.Verify(mock => mock.Execute(It.IsAny<IMediatorCommand>()), Times.Once());
        }

        [Test]
        public void CastNonAttackSpell()
        {
            Spell spell = SpellFactory.FireBall;
            CastSpellCommand command = new CastSpellCommand(_character, spell);
            _mediator.ConfigureCommandHandler<NormalSpellLevelQuery>(new ValidableResponse<SpellLevel>(true, new SpellLevel(spell.BaseLevel) { Value = 4 }));
            _mediator.ConfigureCommandHandler<SpellTargetQuery>(new ValidableResponse<SpellTargets>(true, new SpellTargets(new List<string> { "TestCharacter" })));
            Mock<IMediatorHandler> castNonAttackSpellCommandHandler = _mediator.ConfigureCommandHandler<CastNonAttackSpellCommand>(new ValidableResponse<MediatorCommandNoResponse>(true, MediatorCommandStatii.NoResponse));

            ValidableResponse<MediatorCommandNoResponse> response = _mediator.Execute(command) as ValidableResponse<MediatorCommandNoResponse>;

            Assert.IsTrue(response.IsValid);
            castNonAttackSpellCommandHandler.Verify(mock => mock.Execute(It.IsAny<IMediatorCommand>()), Times.Once());
        }

        [Test]
        public void LevelUpSpell_ImpactTargets()
        {
            Spell spell = SpellFactory.Bless;
            CastSpellCommand command = new CastSpellCommand(_character, spell);
            int amountTargets = -1;
            _mediator.ConfigureCommandHandler<NormalSpellLevelQuery>(new ValidableResponse<SpellLevel>(true, new SpellLevel(spell.BaseLevel) { Value = 2 }));
            _mediator.ConfigureCommandHandler<SpellTargetQuery>((IMediatorCommand cmd) => { amountTargets = ((SpellTargetQuery)cmd).AmountTargets; }, new ValidableResponse<SpellTargets>(true, new SpellTargets(new List<string> { _character.DisplayName })));
            Mock<IMediatorHandler> castNonAttackSpellCommandHandler = _mediator.ConfigureCommandHandler<CastNonAttackSpellCommand>(new ValidableResponse<MediatorCommandNoResponse>(true, MediatorCommandStatii.NoResponse));

            ValidableResponse<MediatorCommandNoResponse> response = _mediator.Execute(command) as ValidableResponse<MediatorCommandNoResponse>;

            Assert.AreEqual(2, amountTargets);
        }
    }
}
