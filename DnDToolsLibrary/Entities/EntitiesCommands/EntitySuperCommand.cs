using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight;
using System;
using System.Linq;

namespace DnDToolsLibrary.Entities.EntitiesCommands
{
    /// <summary>
    ///     Base class for super commands related to PlayableEntity
    ///     As such commands are only supposed to be used during a fight, it will store only the name of the entity,
    ///         and lazy load it upon need from the FightersList
    /// </summary>
    public abstract class EntitySuperCommand : BaseSuperCommand
    {
        private readonly string _entityName;

        public EntitySuperCommand(string entity_name)
        {
            _entityName = entity_name;
        }

        public PlayableEntity GetEntity()
        {
            PlayableEntity result = FightersList.Instance.FirstOrDefault(x => x.DisplayName == _entityName);

            if (result == null)
            {
                Console.WriteLine($"ERROR : Trying to execute the command {this.GetType()} on {_entityName} which was no found among Fighters");
                throw new NullReferenceException($"Trying to execute the command {this.GetType()} on {_entityName} which was no found among Fighters");
            }

            return result;
        }
    }
}
