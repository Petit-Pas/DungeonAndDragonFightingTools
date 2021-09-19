using System.Xml.Serialization;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseHp
{
    public class LooseHpCommand : EntityCommand
    {
        [XmlAttribute]
        public int Amount { get; set; }
        [XmlAttribute]
        public int? From { get; set; }
        [XmlAttribute]
        public int? To { get; set; }

        public LooseHpCommand(PlayableEntity entity, int amount) : base(entity.DisplayName)
        {
            Amount = amount;
        }
    }
}
