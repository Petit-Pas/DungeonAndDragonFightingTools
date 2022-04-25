using BaseToolsLibrary.DependencyInjection;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using DnDToolsLibrary.Fight;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ConcentrationCheckQueries
{
    public class ConcentrationCheckQuery : SavingThrowQuery
    {
        private static Lazy<IFighterProvider> fighterProvider = new(() => DIContainer.GetImplementation<IFighterProvider>());

        public ConcentrationCheckQuery(string entityName) : base(null, "Concentration check")
        {
            PlayableEntity entity = fighterProvider.Value.GetFighterByDisplayName(entityName);

            base.SavingThrow = entity.GetSavingThrowTemplate(CharacteristicsEnum.Constitution, 10);
            base.SavingThrow.TargetName = entityName;
        }
    }
}
