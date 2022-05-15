using DnDToolsLibrary.Characteristics;

namespace DnDToolsLibrary.Entities
{
    public static class PlayableEntityExtensions
    {

        /// <summary>
        /// if the entity is named "Gobelin - 3", returns 3
        /// </summary>
        public static int GetNameNumber(this PlayableEntity entity)
        {
            return int.Parse(entity.DisplayName[(entity.Name.Length + 2)..]);
        }

        public static int GetInitiative(this PlayableEntity entity)
        {
            return (int)entity.InitiativeRoll + entity.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity);
        }

        public static int GetStrModifier(this PlayableEntity entity)
        {
            return entity.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Strength);
        }
        public static int GetDexModifier(this PlayableEntity entity)
        {
            return entity.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity);
        }
        public static int GetConModifier(this PlayableEntity entity)
        {
            return entity.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Constitution);
        }
        public static int GetIntModifier(this PlayableEntity entity)
        {
            return entity.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Intelligence);
        }
        public static int GetWisModifier(this PlayableEntity entity)
        {
            return entity.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Wisdom);
        }
        public static int GetChaModifier(this PlayableEntity entity)
        {
            return entity.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Charisma);
        }
    }
}
