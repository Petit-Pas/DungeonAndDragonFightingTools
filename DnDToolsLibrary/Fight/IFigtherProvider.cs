using DnDToolsLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Fight
{
    public interface IFigtherProvider
    {
        List<string> GetFightersNames();

        PlayableEntity GetFighterByDisplayName(string name);

        void AddFighter(PlayableEntity fighter);
    }
}
