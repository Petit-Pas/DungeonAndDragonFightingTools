using BaseToolsLibrary.DependencyInjection;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using DnDToolsLibrary.Fight;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ConcentrationCheckQueries
{
    public class ConcentrationCheckQuery : SavingThrowQuery
    {
        private static Lazy<IFightersProvider> fighterProvider = new(() => DIContainer.GetImplementation<IFightersProvider>());

        public ConcentrationCheckQuery(string entityName) : base(null, "Concentration check")
        {
            PlayableEntity entity = fighterProvider.Value.GetFighterByDisplayName(entityName);

            base.SavingThrow = entity.GetSavingThrowTemplate(CharacteristicsEnum.Constitution, 10);
            base.SavingThrow.TargetName = entityName;
        }
    }
}
