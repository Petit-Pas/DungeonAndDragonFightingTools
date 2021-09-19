using System.Xml.Serialization;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.Heal
{
    public class HealCommand : EntityCommand
    {
        [XmlAttribute]
        public int Amount { get; set; }
        [XmlAttribute]
        public int? From { get; set ; }
        [XmlAttribute]
        public int? To { get; set; }

        public HealCommand(PlayableEntity target, int amount) : base(target.DisplayName)
        {
            Amount = amount;
        }
    }
}
