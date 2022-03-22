using DnDToolsLibrary.Entities;
using System.Collections.Generic;

namespace DnDToolsLibrary.Fight
{
    public interface IFigtherProvider : IList<PlayableEntity>
    {
        List<string> GetFightersNames();

        PlayableEntity GetFighterByDisplayName(string name);

        void AddFighter(PlayableEntity fighter);
    }
}
