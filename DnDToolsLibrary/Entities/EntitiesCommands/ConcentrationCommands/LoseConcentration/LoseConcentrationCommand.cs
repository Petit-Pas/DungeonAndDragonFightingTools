using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.LoseConcentration
{
    public class LoseConcentrationCommand : EntitySuperCommand
    {
        public LoseConcentrationCommand(string entity_name) : base(entity_name)
        {
        }

        public bool WasFocused { get; set; } = false;
    }
}
