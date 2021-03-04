using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Entities;
using DDFight.Tools;
using DDFight.ValidationRules;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Game.Aggression.Spells.Display
{
    /// <summary>
    /// Interaction logic for SpellAttackResultRollableUserControl.xaml
    /// </summary>
    public partial class SpellAttackResultRollableUserControl : UserControl, INotifyPropertyChanged, IValidable
    {
        private PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }

        public SpellAttackResultRollableUserControl()
        {
            InitializeComponent();
        }

        public AttackSpellResult SpellResult
        {
            get { return (AttackSpellResult)this.GetValue(SpellResultProperty); }
            set 
            {
                this.SetValue(SpellResultProperty, value);
                NotifyPropertyChanged();
            }
        }
        public static readonly DependencyProperty SpellResultProperty = DependencyProperty.Register(
          nameof(SpellResult), typeof(AttackSpellResult), typeof(SpellAttackResultRollableUserControl), new PropertyMetadata(new AttackSpellResult(), new PropertyChangedCallback(OnSpellPropertyChanged)));

        private static void OnSpellPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((SpellAttackResultRollableUserControl)sender).RefreshHitDamage();
            ((SpellAttackResultRollableUserControl)sender).RefreshHitResult();
        }

        public void RefreshHitResult()
        {
            // the same AttackSpellResult serves all such controls, so we duplicate the important data
            RollResult = (AttackRollResult)SpellResult.RollResult.Clone();
            RollResult.Target = this.data_context;
        }

        public void RefreshHitDamage()
        {
            HitDamage = (DamageResultList)SpellResult.HitDamage.Clone();
        }

        public AttackRollResult RollResult
        {
            get => _rollResult;
            set
            {
                _rollResult = value;
                NotifyPropertyChanged();
            }
        }
        private AttackRollResult _rollResult = null;

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

        public bool IsValid()
        {
            if (RollResult == null)
                return false;
            if (RollResult.AttackRoll == 0)
                return false;
            foreach (DamageResult dmg in HitDamage.Elements)
            {
                if (dmg.Damage.LastRoll == 0)
                    return false;
            }
            return this.AreAllChildrenValid();
        }

    }
}
