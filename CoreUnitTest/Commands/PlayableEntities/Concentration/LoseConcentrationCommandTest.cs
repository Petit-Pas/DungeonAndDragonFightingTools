using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using CoreUnitTest.TestFactories;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreUnitTest.Commands.PlayableEntities.Concentration
{
    [TestFixture]
    public class LoseConcentrationCommandTest
    {
        private IMediator _mediator;
        private PlayableEntity _character;

        [OneTimeSetUp]
        public void MainSetup()
        {
            _mediator = DIContainer.GetImplementation<IMediator>();
            _character = FightersList.Instance[0];
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
        public void NoConcentrationToLose()
        {

        }
    }
}