﻿using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight;
using System;
using BaseToolsLibrary.DependencyInjection;

namespace DnDToolsLibrary.Entities.EntitiesCommands
{
    /// <summary>
    ///     Base class for super commands related to PlayableEntity
    ///     As such commands are only supposed to be used during a fight, it will store only the name of the entity,
    ///         and lazy load it upon need from the FightersManager
    /// </summary>
    public abstract class EntitySuperCommand : SuperCommandBase
    {
        private static readonly Lazy<IFightManager> _lazyFighterProvider = new(DIContainer.GetImplementation<IFightManager>);
        protected static IFightManager FightManager => _lazyFighterProvider.Value;


        private readonly string _entityName;

        public EntitySuperCommand(string entityName)
        {
            _entityName = entityName;
        }

        // this method should preffered to GetEntity since it does not call the FighterProvider
        public string GetEntityName()
        {
            return _entityName;
        }

        public PlayableEntity GetEntity()
        {
            var result = FightManager.GetFighterByDisplayName(_entityName);

            if (result == null)
            {
                Console.WriteLine($"ERROR : Trying to execute the command {this.GetType()} on {_entityName} which was no found among Fighters");
                throw new NullReferenceException($"Trying to execute the command {this.GetType()} on {_entityName} which was no found among Fighters");
            }

            return result;
        }
    }
}
