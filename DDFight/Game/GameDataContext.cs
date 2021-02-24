using DDFight.Game.Aggression.Spells;
using DDFight.Game.Entities;
using DDFight.Game.Fight;
using DDFight.Tools.Save;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace DDFight.Game
{
    /// <summary>
    ///     This class is intended to contain all the generic (and saved) informations:
    ///         - players
    ///         - ennemies schemes
    /// </summary>
    public class GameDataContext : INotifyPropertyChanged
    {
        public CharacterList CharacterList
        {
            get => _newCharacterList;
            set
            {
                _newCharacterList = value;
                NotifyPropertyChanged();
            }
        }
        private CharacterList _newCharacterList = new CharacterList();

        /// <summary>
        ///     The list of all possible spells encountered
        /// </summary>
        public SpellList SpellList
        {
            get => _spellList;
            set {
                _spellList = value;
                NotifyPropertyChanged();
            }
        }
        private SpellList _spellList = new SpellList();

        /// <summary>
        ///     The list of all possible ennemies encountered
        /// </summary>
        public MonsterList MonsterList
        {
            get => _monsterList;
            set
            {
                _monsterList = value;
                NotifyPropertyChanged();
            }
        }
        private MonsterList _monsterList = new MonsterList();

        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        /// <summary>
        ///     Used to log what happens on the user side
        /// </summary>
        [XmlIgnore]
        public FlowDocument UserLogs
        {
            get {
                return _userLogs; 
            }
            set
            {
                _userLogs = value;
                NotifyPropertyChanged();
            }
        }
        private FlowDocument _userLogs = new FlowDocument ();

        [XmlIgnore]
        public FightDataContext FightContext
        {
            get => _fightContext;
            set
            {
                _fightContext = value;
                NotifyPropertyChanged();
            }
        }
        private FightDataContext _fightContext = new FightDataContext();

    }
}
