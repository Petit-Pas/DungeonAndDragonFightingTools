using BaseToolsLibrary.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ConcentrationCheckQueries
{
    public class ConcentrationCheckQueryResponse : IMediatorCommandResponse
    {
        public ConcentrationCheckQueryResponse(string entityName, bool success)
        {
            EntityName = entityName;
            Success = success;
        }

        public string EntityName { get; set; }
        public bool Success { get; set; }
    }
}
