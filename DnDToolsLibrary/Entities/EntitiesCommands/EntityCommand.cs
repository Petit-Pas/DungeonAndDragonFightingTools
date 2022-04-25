using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands
{
    /// <summary>
    ///     Base class for basic commands related to PlayableEntity
    ///     As such commands are only supposed to be used during a fight, it will store only the name of the entity,
    ///         and lazy load it upon need from the FightersList
    /// </summary>
    public abstract class EntityCommand : IMediatorCommand
    {
        private static IFighterProvider fighterProvider = DIContainer.GetImplementation<IFighterProvider>();

        private readonly string _entityName;

        public EntityCommand(string entity_name)
        {
            _entityName = entity_name;
        }

        // this method should preffered to GetEntity since it does not call the FighterProvider
        public string GetEntityName()
        {
            return _entityName;
        }

        public PlayableEntity GetEntity()
        {
            PlayableEntity result = fighterProvider.GetFighterByDisplayName(_entityName);

            if (result == null)
            {
                Console.WriteLine($"ERROR : Trying to execute the command {this.GetType()} on {_entityName} which was no found among Fighters");
                throw new NullReferenceException($"Trying to execute the command {this.GetType()} on {_entityName} which was no found among Fighters");
            }

            return result;
        }
    }
}
