using DDFight.Game.Characteristics;
using DDFight.Game.Status;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using DDFight.Game.Dices.SavingThrow;

namespace DDFight.Game.Aggression.Spells
{
    public class NonAttackSpellResult : INotifyPropertyChanged
    {

        public void Cast(List<SavingThrow> savings = null)
        {
            Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(Extensions.BuildRun(Caster.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" casts a lvl ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(Extensions.BuildRun(Level.ToString() + " " + Name + "\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));

            foreach (DamageTemplate dmg in HitDamage)
            {
                dmg.LastSavingWasSuccesfull = false;
            }
            for (int i = 0; i != Targets.Count; i += 1)
            {
                if (HasSavingThrow)
                {
                    paragraph.Inlines.Add(Extensions.BuildRun(Targets.ElementAt(i).DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    paragraph.Inlines.Add(Extensions.BuildRun(" (", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                    paragraph.Inlines.Add(Extensions.BuildRun(savings.ElementAt(i).Result + " / " + savings.ElementAt(i).Difficulty, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    paragraph.Inlines.Add(Extensions.BuildRun(") ==> ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                    if (savings.ElementAt(i).IsSuccesful)
                    {
                        foreach (DamageTemplate dmg in HitDamage)
                        {
                            dmg.LastSavingWasSuccesfull = true;
                        }
                        paragraph.Inlines.Add(Extensions.BuildRun("Resists\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    }
                    else
                    {
                        foreach (DamageTemplate dmg in HitDamage)
                        {
                            dmg.LastSavingWasSuccesfull = false;
                        }
                        paragraph.Inlines.Add(Extensions.BuildRun("Does not resist\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                    }
                }
                Targets.ElementAt(i).TakeHitDamage(HitDamage);
            }
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

        public List<DamageTemplate> HitDamage
        {
            get => _hitDamage;
            set
            {
                _hitDamage = value;
                NotifyPropertyChanged();
            }
        }
        private List<DamageTemplate> _hitDamage = new List<DamageTemplate>();

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
