using BaseToolsLibrary;
using DnDToolsLibrary.Attacks.Damage.Type;
using System;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Attacks.Damage
{
    public class DotTemplate : DamageTemplate, ICloneable, IEquivalentComparable<DotTemplate>
    {

        public DotTemplate(string damage, DamageTypeEnum damage_type) : base(damage, damage_type) { }
        public DotTemplate() : base() { }

        /// <summary>
        ///     opposed to end of turn
        /// </summary>
        [XmlAttribute]
        public bool TriggersStartOfTurn
        {
            get => _triggersStartOfTurn;
            set
            {
                if (_triggersStartOfTurn != value)
                {
                    _triggersStartOfTurn = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(TriggersEndOfTurn));
                }
            }
        }
        private bool _triggersStartOfTurn = true;

        [XmlIgnore]
        public bool TriggersEndOfTurn
        {
            get => !TriggersStartOfTurn;
            set
            {
                if (value != TriggersEndOfTurn)
                {
                    TriggersStartOfTurn = !value;
                }
            }
        }

        [XmlAttribute]
        public bool TriggersOnCastersTurn
        {
            get => _triggersOnCastersTurn;
            set
            {
                if (_triggersOnCastersTurn != value)
                {
                    _triggersOnCastersTurn = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(TriggersOnAffectedsTurn));
                }
            }
        }
        private bool _triggersOnCastersTurn = false;

        [XmlIgnore]
        public bool TriggersOnAffectedsTurn
        {
            get => !TriggersOnCastersTurn;
            set
            {
                if (TriggersOnAffectedsTurn != value)
                {
                    TriggersOnCastersTurn = !value;
                }
            }
        }


        #region ICloneable

        private void init_copy(DotTemplate to_copy)
        {
            TriggersStartOfTurn = to_copy.TriggersStartOfTurn;
            TriggersOnCastersTurn = to_copy.TriggersOnCastersTurn;
        }

        public DotTemplate(DotTemplate to_copy) : base(to_copy)
        {
            init_copy(to_copy);
        }
        public override object Clone()
        {
            return new DotTemplate(this);
        }

        public bool IsEquivalentTo(DotTemplate toCompare)
        {
            if (!base.IsEquivalentTo(toCompare))
                return false;
            if (TriggersOnCastersTurn != toCompare.TriggersOnCastersTurn)
                return false;
            if (TriggersStartOfTurn != toCompare.TriggersStartOfTurn)
                return false;
            return true;
        }

        #endregion
    }
}
