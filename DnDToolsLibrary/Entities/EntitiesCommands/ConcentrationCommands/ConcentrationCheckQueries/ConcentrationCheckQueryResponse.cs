using BaseToolsLibrary.Mediator;

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
