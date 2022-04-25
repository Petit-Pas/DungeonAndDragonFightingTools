using DnDToolsLibrary.Entities;
using System.Collections.Generic;

namespace DnDToolsLibrary.Fight
{
    public interface IFighterProvider : IList<PlayableEntity>
    {
        List<string> GetFightersNames();

        PlayableEntity GetFighterByDisplayName(string name);

        int CountWithName(string name);

        void AddFighter(PlayableEntity fighter);

        void AddOrUpdateFighter(PlayableEntity fighter);
    }
}
