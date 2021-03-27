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
using DDFight.Game.Aggression.Display;
using DDFight.Game.Entities;
using System;

namespace DDFight.Game.Status
{
    public class OnHitStatus : CustomVerboseStatus, IEventUnregisterable
    {

        public OnHitStatus()
        {
        }

        #region Properties

        #region Properties_Concerned

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

        #endregion Properties_Concerned

        #region Properties_ApplyCondition

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

        #endregion Properties_ApplyCondition

        #region Properties_Damage

        #region Properties_Damage_OnApply

        public DamageTemplateList OnApplyDamageList
        {
            get => _onApplyDamageList;
            set
            {
                _onApplyDamageList = value;
                NotifyPropertyChanged();
            }
        }
        private DamageTemplateList _onApplyDamageList = new DamageTemplateList();

        #endregion Properties_Damage_OnApply

        #region Properties_Damage_Dot

        public DotTemplateList DotDamageList
        {
            get => _dotDamageList;
            set
            {
                _dotDamageList = value;
                NotifyPropertyChanged();
            }
        }
        private DotTemplateList _dotDamageList = new DotTemplateList();

        #endregion Properties_Damage_Dot

        #endregion Properties_Damage

        #region Properties_EndCondition

        #region Properties_EndCondition_MaximumDuration
        
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

        [XmlAttribute]
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

        [XmlAttribute]
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

        #endregion Properties_EndCondition_MaximumDuration

        #region Properties_EndCondition_SavingRemade
        
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
        ///     Only used if CanRedoSavingThrow is set to true
        ///     true ==> start of turn
        ///     false ==> end of turn
        /// </summary>
        [XmlAttribute]
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

        #endregion properties_EndCondition_SavingRemade

        #region Properties_EndConditions_Concentration

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

        #endregion Properties_EndConditions_Concentration

        #endregion Properties_EndCondition

        #endregion Properties

        #region Spells

        // TODO check if this is realy necessary

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

        #region InteractionLogic

        #region InteractionLogic_Damage

        #region InteractionLogic_Damage_Dot

        /// <summary>
        ///     Will trigger any dot damage required
        /// </summary>
        /// <param name="start"> true if its start of turn, false otherwise </param>
        /// <param name="caster"> true if its caster's turn, false otherwise </param>
        private void checkDotDamage(bool start, bool caster)
        {
            DamageResultList to_apply = new DamageResultList();
            foreach (DotTemplate dot in DotDamageList.Elements)
            {
                if (dot.TriggersStartOfTurn == start && dot.TriggersOnCastersTurn == caster)
                    to_apply.AddElementSilent(new DamageResult(dot));
            }
            if (to_apply.Elements.Count != 0)
            {
                DamageResultListRollableWindow window = new DamageResultListRollableWindow() { DataContext = to_apply, };
                window.TitleControl.Text = this.Header + " inflicts damage to " + Affected.DisplayName;
                window.ShowCentered();

                if (window.Validated)
                {
                    Affected.TakeHitDamage(to_apply);
                }
            }
        }

        #endregion InteractionLogic_Damage_Dot

        #endregion InteractionLogic_Damage

        #region InteractionLogic_EndCondition

        #region InteractionLogic_EndCondition_MaximumDuration

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
                Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;
                paragraph.Inlines.Add(Extensions.BuildRun("The Status inflicted by ", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(Caster.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
                paragraph.Inlines.Add(Extensions.BuildRun(" has expired. ", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
                removeStatus();

                // if caster was focused on this, he can now be "un"focused
                if (this.EndsOnCasterLossOfConcentration && this.Caster.IsFocused && this.Caster == this.Affected)
                    this.Caster.IsFocused = false;

                return true;
            }
            return false;
        }

        #endregion InteractionLogic_EndCondition_MaximumDuration

        /// <summary>
        ///     Remove the status from the target, and unregister all events
        /// </summary>
        private void removeStatus()
        {
            Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(Extensions.BuildRun(Affected.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
            paragraph.Inlines.Add(Extensions.BuildRun(" is no more affected by ", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(Extensions.BuildRun(this.Header, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
            paragraph.Inlines.Add(Extensions.BuildRun(".\r\n", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));

            Affected.CustomVerboseStatusList.RemoveElement(this);
            UnregisterToAll();
        }

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

        #endregion InteractionLogic_EndCondition

        #endregion InteractionLogic

        #region Events

        public void Affected_NewTurnStarted(object sender, StartNewTurnEventArgs args)
        {
            bool expired = false;

            checkDotDamage(true, false);

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

            checkDotDamage(false, false);

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
            checkDotDamage(false, true);

            if (HasAMaximumDuration && DurationIsCalculatedOnCasterTurn && !DurationIsBasedOnStartOfTurn)
                removeDuration();
        }

        private void Caster_NewTurnStarted(object sender, StartNewTurnEventArgs args)
        {
            checkDotDamage(true, true);

            if (HasAMaximumDuration && DurationIsCalculatedOnCasterTurn && DurationIsBasedOnStartOfTurn)
                removeDuration();
        }

        public void Caster_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.EndsOnCasterLossOfConcentration && e.PropertyName == "IsFocused" && Caster.IsFocused == false)
            {
                Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

                paragraph.Inlines.Add(Extensions.BuildRun("Due to ", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(Caster.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
                paragraph.Inlines.Add(Extensions.BuildRun("'s loss of concentration, ", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
                removeStatus();
            }
        }

        #endregion Events

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


        /// <summary>
        ///     A function that applies this status to the given target
        ///     it will register to any required event for the status to automatically ends
        /// </summary>
        /// <param name="caster"> the one that tries to apply the status </param>
        /// <param name="target"> the target of the status </param>
        /// <param name="application_success"> tells wether or not the application is a success, only used with "false" to tell the OnApplyDamage can be resisted / canceled </param>
        /// <param name="multiple_application"> tells that a status will be applied more than once ==> to avoid the removal of concentration on every new affected ==> false for the first call, true for the other ones </param>
        public void Apply(PlayableEntity caster, PlayableEntity target, bool application_success = true, bool multiple_application = false)
        {

            // the applied status is a copy
            OnHitStatus applied = (OnHitStatus)this.Clone();

            if (applied.OnApplyDamageList.Elements.Count != 0)
            {
                DamageResultList onApplyDamageList = OnApplyDamageList.GetResultList();
                foreach (DamageResult dmg in onApplyDamageList.Elements)
                    dmg.LastSavingWasSuccesfull = !application_success;
                DamageResultListRollableWindow window = new DamageResultListRollableWindow() { DataContext=onApplyDamageList, };
                window.ShowCentered();
                if (window.Validated)
                    target.TakeHitDamage(onApplyDamageList);
            }

            if (application_success)
            {
                Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

                paragraph.Inlines.Add(Extensions.BuildRun(caster.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" applies ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(this.Header, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" on ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(target.DisplayName + "\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));

                target.CustomVerboseStatusList.AddElementSilent(applied);
                applied.Caster = caster;
                applied.Affected = target;
                if ((applied.CanRedoSavingThrow && applied.SavingIsRemadeAtStartOfTurn) ||
                    (applied.HasAMaximumDuration && !applied.DurationIsCalculatedOnCasterTurn && applied.DurationIsBasedOnStartOfTurn) ||
                    applied.DotDamageList.Elements.Count != 0)
                    target.NewTurnStarted += applied.Affected_NewTurnStarted;
                if ((applied.CanRedoSavingThrow && applied.SavingIsRemadeAtStartOfTurn == false) ||
                    (applied.HasAMaximumDuration && !applied.DurationIsCalculatedOnCasterTurn && !applied.DurationIsBasedOnStartOfTurn) ||
                    applied.DotDamageList.Elements.Count != 0)
                    target.TurnEnded += applied.Affected_TurnEnded;
                if ((applied.HasAMaximumDuration && applied.DurationIsCalculatedOnCasterTurn && applied.DurationIsBasedOnStartOfTurn) ||
                    applied.DotDamageList.Elements.Count != 0)
                    caster.NewTurnStarted += applied.Caster_NewTurnStarted;
                if ((applied.HasAMaximumDuration && applied.DurationIsCalculatedOnCasterTurn && !applied.DurationIsBasedOnStartOfTurn) ||
                    applied.DotDamageList.Elements.Count != 0)
                    caster.TurnEnded += applied.Caster_TurnEnded;
                if (applied.EndsOnCasterLossOfConcentration)
                {
                    if (caster.IsFocused == true && multiple_application == false)
                        caster.IsFocused = false;
                    caster.PropertyChanged += applied.Caster_PropertyChanged;
                    caster.IsFocused = true;
                }
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
            }
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

            if (window.Validated)
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
            OnApplyDamageList = (DamageTemplateList)to_copy.OnApplyDamageList.Clone();
            DotDamageList = (DotTemplateList)to_copy.DotDamageList.Clone();
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
            if (Caster != null)
            {
                Caster.PropertyChanged -= Caster_PropertyChanged;
                Caster.NewTurnStarted -= Caster_NewTurnStarted;
                Caster.TurnEnded -= Caster_TurnEnded;
            }
            if (Affected != null)
            {
                Affected.NewTurnStarted -= Affected_NewTurnStarted;
                Affected.TurnEnded -= Affected_TurnEnded;
            }
        }

        #endregion ICloneable
    }
}
