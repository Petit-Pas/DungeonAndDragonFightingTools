using DDFight.Game.Aggression;
using DDFight.Game.DamageAffinity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSandbox.Types
{
    public class DotTemplateType : DamageTemplate, ICloneable
    {

        public static DotTemplateList ConvertList(IEnumerable<DotTemplateType> list)
        {
            DotTemplateList result = new DotTemplateList();
            foreach (DotTemplateType type in list)
                result.AddElementSilent(Convert(type));
            return result;
        }

        private static DotTemplate Convert(DotTemplateType type)
        {
            DotTemplate result = new DotTemplate()
            {
                Damage = type.Damage,
                DamageType = type.DamageType,
                LastSavingWasSuccesfull = type.LastSavingWasSuccesfull,
                SituationalDamageModifier = type.SituationalDamageModifier,
                TriggersOnCastersTurn = type.TriggersOnCastersTurn,
                TriggersStartOfTurn = type.TriggersStartOfTurn,
            };
            return result;
        }

        public DotTemplateType(string damage, DamageTypeEnum damage_type) : base(damage, damage_type) { }
        public DotTemplateType() : base() { }

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

        private void init_copy(DotTemplateType to_copy)
        {
            TriggersStartOfTurn = to_copy.TriggersStartOfTurn;
            TriggersOnCastersTurn = to_copy.TriggersOnCastersTurn;
        }

        public DotTemplateType(DotTemplateType to_copy) : base(to_copy)
        {
            init_copy(to_copy);
        }
        public override object Clone()
        {
            return new DotTemplateType(this);
        }

        #endregion
    }
}
