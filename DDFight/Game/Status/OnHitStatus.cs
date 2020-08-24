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
        [XmlIgnore]
        public PlayableEntity Caster = null;

        /// <summary>
        ///     The Entity that is affected by the status, can be used to remove the status from its list of Statuses   
        /// </summary>
        [XmlIgnore]
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

        #region MaximumLength

        [XmlAttribute]
        public bool HasAMaximumDuration
        {
            get => _hasAMaximumLength;
            set 
            {
                _hasAMaximumLength = value;
                NotifyPropertyChanged();
            }
        }
        private bool _hasAMaximumLength = false;

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

        public void Applicant_TurnEnded(object sender, Fight.FightEvents.TurnEndedEventArgs args)
        {
            if (HasAMaximumDuration == true && args.Character == Caster)
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
            }
        }

        #endregion MaximumLength

        #region SavingRemade

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

        public void Affected_NewTurnStarted(object sender, StartNewTurnEventArgs args)
        {
            OnHitStatusApplyWindow window = new OnHitStatusApplyWindow(Caster, Affected, false);
            window.DataContext = this;

            window.ShowDialog();
        }

        public void Affected_TurnEnded(object sender, TurnEndedEventArgs args)
        {
            OnHitStatusApplyWindow window = new OnHitStatusApplyWindow(Caster, Affected, false);
            window.DataContext = this;

            window.ShowDialog();
        }


        #endregion SavingRemade

        #region Concentration

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

        private void removeStatus()
        {
            Affected.CustomVerboseStatusList.List.Remove(this);
            UnregisterToAll();
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

        public void Apply(PlayableEntity applicant, PlayableEntity target)
        {
            target.CustomVerboseStatusList.List.Add(this);
            this.Caster = applicant;
            this.Affected = target;
            if (this.EndsOnCasterLossOfConcentration)
                applicant.PropertyChanged += this.Caster_PropertyChanged;
            if (this.CanRedoSavingThrow == true)
                if (this.SavingIsRemadeAtStartOfTurn == true)
                    target.NewTurnStarted += this.Affected_NewTurnStarted;
                else
                    target.TurnEnded += this.Affected_TurnEnded;
            if (this.HasAMaximumDuration == true)
                applicant.TurnEnded += this.Applicant_TurnEnded;
        }

        /// <summary>
        ///     Will open a window if a check has to be made for the OnHitStatus to affect the target
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public void CheckIfApply(PlayableEntity caster, PlayableEntity target)
        {
            if (this.HasApplyCondition)
            {
                OnHitStatusApplyWindow window = new OnHitStatusApplyWindow(caster, target);
                window.DataContext = this;
                window.ShowDialog();
            }
            else
            {
                Apply(caster, target);
            }
        }

        /// <summary>
        ///     In this method should be implemented any "end of condition" such as : 
        ///     - after n turns
        ///     - after a saving throw has been successfully remade
        ///     - after 10 rounds
        ///     - etc...
        ///     
        ///     Will be used as such condition can be vanished by the end of a fight
        /// </summary>
        /// <returns></returns>
        public bool HasEndCondition()
        {
            //TODO implement them all
            if (EndsOnCasterLossOfConcentration == true)
                return true;
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
            CanRedoSavingThrow = to_copy.CanRedoSavingThrow;
            SavingIsRemadeAtStartOfTurn = to_copy.SavingIsRemadeAtStartOfTurn;
            RemainingRounds = to_copy.RemainingRounds;
            HasAMaximumDuration = to_copy.HasAMaximumDuration;
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
