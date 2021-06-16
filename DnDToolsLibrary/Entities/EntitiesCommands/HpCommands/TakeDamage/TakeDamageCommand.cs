using BaseToolsLibrary.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.TakeDamage
{
    /// <summary>
    ///     A super command, can be composed of both LooseTempHpCommand and LooseHpCommand
    /// </summary>
    public class TakeDamageCommand : EntitySuperCommand
    {
        [XmlAttribute]
        public int Amount { get; set; }

        public TakeDamageCommand(PlayableEntity entity, int amount) : base(entity.DisplayName)
        {
            Amount = amount;
        }
    }
}
