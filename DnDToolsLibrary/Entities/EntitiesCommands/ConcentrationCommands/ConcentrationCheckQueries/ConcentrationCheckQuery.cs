using BaseToolsLibrary.DependencyInjection;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using DnDToolsLibrary.Fight;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ConcentrationCheckQueries
{
    public class ConcentrationCheckQuery : SavingThrowQuery
    {
        private static Lazy<IFigtherProvider> fighterProvider = new Lazy<IFigtherProvider>(() => DIContainer.GetImplementation<IFigtherProvider>());

        public ConcentrationCheckQuery(string entityName) : base(null, "Concentration check")
        {
            PlayableEntity entity = fighterProvider.Value.GetFighterByDisplayName(entityName);

            base.SavingThrow = entity.GetSavingThrowTemplate(CharacteristicsEnum.Constitution, 10);
        }
    }
}
