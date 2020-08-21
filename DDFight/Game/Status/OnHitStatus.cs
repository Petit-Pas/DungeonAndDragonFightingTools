using DDFight.Game.Characteristics;
using DDFight.Game.Dices.SavingThrow;
using DDFight.Game.Status.Display;
using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDFight.Game.Status
{
    public class OnHitStatus : CustomVerboseStatus, IEventUnregisterable
    {
        public OnHitStatus()
        {
        }

        #region Properties

        /// <summary>
        ///     The Entity that initiated the status, can be used when its concentration loss provokes the annulation of the status
        /// </summary>
        public PlayableEntity Caster = null;

        /// <summary>
        ///     The Entity that is affected by the status, can be used to remove the status from its list of Statuses   
        /// </summary>
        public PlayableEntity Affected = null;

        #region Apply

        [XmlAttribute]
        public bool HasApplyCondition
        {
            get => _hasApplyCondition;
            set
            {
                _hasApplyCondition = value;
                NotifyPropertyChanged();
            }
        }
        private bool _hasApplyCondition = false;

        [XmlAttribute]
        public CharacteristicsEnum ApplySavingCharacteristic
        {
            get => _applySavingCharacteristic;
            set
            {
                _applySavingCharacteristic = value;
                NotifyPropertyChanged();
            }
        }
        private CharacteristicsEnum _applySavingCharacteristic = CharacteristicsEnum.Wisdom;

        // 0 for default spellcasting ability DC
        [XmlAttribute]
        public int ApplySavingDifficulty
        {
            get => _applySavingDifficulty;
            set
            {
                _applySavingDifficulty = value;
                NotifyPropertyChanged();
            }
        }
        private int _applySavingDifficulty = 0;

        #endregion Apply

        #region EndConditions

        [XmlAttribute]
        public bool EndsOnCasterLossOfConcentration
        {
            get => _endsOnCasterLossOfConcentration;
            set
            {
                _endsOnCasterLossOfConcentration = value;
                NotifyPropertyChanged();
            }
        }
        private bool _endsOnCasterLossOfConcentration = false;

        public void Caster_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsFocused" && Caster.IsFocused == false)
            {
                Affected.CustomVerboseStatusList.List.Remove(this);
            }
        }

        #endregion EndConditons

        #endregion Properties

        public SavingThrow GetSavingThrow(PlayableEntity applicant, PlayableEntity target)
        {
            SavingThrow result = new SavingThrow
            {
                Characteristic = this.ApplySavingCharacteristic,
                Difficulty = this.ApplySavingDifficulty != 0 ? this.ApplySavingDifficulty : applicant.SpellSave,
                Target = target,
            };
            return result;
        }

        public bool Apply(PlayableEntity applicator, PlayableEntity target)
        {
            OnHitStatusApplyWindow window = new OnHitStatusApplyWindow(applicator, target);
            window.DataContext = this;
            window.ShowDialog();

            return false;
        }

        public override bool OpenEditWindow()
        {

            OnHitStatusEditWindow window = new OnHitStatusEditWindow();
            OnHitStatus dc = (OnHitStatus)this.Clone();
            window.DataContext = dc;

            window.ShowDialog();

            if (dc.Validated)
            {
                this.CopyAssign(dc);
                return true;
            }
            return false;
        }

        #region ICloneable

        public override object Clone()
        {
            return new OnHitStatus(this);
        }

        protected void init_copy(OnHitStatus to_copy)
        {
            base.init_copy(to_copy);
            HasApplyCondition = to_copy.HasApplyCondition;
            ApplySavingCharacteristic = to_copy.ApplySavingCharacteristic;
            ApplySavingDifficulty = to_copy.ApplySavingDifficulty;
            EndsOnCasterLossOfConcentration = to_copy.EndsOnCasterLossOfConcentration;
        }

        public OnHitStatus(OnHitStatus to_copy)
        {
            init_copy(to_copy);
        }

        public void CopyAssign(OnHitStatus to_copy)
        {
            init_copy(to_copy);
        }

        public void UnregisterToAll()
        {
            Caster.PropertyChanged -= Caster_PropertyChanged;
        }

        #endregion ICloneable
    }
}
