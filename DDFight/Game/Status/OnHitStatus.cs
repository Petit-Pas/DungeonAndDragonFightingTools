using DDFight.Game.Characteristics;
using DDFight.Game.Dices.SavingThrow;
using DDFight.Game.Fight.FightEvents;
using DDFight.Game.Status.Display;
using DDFight.Tools;
using System.ComponentModel;
using System.Windows.Documents;
using System.Xml.Serialization;
using System.Windows;
using System.Windows.Media;
using DDFight.Game.Aggression;
using System.Collections.Generic;

namespace DDFight.Game.Status
{
    public class OnHitStatus : CustomVerboseStatus, IEventUnregisterable
    {
        public OnHitStatus()
        {
        }

        /// <summary>
        ///     The Entity that initiated the status, can be used when its concentration loss provokes the annulation of the status
        /// </summary>
        [XmlIgnore]
        public PlayableEntity Caster = null;

        /// <summary>
        ///     The Entity that is affected by the status, can be used to remove the status from its list of Statuses   
        /// </summary>
        [XmlIgnore]
        public PlayableEntity Affected = null;

        #region Spells

        /* WARNING : these (in Spells region) are only used when the status is applied by a spell
                     with a spell you can have a Saving Throw (on the spell, not the status)
                     Is is different than the Saving Throw you could set in the OnHitStatus itself
                     You could resist a Status from the Saving of the spell, not the saving of the Status itself
        */

        /// <summary>
        ///     Means it is applied by a spell that has a Saving Throw
        /// </summary>
        [XmlAttribute]
        public bool HasSpellSaving
        {
            get => _hasSpellSaving;
            set
            {
                _hasSpellSaving = value;
                NotifyPropertyChanged();
            }
        }    
        private bool _hasSpellSaving = false;

        [XmlAttribute]
        public ApplicationModifierEnum SpellApplicationModifier
        {
            get => _spellApplicationModifier;
            set
            {
                _spellApplicationModifier = value;
                NotifyPropertyChanged();
            }
        }
        private ApplicationModifierEnum _spellApplicationModifier = ApplicationModifierEnum.Maintained;

        [XmlIgnore]
        public bool SpellSavingWasSuccessful
        {
            get => _spellSavingWasSuccessful;
            set
            {
                _spellSavingWasSuccessful = value;
                NotifyPropertyChanged();
            }
        }
        private bool _spellSavingWasSuccessful = false;

        #endregion Spells

        #region Apply Properties

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

        #endregion Apply Properties

        #region Damage

        #region ApplyDamage

        public List<DamageTemplate> OnApplyDamageList
        {
            get => _onApplyDamageList;
            set {
                _onApplyDamageList = value;
                NotifyPropertyChanged();
            }
        }
        private List<DamageTemplate> _onApplyDamageList = new List<DamageTemplate> ();

        #endregion ApplyDamage

        #endregion Damage

        #region EndConditions

        #region Maximum Duration

        [XmlAttribute]
        public bool HasAMaximumDuration
        {
            get => _hasAMaximumDuration;
            set
            {
                _hasAMaximumDuration = value;
                NotifyPropertyChanged();
            }
        }
        private bool _hasAMaximumDuration = false;

        public bool DurationIsCalculatedOnCasterTurn
        {
            get => _durationIsCalculatedOnCasterTurn;
            set
            {
                _durationIsCalculatedOnCasterTurn = value;
                NotifyPropertyChanged();
            }
        }
        private bool _durationIsCalculatedOnCasterTurn = true;

        public bool DurationIsBasedOnStartOfTurn
        {
            get => _durationIsBasedOnStartOfTurn;
            set
            {
                _durationIsBasedOnStartOfTurn = value;
                NotifyPropertyChanged();
            }
        }
        private bool _durationIsBasedOnStartOfTurn = false;

        [XmlAttribute]
        public int RemainingRounds
        {
            get => _remainingRounds;
            set 
            {
                _remainingRounds = value;
                NotifyPropertyChanged();
            }
        }
        private int _remainingRounds = 0;

        /// <summary>
        ///     removes 1 turn from the Remaining rounds variable
        ///     if the status expires, the function removes it from the target of the status
        /// </summary>
        /// <returns></returns>
        private bool removeDuration()
        {
            RemainingRounds -= 1;
            if (RemainingRounds <= 0)
            {
                removeStatus();
                Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;
                paragraph.Inlines.Add(Extensions.BuildRun("The Status \"", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(this.Header, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
                paragraph.Inlines.Add(Extensions.BuildRun("\" inflicted by ", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(Caster.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
                paragraph.Inlines.Add(Extensions.BuildRun(" has expired on ", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(Affected.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
                paragraph.Inlines.Add(Extensions.BuildRun(".\r\n", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
                return true;
            }
            return false;
        }

        #endregion Maximum Duration

        #region Saving can be remade

        [XmlAttribute]
        public bool CanRedoSavingThrow
        {
            get => _canRedoSavingThrow;
            set 
            {
                _canRedoSavingThrow = value;
                NotifyPropertyChanged();
            }
        }
        private bool _canRedoSavingThrow = false;

        /// <summary>
        ///     This is only used if CanRedoSavingThrow is set to True
        ///         If this value is true ---> the saving is remade at start of turn
        ///         If this value is false --> the saving is remade at end of turn
        /// </summary>
        public bool SavingIsRemadeAtStartOfTurn 
        {
            get => _savingIsRemadeAtStartOfTurn;
            set 
            {
                _savingIsRemadeAtStartOfTurn = value;
                NotifyPropertyChanged();
            }
        }
        private bool _savingIsRemadeAtStartOfTurn = true;
        #endregion  Saving can be remade

        #region Concentration Loss

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

        #endregion Concentration

        #region End status events
        public void Affected_NewTurnStarted(object sender, StartNewTurnEventArgs args)
        {
            bool expired = false;

            if (HasAMaximumDuration && !DurationIsCalculatedOnCasterTurn && DurationIsBasedOnStartOfTurn)
                expired = removeDuration();
            if (!expired && CanRedoSavingThrow && SavingIsRemadeAtStartOfTurn)
            {
                OnHitStatusApplyWindow window = new OnHitStatusApplyWindow(Caster, Affected, false);
                window.DataContext = this;

                window.ShowCentered();
            }
        }

        public void Affected_TurnEnded(object sender, TurnEndedEventArgs args)
        {
            bool expired = false;

            if (HasAMaximumDuration && !DurationIsCalculatedOnCasterTurn && !DurationIsBasedOnStartOfTurn)
                expired = removeDuration();
            if (!expired && CanRedoSavingThrow && !SavingIsRemadeAtStartOfTurn)
            {
                OnHitStatusApplyWindow window = new OnHitStatusApplyWindow(Caster, Affected, false);
                window.DataContext = this;

                window.ShowCentered();
            }
        }

        private void Caster_TurnEnded(object sender, TurnEndedEventArgs args)
        {
            if (HasAMaximumDuration && DurationIsCalculatedOnCasterTurn && !DurationIsBasedOnStartOfTurn)
                removeDuration();
        }

        private void Caster_NewTurnStarted(object sender, StartNewTurnEventArgs args)
        {
            if (HasAMaximumDuration && DurationIsCalculatedOnCasterTurn && DurationIsBasedOnStartOfTurn)
                removeDuration();
        }

        public void Caster_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.EndsOnCasterLossOfConcentration && e.PropertyName == "IsFocused" && Caster.IsFocused == false)
            {
                removeStatus();
                Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

                paragraph.Inlines.Add(Extensions.BuildRun("Due to ", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(Caster.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
                paragraph.Inlines.Add(Extensions.BuildRun("'s loss of concentration, ", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(Affected.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
                paragraph.Inlines.Add(Extensions.BuildRun(" is not affected by ", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(this.Header, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
                paragraph.Inlines.Add(Extensions.BuildRun(" anymore.\r\n", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));

            }
        }

        #endregion End status events

        /// <summary>
        ///     Remove the status from the target, and unregister all events
        /// </summary>
        private void removeStatus()
        {
            Affected.CustomVerboseStatusList.List.Remove(this);
            UnregisterToAll();
        }

        #endregion EndConditons

        /// <summary>
        ///     Returns a saving throw object to know if the status is applied
        /// </summary>
        /// <param name="caster"> the one that tries to apply the status </param>
        /// <param name="target"> the target of the status </param>
        /// <returns></returns>
        public SavingThrow GetSavingThrow(PlayableEntity caster, PlayableEntity target)
        {
            SavingThrow result = new SavingThrow
            {
                Characteristic = this.ApplySavingCharacteristic,
                Difficulty = this.ApplySavingDifficulty != 0 ? this.ApplySavingDifficulty : caster.SpellSave,
                Target = target,
            };
            return result;
        }

        #region Application Methods

        /// <summary>
        ///     A function that applies this status to the given target
        ///     
        ///     it will register to any required event for the status to automatically ends
        /// </summary>
        /// <param name="caster"> the one that tries to apply the status </param>
        /// <param name="target"> the target of the status </param>
        /// <param name="success"> tells wether or not the application is a success, only used with "false" to tell the OnApplyDamage can be resisted / canceled </param>
        public void Apply(PlayableEntity caster, PlayableEntity target, bool success = true)
        {
            if (OnApplyDamageList.Count != 0)
            {
                if (!success)
                    // the target resisted
                    foreach (DamageTemplate dmg in OnApplyDamageList)
                        dmg.LastSavingWasSuccesfull = true;
                target.TakeHitDamage(OnApplyDamageList);
            }
            if (success)
            {
                this.Caster = caster;
                this.Affected = target;
                if (this.EndsOnCasterLossOfConcentration)
                {
                    if (caster.IsFocused == true)
                        caster.IsFocused = false;
                    caster.PropertyChanged += this.Caster_PropertyChanged;
                    caster.IsFocused = true;
                }
                if ((this.CanRedoSavingThrow && this.SavingIsRemadeAtStartOfTurn) ||
                    (this.HasAMaximumDuration && !this.DurationIsCalculatedOnCasterTurn && this.DurationIsBasedOnStartOfTurn))
                    target.NewTurnStarted += this.Affected_NewTurnStarted;
                if ((this.CanRedoSavingThrow && this.SavingIsRemadeAtStartOfTurn == false) ||
                    (this.HasAMaximumDuration && !this.DurationIsCalculatedOnCasterTurn && !this.DurationIsBasedOnStartOfTurn))
                    target.TurnEnded += this.Affected_TurnEnded;
                if (this.HasAMaximumDuration && this.DurationIsCalculatedOnCasterTurn && this.DurationIsBasedOnStartOfTurn)
                    caster.NewTurnStarted += Caster_NewTurnStarted;
                if (this.HasAMaximumDuration && this.DurationIsCalculatedOnCasterTurn && !this.DurationIsBasedOnStartOfTurn)
                    caster.TurnEnded += Caster_TurnEnded;
            }
        }

        /// <summary>
        ///     Will open a window if a check has to be made for the OnHitStatus to affect the target, then apply the status if required
        /// </summary>
        /// <param name="caster"> the one that tries to apply the status </param>
        /// <param name="target"> the target of the status </param>
        public void CheckIfApply(PlayableEntity caster, PlayableEntity target)
        {
            if (this.HasApplyCondition)
            {
                OnHitStatusApplyWindow window = new OnHitStatusApplyWindow(caster, target);
                window.DataContext = this;
                window.ShowCentered();
            }
            else
            {
                Apply(caster, target);
                Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;
                paragraph.Inlines.Add(Extensions.BuildRun(caster.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" applies ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(Header, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" on ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(target.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun("\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            }
        }

        #endregion Application Methods

        /// <summary>
        ///     In this method implements any "end of condition" such as : 
        ///     - after n turns
        ///     - after a saving throw has been successfully remade
        ///     - after 10 rounds
        ///     - etc...
        ///     
        ///     Will be used as such condition can be vanished by the end of a fight
        /// </summary>
        /// <returns> true if so, false otherwise </returns>
        public bool HasEndCondition()
        {
            if (EndsOnCasterLossOfConcentration)
                return true;
            if (CanRedoSavingThrow)
                return true;
            // if more than 50 rounds remain (5 mins), it is left on the characters, in case a second fight comes close
            if (HasAMaximumDuration && RemainingRounds < 50)
                return true;
            return false;
        }

        /// <summary>
        ///     Will open an edit window to updates the status
        /// </summary>
        /// <returns></returns>
        public override bool OpenEditWindow()
        {

            OnHitStatusEditWindow window = new OnHitStatusEditWindow();
            OnHitStatus dc = (OnHitStatus)this.Clone();
            window.DataContext = dc;

            window.ShowCentered();

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
            CanRedoSavingThrow = to_copy.CanRedoSavingThrow;
            SavingIsRemadeAtStartOfTurn = to_copy.SavingIsRemadeAtStartOfTurn;
            RemainingRounds = to_copy.RemainingRounds;
            HasAMaximumDuration = to_copy.HasAMaximumDuration;
            DurationIsCalculatedOnCasterTurn = to_copy.DurationIsCalculatedOnCasterTurn;
            DurationIsBasedOnStartOfTurn = to_copy.DurationIsBasedOnStartOfTurn;
            OnApplyDamageList = (List<DamageTemplate>)to_copy.OnApplyDamageList.Clone();
            HasSpellSaving = to_copy.HasSpellSaving;
            SpellApplicationModifier = to_copy.SpellApplicationModifier;
            SpellSavingWasSuccessful = to_copy.SpellSavingWasSuccessful;
        }

        public OnHitStatus(OnHitStatus to_copy)
        {
            init_copy(to_copy);
        }

        public void CopyAssign(OnHitStatus to_copy)
        {
            init_copy(to_copy);
        }

        /// <summary>
        ///     Unregister to all possible events
        /// </summary>
        public void UnregisterToAll()
        {
            Caster.PropertyChanged -= Caster_PropertyChanged;
            Caster.NewTurnStarted -= Caster_NewTurnStarted;
            Affected.NewTurnStarted -= Affected_NewTurnStarted;
            Affected.TurnEnded -= Affected_TurnEnded;
            Caster.TurnEnded -= Caster_TurnEnded;
        }

        #endregion ICloneable
    }
}
