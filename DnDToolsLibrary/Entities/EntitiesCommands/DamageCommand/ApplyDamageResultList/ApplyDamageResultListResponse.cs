using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList
{
    public class ApplyDamageResultListResponse : IMediatorCommandResponse
    {
        public int Amount { get; set; } = 0;

        public ApplyDamageResultListResponse()
        {
        }

        public ApplyDamageResultListResponse(int amount)
        {
            Amount = amount;
        }
    }
}
