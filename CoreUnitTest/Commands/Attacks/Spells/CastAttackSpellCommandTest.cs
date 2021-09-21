using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.AttackSpellResultsQueries;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Fight;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CoreUnitTest.Commands.Attacks.Spells
{
    [TestFixture]
    public class CastAttackSpellCommandTest
    {
        private IMediator _mediator;
        private PlayableEntity _character;

        [OneTimeSetUp]
        public void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _character = FightersList.Instance.Elements[0];
        }

        [Test]
        public void GenerateCorrectDamageInnerCommands()
        {
            CastAttackSpellCommand command = new CastAttackSpellCommand(_character.DisplayName, new Mock<Spell>().Object, 1, new List<string>() {});
            AttackSpellResults results = new AttackSpellResults();
            results.Add(new NewAttackSpellResult() { 
                Target = _character,
                HitDamage = new DamageResultList()
                {
                    Elements = new ObservableCollection<DamageResult>() {
                        // using D1 to remove random factor
                        new DamageResult("1d1+9", DamageTypeEnum.Fire),
                        new DamageResult("1d1+4", DamageTypeEnum.Cold),
                        new DamageResult("1d1+9", DamageTypeEnum.Poison),
                    },
                },
            });
            foreach (DamageResult dmg in results[0].HitDamage.Elements)
                dmg.Damage.Roll();
            ValidableResponse<AttackSpellResults> test = new ValidableResponse<AttackSpellResults>(true, results);
            Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
            mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns((IMediatorCommandResponse)test);
            _mediator.RegisterHandler(mock.Object, typeof(AttackSpellResultsQuery));

            _mediator.Execute(command);

            Assert.AreEqual(1, command.InnerCommands.Count);
            Assert.AreEqual(25, ((ApplyDamageResultListCommand)command.PopLastInnerCommand()).DamageList.Sum(x => x.RawAmount));
        }
    }
}
