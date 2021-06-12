using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DnDToolsLibrary.Attacks.Spells
{
    public class NonAttackSpellResult : INotifyPropertyChanged
    {

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
