using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DnDToolsLibrary.Attacks.Spells
{
    public class NonAttackSpellResult : INotifyPropertyChanged
    {

        public void Cast(List<SavingThrow> savings = null)
        {
            Console.WriteLine("CRITICAL");
            /*

            Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(Extensions.BuildRun(Caster.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" casts a lvl ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(Extensions.BuildRun(Level.ToString() + " " + Name + "\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));

            if (Targets.Contains(Caster))
            {
                // This is to move the Caster at the end of the list, organizing things better for the Log Console if there is a Status
                Targets.Remove(Caster);
                Targets.Add(Caster);
            }

            bool already_applied = false;

            if (this.HasSavingThrow)
            {
                foreach (OnHitStatus status in this.AppliedStatusList.Elements)
                {
                    status.ApplySavingCharacteristic = this.SavingCharacteristic;
                    status.ApplySavingDifficulty = this.SavingDifficulty;
                }
            }
            foreach (DamageResult dmg in HitDamage.Elements)
            {
                dmg.LastSavingWasSuccesfull = false;
            }
            
            for (int i = 0; i != Targets.Count; i += 1)
            {
                // damage lessening if has saving throw
                if (HasSavingThrow)
                {
                    paragraph.Inlines.Add(Extensions.BuildRun(Targets.ElementAt(i).DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    paragraph.Inlines.Add(Extensions.BuildRun(" (", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                    paragraph.Inlines.Add(Extensions.BuildRun(savings.ElementAt(i).Result + " / " + savings.ElementAt(i).Difficulty, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    paragraph.Inlines.Add(Extensions.BuildRun(") ==> ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                    if (savings.ElementAt(i).IsSuccesful)
                    {
                        foreach (DamageResult dmg in HitDamage.Elements)
                        {
                            dmg.LastSavingWasSuccesfull = true;
                        }
                        paragraph.Inlines.Add(Extensions.BuildRun("Resists\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    }
                    else
                    {
                        foreach (DamageResult dmg in HitDamage.Elements)
                        {
                            dmg.LastSavingWasSuccesfull = false;
                        }
                        paragraph.Inlines.Add(Extensions.BuildRun("Does not resist\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    }
                }
                // damage application
                if (this.HitDamage.Count != 0)
                    Targets.ElementAt(i).TakeHitDamage(HitDamage);

                // OnHitStatus Application
                foreach (OnHitStatus status in this.AppliedStatusList.Elements)
                {
                    if (this.HasSavingThrow == false)
                    {
                        status.Apply(this.Caster, Targets.ElementAt(i), multiple_application: already_applied);
                        already_applied = true;
                    }
                    else if (savings.ElementAt(i).IsSuccesful == false)
                    {
                        status.Apply(this.Caster, Targets.ElementAt(i), multiple_application: already_applied);
                        already_applied = true;
                    }
                    else if (this.HasSavingThrow = true && savings.ElementAt(i).IsSuccesful && status.SpellApplicationModifier == ApplicationModifierEnum.Maintained)
                    {
                        status.Apply(this.Caster, Targets.ElementAt(i), application_success: false, multiple_application: already_applied);
                        already_applied = true;
                    }
                }
            }*/
        }

        #region Properties
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        private string _name = "";

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                NotifyPropertyChanged();
            }
        }
        private int _level = 0;

        public DamageResultList HitDamage
        {
            get => _hitDamage;
            set
            {
                _hitDamage = value;
                NotifyPropertyChanged();
            }
        }
        private DamageResultList _hitDamage = new DamageResultList();

        public OnHitStatusList AppliedStatusList
        {
            get => _appliedStatusList;
            set
            {
                _appliedStatusList = value;
                NotifyPropertyChanged();
            }
        }
        private OnHitStatusList _appliedStatusList = new OnHitStatusList();

        public CharacteristicsEnum SavingCharacteristic
        {
            get => _savingCharacteristic;
            set
            {
                _savingCharacteristic = value;
                NotifyPropertyChanged();
            }
        }
        private CharacteristicsEnum _savingCharacteristic = CharacteristicsEnum.Dexterity;

        public int SavingDifficulty
        {
            get => _savingDifficulty;
            set
            {
                _savingDifficulty = value;
                NotifyPropertyChanged();
            }
        }
        private int _savingDifficulty = 0;


        public bool HasSavingThrow
        {
            get => _hasSavingThrow;
            set
            {
                _hasSavingThrow = value;
                NotifyPropertyChanged();
            }
        }
        private bool _hasSavingThrow = false;

        public ObservableCollection<PlayableEntity> Targets
        {
            get => _targets;
            set
            {
                _targets = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<PlayableEntity> _targets = new ObservableCollection<PlayableEntity>();

        public PlayableEntity Caster
        {
            get => _caster;
            set
            {
                _caster = value;
                NotifyPropertyChanged();
            }
        }
        private PlayableEntity _caster = null;
        
        #endregion Properties

        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
