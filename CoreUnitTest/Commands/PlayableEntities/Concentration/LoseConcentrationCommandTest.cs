using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.AddStatus;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.RemoveStatus;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreUnitTest.Commands.PlayableEntities.Concentration
{
    [TestFixture]
    public class LoseConcentrationCommandTest
    {
        private IMediator _mediator;
        private PlayableEntity _character;

        [OneTimeSetUp]
        public virtual void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _character = FightersList.Instance[0];
            _character.IsFocused = false;
        }

        [TestFixture]
        public class NoConcentration : LoseConcentrationCommandTest
        {
            [Test]
            public void CommandCanceled()
            {
                // arrange
                LoseConcentrationCommand command = new LoseConcentrationCommand(_character.DisplayName);

                // act
                IMediatorCommandResponse response = _mediator.Execute(command);

                // assert
                Assert.AreEqual(typeof(MediatorCommandCanceled), response.GetType());
            }
        }

        [TestFixture]
        public class Execute : LoseConcentrationCommandTest
        {
            // ends on focus lost
            OnHitStatus _slow;
            // does not end on focus lost
            OnHitStatus _infernalWound;
            Character _affected;

            LoseConcentrationCommand _command;

            [OneTimeSetUp]
            public override void MainSetup()
            {
                base.MainSetup();

                // arrange
                _affected = EntitiesFactory.GetWizard() as Character;
                FightersList.Instance.AddOrUpdateFighter(_affected);
                _character.IsFocused = true;
                _slow = StatusFactory.Slow;
                _infernalWound = StatusFactory.InfernalWound;
                _slow.Caster = _character;
                _infernalWound.Caster = _character;
                _mediator.Execute(new AddStatusCommand(_affected, _slow));
                _mediator.Execute(new AddStatusCommand(_affected, _infernalWound));
                _command = new LoseConcentrationCommand(_character.DisplayName);

                // act
                _mediator.Execute(_command);
            }

            [Test]
            public void ConcentrationRemoved()
            {
                // assert
                Assert.IsFalse(_character.IsFocused);
            }

            [Test]
            public void StatusWithConcentrationRemoved()
            {
                // assert
                Assert.AreEqual(_slow.Name, ((RemoveStatusCommand)_command.InnerCommands.Peek()).Status.Name);
            }

            [Test]
            public void StatusWithNoConcentrationRemained()
            {
                // assert
                Assert.AreEqual(1, _command.InnerCommands.Count); // would be 2 if the other status had been removed
            }
        }

        [TestFixture]
        // tests should be working in LoseConcentrationCommandTest_Execute before looking at these
        public class Undo : LoseConcentrationCommandTest
        {
            // ends on focus lost
            OnHitStatus _slow;
            // does not end on focus lost
            OnHitStatus _infernalWound;
            Character _affected;

            LoseConcentrationCommand _command;

            [OneTimeSetUp]
            public override void MainSetup()
            {
                base.MainSetup();

                // arrange
                _affected = EntitiesFactory.GetWizard() as Character;
                FightersList.Instance.AddOrUpdateFighter(_affected);
                _character.IsFocused = true;
                _slow = StatusFactory.Slow;
                _infernalWound = StatusFactory.InfernalWound;
                _slow.Caster = _character;
                _infernalWound.Caster = _character;
                _mediator.Execute(new AddStatusCommand(_affected, _slow));
                _mediator.Execute(new AddStatusCommand(_affected, _infernalWound));
                _command = new LoseConcentrationCommand(_character.DisplayName);

                // act
                _mediator.Execute(_command);
                _mediator.Undo(_command);
            }

            [Test]
            public void ConcentrationAddedBack()
            {
                // assert
                Assert.IsTrue(_character.IsFocused);
            }
        }
    }
}