using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight;
using System;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Entities.EntitiesCommands
{
    /// <summary>
    ///     Base class for basic commands related to PlayableEntity
    ///     As such commands are only supposed to be used during a fight, it will store only the name of the entity,
    ///         and lazy load it upon need from the FightersProvider
    /// </summary>
    public abstract class EntityCommand : DndCommandBase
    {
        private static IFightersProvider _fightersProvider = DIContainer.GetImplementation<IFightersProvider>();

        private readonly string _entityName;

        public EntityCommand(string entity_name)
        {
            _entityName = entity_name;
        }

        // this method should preffered to GetEntity since it does not call the FightersProvider
        public string GetEntityName()
        {
            return _entityName;
        }

        public PlayableEntity GetEntity()
        {
            PlayableEntity result = _fightersProvider.GetFighterByDisplayName(_entityName);

            if (result == null)
            {
                Console.WriteLine($"ERROR : Trying to execute the command {this.GetType()} on {_entityName} which was no found among Fighters");
                throw new NullReferenceException($"Trying to execute the command {this.GetType()} on {_entityName} which was no found among Fighters");
            }

            return result;
        }
    }
}
