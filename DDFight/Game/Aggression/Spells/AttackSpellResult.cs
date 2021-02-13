using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Aggression.Spells.Display;
using DDFight.Game.Entities;
using DDFight.Game.Status;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace DDFight.Game.Aggression.Spells
{
    public class AttackSpellResult : INotifyPropertyChanged
    {

        public void Cast(List<SpellAttackResultRollableUserControl> attacks)
        {
            Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(Extensions.BuildRun(Caster.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" casts a lvl ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(Extensions.BuildRun(Level.ToString() + " " + Name + "\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));


            int index = 0;
            foreach (PlayableEntity target in Targets)
            {
                SpellAttackResultRollableUserControl attack = attacks[index];
                target.GetAttacked(new HitAttackResult {
                    DamageList = attack.HitDamage,
                    HitBonus = this.ToHitBonus,
                    HitRoll = (uint)attack.AttackRoll,
                    OnHitStatuses = this.AppliedStatusList,
                    Owner = Caster,
                    Target = target,
                    SituationalHitAttackModifiers = new SituationalHitAttackModifiers{ACModifier = attack.CAModifier, HitModifier = attack.HitModifier},
                }, Caster);
                index += 1;
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

        public bool AutomaticalyHits
        {
            get => _automaticalyHits;
            set
            {
                if (_automaticalyHits != value)
                {
                    _automaticalyHits = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _automaticalyHits = false;

        public int ToHitBonus
        {
            get => _toHitBonus;
            set
            {
                _toHitBonus = value;
                NotifyPropertyChanged();
            }
        }
        private int _toHitBonus = 0;

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
