using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Memory;
using DDFight;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Memory;
using FakeItEasy;
using Moq;
using NUnit.Framework;

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
            
            //CreateDefaultQueryHandlers();
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
        }

        private void CreateFirstCharacter()
        {
            PlayableEntity character = new Character();
            character.MaxHp = 100;
            ((GenericList<PlayableEntity>)DIContainer.GetImplementation<IFightersProvider>()).AddElementSilent(character);
        }

        private void AddRequiredDependencies()
        {
            DIContainer.RegisterSingleton<ICustomConsole, ICustomConsole>(new Mock<ICustomConsole>().Object);
            DIContainer.RegisterSingleton<IFontWeightProvider, IFontWeightProvider>(new Mock<IFontWeightProvider>().Object);
            DIContainer.RegisterSingleton<IFontColorProvider, IFontColorProvider>(new Mock<IFontColorProvider>().Object);

            DIConfigurer.ConfigureCore(true);
        }

        private void CreateDefaultQueryHandlers()
        {
            IMediator mediator = DIContainer.GetImplementation<IMediator>();

            // SavingThrowQuery
            var savingHandler = A.Fake<IMediatorHandler>();
            A.CallTo(() => savingHandler.Execute(A<IMediatorCommand>._)).Returns(A.Fake<SavingThrow>());
            mediator.RegisterHandler(savingHandler, typeof(SavingThrowQuery));

        }

    }
}
