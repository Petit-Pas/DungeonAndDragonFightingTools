using BaseToolsLibrary.Mediator;
using System;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands
{
    public class HealEntityCommand : EntityCommand
    {
        [XmlAttribute]
        public int Amount { get; set; }
        [XmlAttribute]
        public int? From { get; set ; }
        [XmlAttribute]
        public int? To { get; set; }

        public HealEntityCommand(PlayableEntity target, int amount) : base(target.DisplayName)
        {
            Amount = amount;
        }
    }
}
