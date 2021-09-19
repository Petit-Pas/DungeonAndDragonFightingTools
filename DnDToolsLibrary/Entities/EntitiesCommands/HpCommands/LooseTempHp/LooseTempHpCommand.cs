using System.Xml.Serialization;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.LooseTempHp
{
    public class LooseTempHpCommand : EntityCommand
    {
        [XmlAttribute]
        public int Amount { get; set; }
        [XmlAttribute]
        public int? From { get; set; }
        [XmlAttribute]
        public int? To { get; set; }

        public LooseTempHpCommand(PlayableEntity entity, int amount) : base(entity.DisplayName)
        {
            Amount = amount;
        }
    }
}
