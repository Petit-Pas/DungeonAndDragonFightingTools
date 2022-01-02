using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Mediator.CommandStatii;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ChallengeConcentration;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ConcentrationCheckQueries;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration;
using DnDToolsLibrary.Fight;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreUnitTest.Commands.PlayableEntities.Concentration
{
    [TestFixture]
    public class ChallengeConcentrationCommandTest
    {
        private IMediator _mediator;
        private PlayableEntity _character;

        [OneTimeSetUp]
        public virtual void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _character = FightersList.Instance[0];
            _character.IsFocused = true;
        }

        [TestFixture]
        public class EntityNotFocused : ChallengeConcentrationCommandTest
        {
            IMediatorCommandResponse _response;

            [OneTimeSetUp]
            public override void MainSetup()
            {
                base.MainSetup();
                // arrange
                ChallengeConcentrationCommand command = new ChallengeConcentrationCommand(_character.Name);
                _character.IsFocused = false;

                _response = _mediator.Execute(command);
            }

            [Test]
            public void ConcentrationRemainsTheSame()
            {
                Assert.IsFalse(_character.IsFocused);
            }

            [Test]
            public void ReturnsCancel()
            {
                Assert.AreEqual(typeof(MediatorCommandCanceled), _response.GetType());
            }
        }

        [TestFixture]
        public class QueryCanceled : ChallengeConcentrationCommandTest
        {
            IMediatorCommandResponse _response;

            [OneTimeSetUp]
            public override void MainSetup()
            {
                base.MainSetup();
                // arrange
                ChallengeConcentrationCommand command = new ChallengeConcentrationCommand(_character.Name);
                IMediatorCommandResponse response = new ValidableResponse<SavingThrow>(false, null);
                Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
                mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns(response);
                _mediator.RegisterHandler(mock.Object, typeof(ConcentrationCheckQuery));

                _response = _mediator.Execute(command);
            }

            [Test]
            public void ConcentrationRemain()
            {
                Assert.IsTrue(_character.IsFocused);
            }

            [Test]
            public void ReturnsCancel()
            {
                Assert.AreEqual(typeof(MediatorCommandCanceled), _response.GetType());
            }
        }

        [TestFixture]
        public class CheckSuccess : ChallengeConcentrationCommandTest
        {
            IMediatorCommandResponse _response;

            [OneTimeSetUp]
            public override void MainSetup()
            {
                base.MainSetup();
                // arrange
                ChallengeConcentrationCommand command = new ChallengeConcentrationCommand(_character.Name);
                IMediatorCommandResponse response = new ValidableResponse<SavingThrow>(true, SavingThrowFactory.Successful(_character));
                Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
                mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns(response);
                _mediator.RegisterHandler(mock.Object, typeof(ConcentrationCheckQuery));

                _response = _mediator.Execute(command);
            }

            [Test]
            public void ConcentrationRemain()
            {
                Assert.IsTrue(_character.IsFocused);
            }

            [Test]
            public void ReturnsSuccess()
            {
                Assert.AreEqual(typeof(MediatorCommandSuccess), _response.GetType());
            }
        }

        [TestFixture]
        public class CheckFails : ChallengeConcentrationCommandTest
        {
            IMediatorCommandResponse _response;
            ChallengeConcentrationCommand _command;

            [OneTimeSetUp]
            public override void MainSetup()
            {
                base.MainSetup();
                // arrange
                _command = new ChallengeConcentrationCommand(_character.Name);
                IMediatorCommandResponse response = new ValidableResponse<SavingThrow>(true, SavingThrowFactory.Failed(_character));
                Mock<IMediatorHandler> mock = new Mock<IMediatorHandler>();
                mock.Setup(x => x.Execute(It.IsAny<IMediatorCommand>())).Returns(response);
                _mediator.RegisterHandler(mock.Object, typeof(ConcentrationCheckQuery));

                // act
                _response = _mediator.Execute(_command);
            }

            [Test]
            public void LoseConcentrationCommandUsed()
            {
                LoseConcentrationCommand command = _command.InnerCommands.Peek() as LoseConcentrationCommand;

                Assert.IsNotNull(command);
            }

            [Test]
            public void ReturnsSuccess()
            {
                Assert.AreEqual(typeof(MediatorCommandSuccess), _response.GetType());
            }
        }

    }
}
