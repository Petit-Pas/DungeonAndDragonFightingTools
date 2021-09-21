using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Memory;
using DDFight;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using Moq;
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
            Global.Loading = false;

            AddRequiredDependencies();

            CreateFirstCharacter();
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
        }

        private void CreateFirstCharacter()
        {
            PlayableEntity character = new Character();
            character.MaxHp = 100;
            FightersList.Instance.Elements = new System.Collections.ObjectModel.ObservableCollection<PlayableEntity>();
            FightersList.Instance.AddElementSilent(character);
        }

        private void AddRequiredDependencies()
        {
            DIContainer.RegisterSingleton<ICustomConsole, ICustomConsole>(new Mock<ICustomConsole>().Object);
            DIContainer.RegisterSingleton<IFontWeightProvider, IFontWeightProvider>(new Mock<IFontWeightProvider>().Object);
            DIContainer.RegisterSingleton<IFontColorProvider, IFontColorProvider>(new Mock<IFontColorProvider>().Object);
            DIConfigurer.ConfigureCore(true);
        }
    }
}
