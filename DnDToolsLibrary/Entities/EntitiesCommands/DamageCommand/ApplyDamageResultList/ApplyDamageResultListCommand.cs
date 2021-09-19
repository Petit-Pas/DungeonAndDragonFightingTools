using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList
{
    public class ApplyDamageResultListCommand : EntitySuperCommand
    {
        /// <summary>
        ///     Should always be false, unless the damages were linked to a saving that has been succesfull
        ///         e.g. : resisting a spell or a poison
        /// </summary>
        [XmlAttribute]
        public bool LastSavingWasSuccessfull { get; set; } = false;

        public List<Damage> DamageList { get; set; } = new List<Damage>();

        public ApplyDamageResultListCommand(PlayableEntity target, DamageResultList damageList, bool lastSavingWasSuccessfull = false) 
            : base(target.DisplayName)
        {
            LastSavingWasSuccessfull = lastSavingWasSuccessfull;
            foreach (DamageResult dmg in damageList.Elements)
            {
                DamageList.Add(new Damage(
                    dmg.Damage.LastResult,
                    dmg.SituationalDamageModifier,
                    dmg.AffinityModifier,
                    dmg.DamageType
                    ));
            }
        }

        public class Damage
        {
            private Damage()
            {

            }

            public Damage(int rawAmount, DamageModifierEnum savingModifier, DamageAffinityEnum typeAffinity, DamageTypeEnum type)
            {
                RawAmount = rawAmount;
                SavingModifer = savingModifier;
                TypeAffinity = typeAffinity;
                Type = type;
            }

            /// <summary>
            ///     Raw amount of damages
            /// </summary>
            [XmlAttribute]
            public int RawAmount { get; set; }

            /// <summary>
            ///     The modifier to apply to the damage in cases where ApplyDamageResultListCommand.LastSavingWasSuccesfull is set to true
            /// </summary>
            [XmlAttribute]
            public DamageModifierEnum SavingModifer { get; set; }

            /// <summary>
            ///     The resistance/weakness to apply to this damage.
            ///     WARNING : this will not be evaluated by the target of the command. 
            ///         The value passed with the DamageResul will be used
            ///         ==> this allows for fine tuning by changning comportment at last moment.
            /// </summary>
            [XmlAttribute]
            public DamageAffinityEnum TypeAffinity { get; set; }

            /// <summary>
            ///     Type of damage
            /// </summary>
            [XmlAttribute]
            public DamageTypeEnum Type { get; set; }
        }
    }
}
