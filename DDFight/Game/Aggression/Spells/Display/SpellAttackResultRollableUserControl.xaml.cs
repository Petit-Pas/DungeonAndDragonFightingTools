using DDFight.Tools.UXShortcuts;
using DDFight.ValidationRules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            DataContextChanged += SpellAttackResultRollableUserControl_DataContextChanged;
            this.PropertyChanged += SpellAttackResultRollableUserControl_PropertyChanged;
        }

        private void refresh_result()
        {
            uint CA = data_context.CA + (uint)this.CAModifier;
            int score = this.HitModifier + this.AttackRoll + this.Spell.ToHitBonus;
            string result = score.ToString() + "/" + CA.ToString();

            Hits = CA <= score ? true : false;
            HitResultString = result;
        }

        private void SpellAttackResultRollableUserControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            refresh_result();
        }

        private void SpellAttackResultRollableUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            refresh_result();
        }

        public AttackSpellResult Spell
        {
            get { return (AttackSpellResult)this.GetValue(SpellProperty); }
            set 
            {
                this.SetValue(SpellProperty, value);
                NotifyPropertyChanged();
            }
        }
        public static readonly DependencyProperty SpellProperty = DependencyProperty.Register(
          "Spell", typeof(AttackSpellResult), typeof(SpellAttackResultRollableUserControl), new PropertyMetadata(new AttackSpellResult(), new PropertyChangedCallback(OnSpellPropertyChanged)));

        public void RefreshHitDamage()
        {
            HitDamage = (List<DamageTemplate>)Spell.HitDamage.Clone();
        }

        private static void OnSpellPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((SpellAttackResultRollableUserControl)sender).RefreshHitDamage();
        }

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


        public int AttackRoll
        {
            get => _attackRoll;
            set
            {
                if (_attackRoll != value)
                {
                    _attackRoll = value;
                    NotifyPropertyChanged();
                    if (_attackRoll == 20)
                        Crits = true;
                    else
                        Crits = false;
                }
            }
        }
        private int _attackRoll = 0;

        public bool Crits
        {
            get => _crits;
            set
            {
                if (_crits != value)
                {
                    _crits = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _crits = false;

        public int CAModifier
        {
            get => _caModifier;
            set
            {
                if (_caModifier != value)
                {
                    _caModifier = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _caModifier = 0;

        public int HitModifier
        {
            get => _hitModifier;
            set
            {
                if (_hitModifier != value)
                {
                    _hitModifier = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _hitModifier = 0;

        public bool HasAdvantage
        {
            get => _hasAdvantage;
            set
            {
                if (_hasAdvantage != value)
                {
                    _hasAdvantage = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _hasAdvantage = false;

        public bool HasDisAdvantage
        {
            get => _hasDisAdvantage;
            set
            {
                if (_hasDisAdvantage != value)
                {
                    _hasDisAdvantage = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _hasDisAdvantage = false;

        public bool Hits
        {
            get => _hits;
            set
            {
                if (_hits != value)
                {
                    _hits = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _hits = false;

        public string HitResultString {
            get => _hitResultString;
            set
            {
                if (_hitResultString != value)
                {
                    _hitResultString = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _hitResultString = "0/0";

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
            if (AttackRoll == 0)
                return false;
            foreach (DamageTemplate dmg in HitDamage)
            {
                if (dmg.Damage.LastRoll == 0)
                    return false;
            }
            return this.AreAllChildrenValid();
        }

    }
}
