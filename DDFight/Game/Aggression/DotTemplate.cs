using DDFight.Game.DamageAffinity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Aggression
{
    public class DotTemplate : DamageTemplate, ICloneable
    {

        public DotTemplate(string damage, DamageTypeEnum damage_type) : base(damage, damage_type) { }
        public DotTemplate() : base() { }

        /// <summary>
        ///     opposed to end of turn
        /// </summary>
        public bool TriggersStartOfTurn
        {
            get => _triggersStartOfTurn;
            set
            {
                if (_triggersStartOfTurn != value)
                {
                    _triggersStartOfTurn = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _triggersStartOfTurn = true;

        public bool TriggersOnCastersTurn
        {
            get => _triggersOnCastersTurn;
            set
            {
                if (_triggersOnCastersTurn != value)
                {
                    _triggersOnCastersTurn = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _triggersOnCastersTurn = false;

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

        #endregion
    }
}
