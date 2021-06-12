using DDFight;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreUnitTest
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            DIConfigurer.ConfigureCore();

            PlayableEntity character = new Character();
            character.MaxHp = 100;
            FightersList.Instance.Elements = new System.Collections.ObjectModel.ObservableCollection<PlayableEntity>();
            FightersList.Instance.AddElementSilent(character);
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
        }
    }
}
